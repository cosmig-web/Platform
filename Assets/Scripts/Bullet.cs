using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject particles;
    public Vector2 direction;
    public float speed = 20;
    public float lifeTime = 2;
    public Vector2 damageRange = new Vector2(1, 2);
    
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("SelfDestruct", lifeTime);
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        //if(other.gameObject.CompareTag("Player")) return;
        
        //TODO: get health component
        var damage = 1;

         DamageIndicator.instance.ShowDamage((int)damage, transform.position);

        //print("Hit " + other.gameObject.name + " for " + damage + " damage");
        var health = other.gameObject.GetComponent<Heart>();
        if(health != null)
        {
            health.TakeDamage((int)damage);
        }
        SelfDestruct();

    }

    void SelfDestruct()
    {
        Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
