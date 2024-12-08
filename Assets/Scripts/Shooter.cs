using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefs;
    public Transform muzzle;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            var direction = (mousePos - transform.position).normalized;

            var bullet = Instantiate(bulletPrefs, muzzle.position, Quaternion.identity).GetComponent<Bullet>();
            bullet.direction = direction;
        }
    }
}
