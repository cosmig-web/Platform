using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Movement")]
    public Vector2 direction;
    public float speed = 20;
    public Vector2 damageRange = new Vector2(10, 20);

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = direction * speed;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        //if (other.gameObject.CompareTag("Player")) return;

        var damage = Random.Range(damageRange.x, damageRange.y);

        DamageIndicator.instance.ShowDamage((int)damage, transform.position);
        Destroy(gameObject);
    }
}
