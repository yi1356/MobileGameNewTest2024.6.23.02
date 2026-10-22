using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName="level/leversp",menuName = "LeverManager")]
public class LeverManager : ScriptableObject
{
    public int MainemnuIndex = 0;
    public int FirstLevelIndex = 1;

    public delegate void OnLeverFinished();
    public static event OnLeverFinished onLeverFinished;
    /// <summary>
    /// 敌人消灭完毕，游戏结束
    /// </summary>
    public static void LevelFinished()
    {
        onLeverFinished?.Invoke();
    }

    /// <summary>
    /// 切换到主菜单/开始场景
    /// </summary>
    public void GoToMainMenu()
    {
        LoadSenceByIndx(MainemnuIndex);
    }

    /// <summary>
    /// 第一个单独的游戏场景（单独写一个主要是为了防止Bug）
    /// </summary>
    public void LoadFirstLevel()
    {
        LoadSenceByIndx(FirstLevelIndex);
    }

    private void LoadSenceByIndx(int Index)
    {
        MonoManager.Instance.StartCoroutine(LoadLevel(FirstLevelIndex));
    }
    IEnumerator LoadLevel(int Index)
    {
        AsyncOperation op= SceneManager.LoadSceneAsync(Index);
        op.allowSceneActivation = false;
        yield return op.isDone;
        op.allowSceneActivation = true;
    }

    public IEnumerator StartLoading(int Index)
    {
        float displayProgress = 0;
        float toProgress = 0;
        AsyncOperation op = SceneManager.LoadSceneAsync(Index);
        op.allowSceneActivation = false;
        if (op.progress < 0.9)
        {
            displayProgress += 0.01f;
            yield return new WaitForFixedUpdate();
        }
        //actionBake?.Invoke();
        toProgress = 1;
        if (displayProgress < toProgress)
        {
            displayProgress += 0.01f;
            yield return new WaitForFixedUpdate();
        }
        displayProgress = 1f;
        op.allowSceneActivation = true;
    }

}
