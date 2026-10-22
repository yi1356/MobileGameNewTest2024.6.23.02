using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 存储怪物所需要的数据
/// </summary>
public class Blackboard 
{
    Dictionary<string, object> blackboard = new Dictionary<string, object>();

    public delegate void OnBlackboardvalueChange(string key,object  value);
    public event OnBlackboardvalueChange onBlackboardvalueChange;
    /// <summary>
    /// 向其中添加数据
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void SetOrAddData(string key, object value)
    {
        if (blackboard.ContainsKey(key))
        {
            blackboard[key] = value;
            //onBlackboardvalueChange?.Invoke(key, value);
        }
        else
        {
            //onBlackboardvalueChange?.Invoke(key,value);
            blackboard.Add(key,value);
        }
        //TODO:测试
        if (key == "Target")
        {
            Debug.Log("Target已经保存");
        }

        if (key == "LastPoint")
        {
            Debug.Log("LastPoint已经保存");
        }
        onBlackboardvalueChange?.Invoke(key, value);
    }

    /// <summary>
    /// 获取得到数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool GetBlackbaordData<T>(string key,out T value)
    {

        value = default(T);
        if (blackboard.ContainsKey(key))
        {
            value = (T)blackboard[key];//TODO:注意这里不能用大写的Object
            return true;
        }
        return false;
    }

    /// <summary>
    /// 
    /// 移除数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    public void RemoveBlackbaordData(string key)
    {
        if (key == "Target")
        {
            Debug.Log("Target已经移除");
        }

        if (key == "LastPoint")
        {
            Debug.Log("LastPoint已经移除");
        }

        if (blackboard.ContainsKey(key))
        {
            blackboard.Remove(key);
        }
        onBlackboardvalueChange?.Invoke(key, null);
    }

    /// <summary>
    /// 判断有无数据
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool HasKey(string key)
    {
        if (blackboard.ContainsKey(key))
        {
            return true;
        }
        return false;
    }
}
