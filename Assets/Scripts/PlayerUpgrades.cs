using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    public static PlayerUpgrades I;
    public int dmgAdded = 0;
    public int speedAdded = 0;
    public int bulletSizeAdded = 0;
    private int skillPts = 0;
    
    
    public int startDmgVal = 5;
    public int startSpdVal = 6;
    public int startSizeVal = 10;

    public int dmgPer = 1;
    public int spdPer = 2;
    public int sizePer = 2;
    public TextMeshProUGUI dmgVal;
    public TextMeshProUGUI spdVal;
    public TextMeshProUGUI sizeVal;

    public TextMeshProUGUI coinTMP;

    public MMF_Player purchaseFx;
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

        skillPts = 1;
        coinTMP.text = "COINS: " + skillPts.ToString();
        dmgVal.text = startDmgVal.ToString();
        spdVal.text = startSpdVal.ToString();
        sizeVal.text = startSizeVal.ToString();
    }

    private void OnEnable()
    {
        GameActions.onPlayerLevelUp += AddSkillPoint;
    }
    private void OnDisable()
    {
        GameActions.onPlayerLevelUp -= AddSkillPoint;
    }

    private void AddSkillPoint()
    {
        skillPts++;
    }

    public void GetDamage()
    {
        if (skillPts > 0)
        {
            purchaseFx.PlayFeedbacks();
            skillPts--;
            dmgAdded++;
            dmgVal.text = (startDmgVal+dmgPer*dmgAdded).ToString();
            spdVal.text =(startSpdVal+spdPer*speedAdded).ToString();
            sizeVal.text = (startSizeVal+sizePer*bulletSizeAdded).ToString();
        }
        coinTMP.text = "COINS: " + skillPts.ToString();
    }
    public void GetSpeed()
    {
        if (skillPts > 0)
        {
            purchaseFx.PlayFeedbacks();
            skillPts--;
            speedAdded++;
            
            dmgVal.text = (startDmgVal+dmgPer*dmgAdded).ToString();
            spdVal.text =(startSpdVal+spdPer*speedAdded).ToString();
            sizeVal.text = (startSizeVal+sizePer*bulletSizeAdded).ToString();
        }
        coinTMP.text = "COINS: " + skillPts.ToString();
    }
    public void GetBulletSize()
    {
        if (skillPts > 0)
        {
            purchaseFx.PlayFeedbacks();
            skillPts--;
            bulletSizeAdded++;
            dmgVal.text = (startDmgVal+dmgPer*dmgAdded).ToString();
            spdVal.text =(startSpdVal+spdPer*speedAdded).ToString();
            sizeVal.text = (startSizeVal+sizePer*bulletSizeAdded).ToString();
        }
        coinTMP.text = "COINS: " + skillPts.ToString();
    }
}
