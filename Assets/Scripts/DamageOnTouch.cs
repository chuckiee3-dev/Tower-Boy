using System;
using UnityEngine;

public class DamageOnTouch : PhasedActor
{
    public int damageAmount = 5;
    private bool _dealtDamage;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(_dealtDamage) return;
        if (other.TryGetComponent(out Damagable damagable) && !damagable.dying)
        {
            _dealtDamage = true;
            damagable.Damage(GetDamage());
            Destroy(gameObject);
        }
    }

    protected override void Act(Phase phase)
    {
        if (phase != Phase.War)
        {
            Destroy(gameObject,3);
        }
    }

    protected virtual int GetDamage()
    {
        return damageAmount;
    }
}
