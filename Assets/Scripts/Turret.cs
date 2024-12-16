using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float lifeTime = 1;
    public GameObject bulletPrefs;
    public Transform muzzle;

    private void Start()
    {
        InvokeRepeating("Shoot", 1, lifeTime);
    }
    
    void Shoot()
    {
        var bullet = Instantiate(bulletPrefs, muzzle.position, Quaternion.identity).GetComponent<Bullet>();
        var direction = (muzzle.position - transform.position).normalized;
        bullet.direction.x = -1;
        print("Works");
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
