using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 非常巧妙，利用一个空的接口，解决装箱拆箱问题
/// </summary>
public interface IEventInfo { }
public class EventInfo<T> : IEventInfo
{

    public UnityAction<T> actions;
    public EventInfo(UnityAction<T> action)
    {
        actions += action;
    }
}

public class EventInfo : IEventInfo
{

    public UnityAction actions;
    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}



/// <summary>
/// 事件中心模块
/// 知识：
/// 1、观察者模式
/// 2、Dictionary
/// 3、委托
/// </summary>
public class EventCenterManager :SingletonManager<EventCenterManager>
{
    // private Dictionary<string, UnityAction<object>> eventDic = new Dictionary<string, UnityAction<object>>();
    private Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();
    #region 添加/移除事件监听/触发(有参)
    public void AddEventListener<T>(string name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
        { (eventDic[name] as EventInfo<T>).actions += action; }
        else 
        { eventDic.Add(name,new EventInfo<T>(action)); }
    }
    public void RemoveEventListener<T>(string name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
        { (eventDic[name] as EventInfo<T>).actions-= action; }
    }
   
    /// <summary>
    /// 事件触发
    /// </summary>
    /// <param name="name">哪一个名字的事件触发了</param>
    public void EventTrigger<T>(string name, T info)
    {
        if (eventDic.ContainsKey(name))
        {
            if ((eventDic[name] as EventInfo<T>).actions != null)
            { (eventDic[name] as EventInfo<T>).actions.Invoke(info); }
        }
    }
    #endregion
    #region 添加/移除事件监听/触发(无参)

  
    public void AddEventListener(string name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
        { (eventDic[name] as EventInfo).actions += action; }
        else
        { eventDic.Add(name, new EventInfo(action)); }
    }
    public void RemoveEventListener(string name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
        { (eventDic[name] as EventInfo).actions -= action; }
    }
    /// <summary>
    /// 事件触发（无参）
    /// </summary>
    /// <param name="name">哪一个名字的事件触发了</param>
    public void EventTrigger(string name)
    {
        if (eventDic.ContainsKey(name))
        {
            if ((eventDic[name] as EventInfo).actions != null)
            { (eventDic[name] as EventInfo).actions.Invoke(); }
        }
    }
    #endregion
    //清空监听的事件，主要用在场景切换时
    public void Clear()
    {
        eventDic.Clear();
    }
}
