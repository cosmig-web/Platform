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
    public float dash = 20;
    public float dashDur = 0.2f;
    public float dashCool = 1f;

    [Header("Ground check")]
    public Transform Legs;
    public float radius = 0.2f;
    public LayerMask groundMask;

    [Header("Jump Mechanics")]
    public float coyoteTime = 0.2f;
    public float jumpBufferTime = 0.2f;
    public int maxJumps = 2;

    private float coyoteGround;
    private int remJumps;
    private float jumpBufferGround;
    private Rigidbody2D rb;
    private float horizontal;
    private bool isGround;
    private bool isDashing;
    private float dashTime;
    private float dashCoolTime;



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
            remJumps = maxJumps;
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

        
        if(jumpBufferGround > 0 && (coyoteGround > 0 || remJumps > 0))
        {
            jumpBufferGround = 0;

            var jumpForce = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y * rb.gravityScale);
            rb.velocity = new Vector2(rb.velocity.x * airControl, jumpForce);

            if (!isGround)
            {
                remJumps -= 1;
            }
        }
        if (Input.GetButtonDown("Dash") && dashCoolTime <= 0)
        {
            isDashing = true;
            dashTime = dashDur;
            dashCoolTime = dashCool;
        }

        //dashing
        if (isDashing)
        {
            if (dashTime > 0)
            {
                rb.velocity = new Vector2(dash * horizontal, rb.velocity.y);
                dashTime -= Time.deltaTime;
            }
            else
            {
                isDashing = false;
            }
        }
        dashCoolTime -= Time.deltaTime;
        
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        
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
