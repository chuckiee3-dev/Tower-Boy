using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhaseUI : MonoBehaviour
{
    private float phaseDuration = 10;
    private float remainingDuration = 10;
    private bool timeActive = false;

    public TextMeshProUGUI currentTitleTMP;
    public Image currentFill;
    public Canvas shop;
    public Canvas phaseCanvas;
    public Canvas startCanvas;
    public Canvas deadCanvas;
    public Canvas winCanvas;
    public Canvas controlsCanvas;
    public Image shopControls;
    public Image positionControls;
    public Image battleControls;
    private Phase lastPhase;
    private void OnEnable()
    {
        GameActions.onPhaseChanged += SetTitle;
        GameActions.onSetPhaseDuration += SetDuration;
        GameActions.onGameStateChanged += UpdateCanvases;
    }

    private void OnDisable()
    {
        GameActions.onPhaseChanged -= SetTitle;
        GameActions.onSetPhaseDuration -= SetDuration;
        GameActions.onGameStateChanged -= UpdateCanvases;
    }
    private void UpdateCanvases(GameState obj)
    {
        if (obj == GameState.Playing)
        {
            startCanvas.enabled = false;
            controlsCanvas.enabled = true;
            ShowShopControls();
            deadCanvas.enabled = false;
            winCanvas.enabled = false;
            SetTitle(lastPhase);
        }else if (obj == GameState.Finished)
        {
            controlsCanvas.enabled = false;
            winCanvas.enabled = true;
            timeActive = false;
        }else if (obj == GameState.Failed)
        {
            controlsCanvas.enabled = false;
            deadCanvas.enabled = true;
            timeActive = false;
        }
    }

    private void ShowShopControls()
    {
        shopControls.gameObject.SetActive(true);
        positionControls.gameObject.SetActive(false);
        battleControls.gameObject.SetActive(false);
    }
    private void ShowPosControls()
    {
        shopControls.gameObject.SetActive(false);
        positionControls.gameObject.SetActive(true);
        battleControls.gameObject.SetActive(false);
    }
    private void ShowBattleControls()
    {
        shopControls.gameObject.SetActive(false);
        positionControls.gameObject.SetActive(false);
        battleControls.gameObject.SetActive(true);
    }


    private void Awake()
    {
        shop.enabled = false;
        controlsCanvas.enabled = false;
    }


    private void SetTitle(Phase p)
    {
        lastPhase = p;
        if (GameManager.I.state != GameState.Playing)
        {
            shop.enabled = false;
            phaseCanvas.enabled = false;
            return;
        }
        remainingDuration = phaseDuration;
        string title = "Battle Phase";
        switch (p)
        {
            case Phase.Positioning:
                shop.enabled = false;
                phaseCanvas.enabled = true;
                ShowPosControls();
                title = "Positioning Phase";
                break;
            case Phase.War:
                shop.enabled = false;
                phaseCanvas.enabled = true;
                ShowBattleControls();
                title = "Attack Phase";
                break;
            case Phase.Build:
                title = "Shop Phase";
                ShowShopControls();
                shop.enabled = true;
                phaseCanvas.enabled = true;
                break;
            case Phase.NONE:
                phaseCanvas.enabled = false;
                shop.enabled = false;
                
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(p), p, null);
        }

        currentTitleTMP.text = title;
        remainingDuration = phaseDuration;
        currentFill.fillAmount = Mathf.Clamp01(remainingDuration / phaseDuration);
        
    }

    private void SetDuration(float duration)
    {
        remainingDuration = duration;
        phaseDuration = duration;
        timeActive = true;
    }

    private void Update()
    {
        if(GameManager.I.state != GameState.Playing) return;
        if(!timeActive) return;
        remainingDuration -= Time.deltaTime;
        currentFill.fillAmount = Mathf.Clamp01(remainingDuration / phaseDuration);
    }

}
