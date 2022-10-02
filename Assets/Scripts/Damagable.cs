using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using MoreMountains.Feedbacks;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    public int health;
    public bool dying = false;
    public List<MonoBehaviour> scriptsToDisableOnDeath;
    public MMF_Player takeDamageFx;
    public MMF_Player dieFx;

    public bool givesXpToPlayer;
    public int xpAmount = 1;
    public bool deathFailsGame;
    public void Damage(int amount)
    {
        if(dying) return;
        if (amount >= health)
        {
            Die();
            return;
        }
        takeDamageFx.PlayFeedbacks();
        health -= amount;
    }

    private void Die()
    {
        dying = true;
        foreach (var s in scriptsToDisableOnDeath)
        {
            s.enabled = false;
        }
        dieFx.PlayFeedbacks();
        if (givesXpToPlayer)
        {
            GameActions.PlayerEarnXp(xpAmount);
        }

        if (deathFailsGame)
        {
            GameManager.I.Lose();
        }
        Destroy(gameObject,.5f);
    }
}
