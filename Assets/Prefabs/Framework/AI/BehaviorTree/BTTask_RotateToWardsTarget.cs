using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制转向目标的节点
/// </summary>
public class BTTask_RotateToWardsTarget : BTNode
{
    GameObject target;
    float acceptDegress;
    BehaviorTree tree;
    string targetkey;
    Blackboard blackboard;
    IBehaviorTreeInterface behaviorTreeInterface;
    public BTTask_RotateToWardsTarget(BehaviorTree tree, string targetkey, float acceptDegress=20f)
    {
        this.tree = tree;
        this.targetkey = targetkey;
        this.acceptDegress = acceptDegress;
    }

    protected override E_NodeResult Excute()
    {
        if (tree == null || targetkey == null)
        {
            return E_NodeResult.Failure;
        }
        behaviorTreeInterface = tree.GetbehaviorTreeInterface();
        blackboard = tree.BlackBoard;
        if (blackboard == null|| behaviorTreeInterface==null)
        {
            return E_NodeResult.Failure;
        }
        blackboard.onBlackboardvalueChange -= BlackboardValueChange;
        blackboard.onBlackboardvalueChange += BlackboardValueChange;
        blackboard.GetBlackbaordData(targetkey, out target);
        if (target == null)
        {
            return E_NodeResult.Failure;
        }

        if (IsTargetInAcceptableacceptDegress())
        {
            return E_NodeResult.Success;
        }
        return E_NodeResult.Inprogress;

    }
    /// <summary>
    /// 观察者：目标变化时执行
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    private void BlackboardValueChange(string key, object value)
    {
        if (key != targetkey)
        {
            return;
        }
        blackboard.GetBlackbaordData(targetkey, out target);
    }

    protected override E_NodeResult Update()
    {
        if (target==null)
        {
            return E_NodeResult.Failure;
        }
        behaviorTreeInterface.RotateToWaords(target);
        if (IsTargetInAcceptableacceptDegress())
        {
            return E_NodeResult.Success;
        }

        return E_NodeResult.Inprogress;
    }

    /// <summary>
    /// 判断是否足够朝向目标
    /// </summary>
    /// <returns></returns>
    private bool IsTargetInAcceptableacceptDegress()
    {
        float delt = Vector3.Angle(tree.transform.forward,target.transform.position-tree.transform.position);
        if (delt< acceptDegress)
        {
            return true;
        }
        return false;
    }
}
