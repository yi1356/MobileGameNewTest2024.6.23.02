using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Ó®ºÍÔÝÍ£¡¢Ê§°ÜUIÃæ°åÂß¼­
/// </summary>
public class InGameMenu : MonoBehaviour
{
    public UIManager uiManager;
    public LeverManager levelManager; 
    public Button btnRestart;
    public Button btnResum;
    public Button btnMainMenu;

    private void Start()
    {
        btnRestart.onClick.AddListener(RestartLevel);
        btnResum.onClick.AddListener(ResumGame);
        btnMainMenu.onClick.AddListener(BackToMainMenu);
    }

    private void BackToMainMenu()
    {
        levelManager.GoToMainMenu();
    }
    private void ResumGame()
    {
        GameplayerStatics.SetGamePause(false);
        uiManager.SwitchToGamePlay();
    }
    private void RestartLevel()
    {
        levelManager.LoadFirstLevel();
    }


}
