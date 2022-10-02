using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnTouch : MonoBehaviour
{
    public int damageAmount = 5;
    public float radius = 1;
    private Collider2D[] cols;
    public LayerMask damagableLayer;

    private void Awake()
    {
        cols = new Collider2D[500];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Damagable damagable))
        {
            int hitCount = Physics2D.OverlapCircleNonAlloc(transform.position,radius, cols,damagableLayer);
            for (int i = 0; i < hitCount; i++)
            {
                if (cols[i].TryGetComponent(out Damagable d))
                {
                    d.Damage(damageAmount);
                }
            }
        }
    }
}
