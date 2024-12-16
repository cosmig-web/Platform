using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public float speed = 1f;

    public TextMeshProUGUI text;

    public float lifeTime =  1f;
    void Start()
    {
        Destroy(gameObject, lifeTime);
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        transform.localPosition += Vector3.up * speed * Time.deltaTime;
    }

    public void SetText(int damage)
    {
        text.text = damage.ToString();
    }
}
