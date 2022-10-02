using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using Unity.Mathematics;
using UnityEngine;

public class BulletBehaviour : DamageOnTouch
{
    public Rigidbody2D rb;
    public float speed = .5f;
    public MMF_Player spawnFX;

    private void Start()
    {
        spawnFX.PlayFeedbacks();
    }

    public void SetTarget(Vector3 pos)
    {
        Vector3 dir = pos - transform.position;
        Vector3 rot = transform.position - pos;

        rb.velocity = new Vector2(dir.x, dir.y).normalized * speed;
        float r = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;
        transform.rotation = quaternion.Euler(0,0, r+90);
    }

    protected override int GetDamage()
    {
        return damageAmount + Mathf.FloorToInt(PlayerUpgrades.I.dmgAdded *1.25f);
    }
}
