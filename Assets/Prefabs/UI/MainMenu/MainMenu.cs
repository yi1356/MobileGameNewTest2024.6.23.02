using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public LeverManager levelManager;
    public CanvasGroup panelMain;
    public CanvasGroup panelControl;
    public Button btnStart;
    public Button btnControls;
    public Button btnBack;

    private void Start()
    {
        btnStart.onClick.AddListener(BtnStart);
        btnControls.onClick.AddListener(BtnControls);
        btnBack.onClick.AddListener(BtnBack);
    }
    private void  BtnStart()
    {
        levelManager.LoadFirstLevel();
    } 
    private void BtnControls()
    {
        SetGroupActive(panelMain, false, false);
        SetGroupActive(panelControl, true, true);
       
    }

    private void BtnBack()
    {
        SetGroupActive(panelMain, true,true);
        SetGroupActive(panelControl, false, false);
    }
    private void SetGroupActive(CanvasGroup group, bool isInteract, bool isVisibal)
    {
        group.interactable = isInteract;
        group.blocksRaycasts = isInteract;
        group.alpha = isVisibal ? 1 : 0;
    }
}
