using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeDamager : PhasedActor
{
    public float attackCd = 2f;
    public int damage = 3;
    public float range = .45f;

    public Rigidbody2D rb;
    public float movementSpeed = 2.5f;
    
    private float attackTimer = 2f;
    private Transform target;
    private bool canAttack;
    private void Awake()
    {
        canAttack = true;
        
    }
    
    private void Update()
    {
        attackTimer += Time.deltaTime;
        
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            if ((target.position- transform.position ).magnitude > range)
            {
                rb.velocity = (target.position - transform.position).normalized * movementSpeed;
            }
            else
            {
                rb.velocity = Vector2.zero;
                Attack();
            }
        }
        else
        {
            target = EnemyTargetManager.I.GetClosestTargetPos(transform.position);
            if (target != null)
            {
                Debug.LogWarning("Set target to "+target.name);
            }
        }
    }

    private void Attack()
    {
        if (attackTimer >= attackCd)
        {
            canAttack = true;
        }
        if (canAttack)
        {
            canAttack = false;
            attackTimer = 0;
            Damagable d;
            if (target != null && target.TryGetComponent(out d) && !d.dying)
            {
                d.Damage(damage);
            }
        }
    }

    protected override void Act(Phase phase)
    {
        if (phase != Phase.War)
        {
            Destroy(gameObject);
        }
    }
}
