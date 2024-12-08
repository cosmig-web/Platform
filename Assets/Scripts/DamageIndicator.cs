using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    public static DamageIndicator instance;

    public GameObject damageTextPrefs;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    public void ShowDamage(int damage, Vector3 position)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(position);

        var damagebox = Instantiate(damageTextPrefs, screenPos, Quaternion.identity, transform).GetComponent<DamageText>();

        damagebox.SetText(damage);
    }
}
