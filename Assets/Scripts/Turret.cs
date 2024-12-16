using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float rapid = 1;
    public GameObject bulletPrefs;
    public Transform muzzle;

    void Start()
    {
        Invoke("Shoot", rapid);
    }

    void Shoot()
    {
        var bullet = Instantiate(bulletPrefs, muzzle.position, Quaternion.identity).GetComponent<Bullet>();
        var direction = (muzzle.position - transform.position).normalized;
        bullet.direction = -direction;
    }
}
