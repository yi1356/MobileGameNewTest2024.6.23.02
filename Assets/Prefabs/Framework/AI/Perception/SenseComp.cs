using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 各种感知的基类(画状态图辅助理解)
/// </summary>
public abstract class SenseComp : MonoBehaviour
{
    public float waitTime = 3f;
    static List<PerceptionStimuli> registeredStimuli=new List<PerceptionStimuli>();//注册的刺激


    List<PerceptionStimuli> perceptionStimuli=new List<PerceptionStimuli>();//目前检测到的刺激
    Dictionary<PerceptionStimuli, Coroutine> ForgettingRoutine=new Dictionary<PerceptionStimuli, Coroutine>();//遗忘字典(需要遗忘的刺激 加到里面)

    public delegate void OnPerceptionUpdated(PerceptionStimuli stimuli,bool isStimuli);
    public event OnPerceptionUpdated onPerceptionUpdated;

    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="stimuli"></param>
    public static void RegisterStimuli(PerceptionStimuli stimuli)
    {
       // Debug.Log("RegisterStimuli方法已经执行");
        if (!registeredStimuli.Contains(stimuli))
        {
            //Debug.Log("测试已经注册");
            registeredStimuli.Add(stimuli);
            //Debug.Log("注册者数量" + registeredStimuli.Count);
        }
    }

    /// <summary>
    /// 移除注册
    /// </summary>
    /// <param name="stimuli"></param>
    public static void RemoveStimuli(PerceptionStimuli stimuli)
    {
        if (registeredStimuli.Contains(stimuli))
        {
            registeredStimuli.Remove(stimuli);
        }
    }

    /// <summary>
    /// 检测是否正在被感知
    /// </summary>
    /// <returns></returns>
    protected abstract bool IsStimuliSensable(PerceptionStimuli stimuli);

    /// <summary>
    /// 遗忘协程
    /// </summary>
    /// <param name="stimuli"></param>
    /// <returns></returns>
    IEnumerator IForgettingRoutine(PerceptionStimuli stimuli)
    {
        yield return new WaitForSeconds(waitTime);
        if (perceptionStimuli.Count > 0)
        {
            perceptionStimuli.Remove(stimuli);
        }
        ForgettingRoutine.Remove(stimuli);
        onPerceptionUpdated?.Invoke(stimuli, false);
    }
    private void Update()
    {
        //检测注册的刺激是否正被感知到
        foreach (PerceptionStimuli item in registeredStimuli)
        {
            if (IsStimuliSensable(item))
            {
                if (!perceptionStimuli.Contains(item))
                {
                    perceptionStimuli.Add(item);
                    onPerceptionUpdated?.Invoke(item,true);
                }
                if (ForgettingRoutine.TryGetValue(item,out Coroutine coroutine))
                {
                    StopCoroutine(coroutine);
                    ForgettingRoutine.Remove(item);///???
                }
                
            }
            else
            {
                if (!perceptionStimuli.Contains(item))
                {
                    return;
                }
                if (!ForgettingRoutine.TryGetValue(item, out Coroutine coroutine))
                {
                     coroutine = StartCoroutine(IForgettingRoutine(item));
                    ForgettingRoutine.Add(item, coroutine);
                }
                  
            }
        }
    }
    /// <summary>
    /// 手动设置感知目标
    /// </summary>
    /// <param name="targetStimuli"></param>
    internal void AssignPercievedStimuli(PerceptionStimuli targetStimuli)
    {
        perceptionStimuli.Add(targetStimuli);
        onPerceptionUpdated?.Invoke(targetStimuli, true);

        Coroutine coroutine = StartCoroutine(IForgettingRoutine(targetStimuli));
        ForgettingRoutine.Add(targetStimuli, coroutine);
        //Debug.Log("测试66666666666666666666666666666666666666666666666666666666666------------------------------------已经手动设置了检测目标" + targetStimuli.name);
    }

    /// <summary>
    /// 画出范围
    /// </summary>
    public virtual void  DrawFunc() { }
    private void OnDrawGizmos()
    {
        DrawFunc();
    }
}
