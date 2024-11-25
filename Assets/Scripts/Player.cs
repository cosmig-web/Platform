using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 10f;
    public float jumpHeight = 4;
    public float airControl = 100;

    [Header("Ground check")]
    public Transform Legs;
    public float radius = 0.2f;
    public LayerMask groundMask;

    private Rigidbody2D rb;
    private float horizontal;
    private bool isGround;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        isGround = Physics2D.OverlapCircle(Legs.position, radius, groundMask);

        if(isGround && Input.GetButtonDown("Jump"))
        {
            var jumpForce = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
            rb.velocity = new Vector2(rb.velocity.x * airControl, jumpForce);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void OnDrawGizmos()
    {
        /*if(Legs !=  null)
        {
            Gizmos.DrawWireSphere(Legs.position, radius);
        }*/
            
    }
}
