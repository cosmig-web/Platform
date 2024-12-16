using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 0.2f;
    public Transform LegsRight;
    public Transform LegsLeft;
    public float radius = 1f;
    public LayerMask groundMask;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var isWallRight = Physics2D.OverlapCircle(LegsRight.position, radius, groundMask);
        var isWallLeft = Physics2D.OverlapCircle(LegsLeft.position, radius, groundMask);
        if (isWallRight)
        {
            speed = -0.2f;
            
        }
        if (isWallLeft)
        {
            speed = 0.2f;
        }
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(speed,0);
    }
    private void OnDrawGizmos()
    {
        if (LegsLeft != null)
        {
            Gizmos.DrawWireSphere(LegsLeft.position, radius);
            Gizmos.DrawWireSphere(LegsRight.position, radius);
        }

    }
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                var health = other.gameObject.GetComponent<Heart>();
                if (health != null)
                {
                    health.TakeDamage(1);
                }
                Died();
            }
            if(other.gameObject.CompareTag("Bullet"))
            {
                Died();
            }
        }
    private void Died()
    {
        Destroy(gameObject);
    }
}
