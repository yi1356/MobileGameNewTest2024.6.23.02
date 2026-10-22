using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum E_RunCondition
{ 
    KeyExit,//键存在
    KeyNotExit,
    keyNotExists
}

public enum E_NotifyRule 
{ 
    RunConditionChange,//运行条件发生改变
    KeyChange
}

public enum E_NotigyAbort 
{ 
    AbortSelf,
    AbortLower,
    AbortBoth//终止等级低的和自身节点
}


public class BlackboardDecorator : Decorator
{
    BehaviorTree tree;
    string key;
    object target;
    Blackboard blackboard;

    E_RunCondition runCondition;
    E_NotifyRule notifyRule;
    E_NotigyAbort notigyAbort;

    public BlackboardDecorator(BTNode child,BehaviorTree tree,string key, E_RunCondition runCondition=E_RunCondition.KeyExit, E_NotifyRule notifyRule=E_NotifyRule.RunConditionChange, E_NotigyAbort notigyAbort=E_NotigyAbort.AbortBoth) :base (child)
    {
        this.tree = tree;
        this.key = key;
        this.runCondition = runCondition;
        this.runCondition = runCondition;
        this.notifyRule = notifyRule;
        this.notigyAbort = notigyAbort;

    }
    protected override E_NodeResult Excute()
    {
        //Debug.Log("测试88888888888888888888888888888888888888888——————111Blackbord.Excute()执行");
        #region
        if (tree == null)
        {
            return E_NodeResult.Failure;
        }
        blackboard = tree.BlackBoard;
        if (blackboard == null || key == null)
        {
            return E_NodeResult.Failure;
        }
        //if (blackboard.onBlackboardvalueChange)
        //{

        //}
        blackboard.onBlackboardvalueChange -= CheckNotify;
        blackboard.onBlackboardvalueChange += CheckNotify;

      

        if (!CheckRunCondition())
        {
            //Debug.Log("测试88888888888888888888888888888888888888888——————222Blackbord.Excute()执行");
            return E_NodeResult.Failure;
        }
        if (target == null)
        {
            //Debug.Log("测试88888888888888888888888888888888888888888——————333Blackbord.Excute()执行");
            return E_NodeResult.Failure;
           
        }
        //Debug.Log("测试88888888888888888888888888888888888888888——————444Blackbord.Excute()执行");
        return E_NodeResult.Inprogress;
        #endregion
    }
    /// <summary>
    /// 检查运行条件，目标是否存在
    /// </summary>
    /// <returns></returns>
    bool CheckRunCondition()
    {
        //bool exists = blackboard.GetBlackbaordData(key, out target);
        //if (runCondition == E_RunCondition.KeyExit)
        //{
        //    return exists;
        //}
        //return false;


        bool exists = tree.BlackBoard.GetBlackbaordData(key, out target);
        //Debug.Log("测试999999999999999999999999999999999CheckRunCondition"+ exists+ "target"+ target);
        switch (runCondition)
        {
            case E_RunCondition.KeyExit:
                return exists;
            case E_RunCondition.keyNotExists:
                return !exists;
        }
        return false;


    }

    /// <summary>
    /// 观察者，当目标有无的状态发生改变时，结束节点（both）
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    private void CheckNotify(string key, object value)
    {
        if (key != this.key) { return; }
        if (notifyRule == E_NotifyRule.RunConditionChange)
        {
            bool isTargetExit = value == null ? false : true;
            bool isPrevExit = target== null ? false : true;
            if (isTargetExit!=isPrevExit)
            {
                Debug.Log("测试----------------------目标状态发生改变");
                Notify();
            }
        }
    }

    /// <summary>
    /// 结束节点的方法（根据notigyAbort来决定）
    /// </summary>
    private void Notify()
    {
        switch (notigyAbort)
        {
            case E_NotigyAbort.AbortSelf:
                AbortSelf();
                break;
            case E_NotigyAbort.AbortLower:
                AbortLower();
                break;
            case E_NotigyAbort.AbortBoth:
                AbortBoth();
                break;
            default:
                break;
        }
    }

    private void AbortSelf()
    {
        Abort();
    }
    private void AbortLower()
    {
        tree.AbortLowerThan(GetPriority());
    }
    private void AbortBoth()
    {
        //Debug.Log("测试BlackBord节点已经结束Both");
        AbortLower();
        AbortSelf();
    }

}
