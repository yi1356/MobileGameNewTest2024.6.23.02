using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 需要挂载的单例模式
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonMonoManager<T> : MonoBehaviour where T:SingletonMonoManager<T>
{
    private static T instance;
    public static T Instance
    {
        get { return instance;}
    }
    protected virtual void Awake()
    {
        if (instance != null)
        { Destroy(gameObject); }
        else
        { instance = (T)this; }
    }
    /// <summary>
    /// 这个是用来判断是不是已经删除了脚本（过场景/关闭等原因）
    /// </summary>
    public static  bool IsInitlized
    {
        get { return instance != null; }
    }
    private void OnDestroy()
    {
        if (instance== this) { instance = null;}
    }


}
