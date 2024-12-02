using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 10f;
    public float jumpHeight = 4;
    public float airControl = 100;
    public float dash = 100000;

    [Header("Ground check")]
    public Transform Legs;
    public float radius = 0.2f;
    public LayerMask groundMask;

    [Header("Jump Mechanics")]
    public float coyoteTime = 0.2f;
    public float jumpBufferTime = 0.2f;

    private float coyoteGround;
    private float doubleJump = 1;
    private float jumpBufferGround;
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

        if (isGround)
        {
            coyoteGround =  coyoteTime;
            doubleJump = 1;
        }
        else
        {
            coyoteGround -= Time.deltaTime;
        }

        if(Input.GetButtonDown("Jump"))
        {
            jumpBufferGround = jumpBufferTime;
        }
        else
        {
            jumpBufferGround -= Time.deltaTime;
        }

        if (doubleJump > 0 && Input.GetButtonDown("Jump"))
        {
            doubleJump -= 1;

            var jumpForce = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y * rb.gravityScale);
            rb.velocity = new Vector2(rb.velocity.x * airControl, jumpForce);
        }
        if(jumpBufferGround > 0 && coyoteGround > 0 && doubleJump > 0)
        {
            jumpBufferGround = 0;

            var jumpForce = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y * rb.gravityScale);
            rb.velocity = new Vector2(rb.velocity.x * airControl, jumpForce);
        }
        if(Input.GetButtonDown("Dash"))
        {
            rb.AddForce(rb.velocity.normalized * dash, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void OnDrawGizmos()
    {
        if(Legs !=  null)
        {
            Gizmos.DrawWireSphere(Legs.position, radius);
        }
            
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.relativeVelocity.y > 25)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
