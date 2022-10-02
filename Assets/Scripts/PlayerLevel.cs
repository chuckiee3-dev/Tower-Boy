using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    public int currentLevel = 1;
    public AnimationCurve levelExpCurve;
    private int targetXp;
    private int currentXp;

    public int maxLevel = 10;

    public TextMeshProUGUI levelTMP;
    public Image levelFill;
    public MMF_Player levelUpFx;
    private void Awake()
    {
        currentLevel = 1;
        
        levelTMP.text = "LVL." + currentLevel;
        levelFill.fillAmount = 0;
        targetXp = Mathf.CeilToInt(levelExpCurve.Evaluate(currentLevel));
        
    }

    private void AddXp(int amount)
    {
        if(currentLevel == maxLevel) return;
        if (currentXp + amount >= targetXp)
        {
            if(currentLevel +1 != maxLevel){
                currentXp = currentXp + amount - targetXp;
            }
            else
            {
                currentXp = targetXp;
            }
            LevelUp();
        }
        else
        {
            currentXp += amount;
        }
        levelFill.fillAmount = Mathf.Clamp01(currentXp * 1f / targetXp);
    }

    private void LevelUp()
    {
        levelUpFx.PlayFeedbacks();
        currentLevel++;
        if (currentLevel > maxLevel)
        {
            return;
        }
        if(currentLevel != maxLevel){
            levelTMP.text = "LVL." + currentLevel;
        }
        else
        {
            levelTMP.text = "LVL. MAX!";
        }
        targetXp = Mathf.CeilToInt(levelExpCurve.Evaluate(currentLevel));
        GameActions.PlayerLevelUp();
    }

    private void OnEnable()
    {
        GameActions.onPlayerEarnXP += AddXp;
    }


    private void OnDisable()
    {
        GameActions.onPlayerEarnXP -= AddXp;
    }
}
