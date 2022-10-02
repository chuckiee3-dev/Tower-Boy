using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager I;
    [Header("SFX List")]
    
    public MMF_Player monsterHit;
    public MMF_Player monsterDead;
    public MMF_Player playerShootSmall;
    public MMF_Player playerShootBig;
    public MMF_Player playerTakeDamage;
    public MMF_Player playerLevelUp;
    
    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void PlayMonsterHitSfx(Vector3 pos)
    {
        
    }
}
