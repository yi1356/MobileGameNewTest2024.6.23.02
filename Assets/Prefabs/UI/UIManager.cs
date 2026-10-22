using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制整个游戏模块UI显隐
/// </summary>
public class UIManager : MonoBehaviour
{
    [Header("模块UI")]
    public CanvasGroup GamePlaterControl;
    public CanvasGroup winMenu;
    public CanvasGroup shopMenu;
    public CanvasGroup pauseMenu;
    public CanvasGroup deathMenu;
    public UIAudioPlayer uiAudioPlayer;
    CanvasGroup currentGroup;
    List<CanvasGroup> allChilds;

    private void Start()
    {
        allChilds = new List<CanvasGroup>();
        List<CanvasGroup> childs = new List<CanvasGroup>();
        GetComponentsInChildren(true, childs);
        for (int i=0;i<childs.Count;i++)
        {
            if (childs[i].transform.parent==this.transform)
            {
                allChilds.Add(childs[i]);
            }
        }
        SwitchToGamePlay();
        LeverManager.onLeverFinished += SwitchToWin;
    }
    //控制UI显示
    public void SwitchToGamePlay()
    {
        AllDisActive();
        SetGroupActive(GamePlaterControl,true,true);
        currentGroup = GamePlaterControl;
        GameplayerStatics.SetGamePause(false);
    }
    //胜利UI显示
    public void SwitchToWin()
    {
        AllDisActive();
        SetGroupActive(winMenu, true, true);
        currentGroup = winMenu;
        uiAudioPlayer.Playerwin();
        GameplayerStatics.SetGamePause(true);

    }
    //商店UI显示
    public void SwitchToShop()
    {
        AllDisActive();
        SetGroupActive(shopMenu, true, true);
        currentGroup = shopMenu;
        GameplayerStatics.SetGamePause(true);
    }
    //暂停UI显示
    public void SwitchToPause()
    {
        AllDisActive();
        SetGroupActive(pauseMenu, true, true);
        currentGroup = pauseMenu;
        GameplayerStatics.SetGamePause(true);
    }
    //死亡界面UI
    public void SwitchToDeath()
    {
        AllDisActive();
        SetGroupActive(deathMenu, true, true);
        currentGroup = deathMenu;
    }
    //设置某个UI是否可交互可显示
    private void SetGroupActive(CanvasGroup group,bool isInteract,bool isVisibal)
    {
        group.interactable = isInteract;
        group.blocksRaycasts = isInteract;
        group.alpha = isVisibal ? 1 : 0;
    }
    private void AllDisActive()
    {
        for (int i = 0; i < allChilds.Count; i++)
        {
            SetGroupActive(allChilds[i],false,false);
        }
    }
}
