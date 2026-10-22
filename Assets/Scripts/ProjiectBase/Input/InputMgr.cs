using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 输入控制模块(PC)
/// 知识点：事件中心、input类
/// </summary>
public class InputMgr :SingletonManager<InputMgr>
{
    private bool isStart = false;
    /// <summary>
    /// 构造函数添加 Update
    /// </summary>
    public InputMgr()
    { 
        MonoManager.Instance.AddUpdateListener(MyUpdate);
    }

    void MyUpdate()//检测按键
    {
        if (!isStart) { return; }
        CheckKeyCode(KeyCode.W);
        CheckKeyCode(KeyCode.A);
        CheckKeyCode(KeyCode.S);
        CheckKeyCode(KeyCode.D);
       
    }
    /// <summary>
    /// 用来检测按键 抬起按下 分发事件的
    /// </summary>
    /// <param name="key"></param>
    void CheckKeyCode(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            EventCenterManager.Instance.EventTrigger<KeyCode>("按下按键", key);
        }
        if (Input.GetKeyUp(key))
        {
            EventCenterManager.Instance.EventTrigger<KeyCode>("抬起按键", key);
        }
    }
    public void StartOrEndCheck(bool isOpen)
    {
        isStart = isOpen;
    }
}
