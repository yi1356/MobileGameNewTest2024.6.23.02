using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Internal;
/// <summary>
/// 能够提供方法，使得没有继承Monobehavir的类也能够使用  update函数与协程
/// ֪
/// </summary>
public class MonoManager : SingletonManager<MonoManager>
{
    private MonoController controller;
   public MonoManager()//构造函数
    {
        GameObject obj = new GameObject("MonoManager");
        controller = obj.AddComponent<MonoController>();
    }
    #region 添加/移除帧更新事件
    public void AddUpdateListener(UnityAction func)
    {
        controller.AddupdateListener(func);
    }
    public void RemoveUpdateListener(UnityAction func)
    {
        controller.RemoveupdateListener(func);
    }
    #endregion
    #region 开启/关闭协程相关事件
    public Coroutine StartCoroutine(string methodName)
    {
        return controller.StartCoroutine(methodName);
    }
    public Coroutine StartCoroutine(IEnumerator routine)
    {
        return controller.StartCoroutine(routine);
    }
    public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
    {
        return controller.StartCoroutine(methodName, value);
    }
    public Coroutine StartCoroutine_Auto(IEnumerator routine)
    {
        return controller.StartCoroutine(routine);
    }
    #endregion
}
