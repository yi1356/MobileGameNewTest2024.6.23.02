using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 资源加载模块，同步/异步加载资源
/// </summary>
public class ResourcesMgr :SingletonManager<ResourcesMgr>
{
    /// <summary>
    /// 同步加载资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="pathName"></param>
    /// <returns></returns>
    public T Load<T>(string pathName) where T : Object
    {
        T res = Resources.Load<T>(pathName);
        if(res is GameObject)
        {
            return GameObject.Instantiate(res);
        }
        else
        {
            return res;
        }

    }
    //异步加载资源
    public void LoadAsyn<T>(string pathName,UnityAction<T> callback)where T:Object
    {
        MonoManager.Instance.StartCoroutine(ReallyLoadAsyn( pathName, callback));
    }
    //异步加载资源用到的协程
    IEnumerator ReallyLoadAsyn<T>(string pathName, UnityAction<T> callback) where T:Object
    {
        ResourceRequest r=Resources .LoadAsync<T>(pathName);
        yield return r;
        if (r.asset is GameObject)
        { callback.Invoke(GameObject.Instantiate(r.asset as T)); }
        else
        { callback.Invoke(r.asset as T); }
        
    }
}
