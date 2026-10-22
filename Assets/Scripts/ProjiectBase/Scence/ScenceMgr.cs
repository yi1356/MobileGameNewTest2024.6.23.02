using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
/// <summary>
/// 场景切换模块
/// 知识点：场景异步加载、协程、委托
/// </summary>
public class ScenceMgr : SingletonManager<ScenceMgr>
{

    /// <summary>
    /// 场景同步加载
    /// </summary>
    /// <param name="name"></param>
    /// <param name="func"></param>
    public void LoadScence(string name,UnityAction func)
    {
        SceneManager.LoadScene(name);
        if (func!=null) { func.Invoke(); }
       
    }
    /// <summary>
    ///异步加载的方法
    /// </summary>
    /// <param name="name"></param>
    /// <param name="func"></param>
    public void LoadScenceAsyn(string name, UnityAction func)
    {
        MonoManager.Instance.StartCoroutine(ReallyLoadScence(name, func));
    }
    //异步加载场景用的协程
    IEnumerator ReallyLoadScence(string name, UnityAction func)
    {
       AsyncOperation ao= SceneManager.LoadSceneAsync(name);
        yield return ao;
        //TODO:这里用来更新进度条
        while (!ao.isDone)
        {
            EventCenterManager.Instance.EventTrigger <float>("进度条更新",ao.progress);
        }
        if (func != null) { func.Invoke(); }
    }
   
}
