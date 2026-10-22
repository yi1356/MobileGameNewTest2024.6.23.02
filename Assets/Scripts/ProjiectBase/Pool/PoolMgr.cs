using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 抽屉数据，缓存池容器中的一列容器
/// 属性:抽屉的父对象，列表容器存储对象
/// 方法：构造函数，取出，放回方法
/// </summary>
public class PoolData 
{
    public GameObject fatherObj;//父对象
    public List<GameObject> poolList;//对象的容器
    public PoolData(GameObject obj,GameObject poolObj)//这个构造函数的主要作用是创建这个抽屉的父物体，并将这个抽屉的父物体设置为对象池
    {
        fatherObj = new GameObject(obj.name);
        fatherObj.transform.parent = poolObj.transform;

        poolList = new List<GameObject>();
        PushObj(obj);
    }
    /// <summary>
    /// 向缓存池的抽屉里面放东西
    /// </summary>
    /// <param name="name"></param>
    /// <param name="obj"></param>
    public void PushObj(GameObject obj)
    {     
        poolList.Add(obj); //存起来
        obj.transform.SetParent(fatherObj.transform);  //设置父对象
        obj.SetActive(false);//失活隐藏
    }

    /// <summary>
    /// 从抽屉中取出物品的方法
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject GetObj()
    {
        GameObject obj = null;
        obj = poolList[0];
        poolList.RemoveAt(0);
        obj.SetActive(true);
        //断开父子关系
        obj.transform.parent = null;
        return obj;
    }
}

/// <summary>
/// 缓存池模块
/// </summary>
/// 用到的知识：
/// 1、Dictionary List
/// 2、GameObject和Resources中的API
public class PoolMgr : SingletonManager<PoolMgr>
{
    private GameObject poolObj;
    //容器
    public Dictionary<string, PoolData> poolDic = new Dictionary<string, PoolData>();
    /// <summary>
    /// 取出物品的方法
    /// </summary>
    /// <param name="name">资源名</param>
    /// <param name="callback">资源加载完成后，要执行什么操作</param>
    public void GetObj(string name,UnityAction<GameObject> callback)
    {
        GameObject obj = null;
        //有抽屉 并且抽屉中有东西
        if (poolDic.ContainsKey(name)&& poolDic[name].poolList.Count >0)
        {
            obj = poolDic[name].GetObj();
            callback(obj);
        }
        else//否则就加载生成目标物品
        {
            ResourcesMgr.Instance.LoadAsyn<GameObject>(name,(obj)=> 
            {
                obj.name = name;//改名
                callback(obj);
            });
        }
     
    }

    /// <summary>
    /// 向缓存池中放入物品的方法
    /// </summary>
    /// <param name="name"></param>
    /// <param name="obj"></param>
    public void PushObj(string name,GameObject obj)
    {
        //作为父对象的子物体
        if (poolObj==null) { poolObj = new GameObject("pool"); }
        obj.SetActive(false);
        //有抽屉
        if (poolDic.ContainsKey(name) )
        {
            poolDic[name].PushObj(obj);
        }
        else//没有抽屉
        {
            poolDic.Add(name,new PoolData(obj,poolObj));
        }
      
    }

    /// <summary>
    /// 清空缓存池，主要用于过场景时
    /// </summary>
    public void Clear()
    {
        poolDic.Clear();
    }
}
