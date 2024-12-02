using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;

    private Vector3 offset;
    public float speed = 10;

    [Range(0f, 1f)]
    public float smoothness = 0.3f;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        var targetPosition = Vector3.MoveTowards(transform.position, target.position + offset, speed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, targetPosition, 1 - smoothness);
       
    }
}
