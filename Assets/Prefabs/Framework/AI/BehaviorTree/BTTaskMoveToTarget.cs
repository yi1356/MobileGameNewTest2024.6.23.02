using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTTaskMoveToTarget : BTNode
{
    GameObject target;
    float acceptDistance;
    BehaviorTree tree;
    string targetkey;
    Blackboard blackboard;
    NavMeshAgent agent;
    public BTTaskMoveToTarget(BehaviorTree tree, string targetkey, float acceptDistance = 1.5f)
    {
        this.tree = tree;
        this.targetkey = targetkey;
        this.acceptDistance = acceptDistance;
    }

    protected override E_NodeResult Excute()
    {
        if (tree == null || targetkey == null)
        {
            return E_NodeResult.Failure;
        }
        agent = tree.GetComponent<NavMeshAgent>();
        blackboard = tree.BlackBoard;
        if (blackboard == null || agent == null)
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

        if (IsTargetInAcceptableDistance())
        {
            return E_NodeResult.Success;
        }
        agent.isStopped = false;
        return E_NodeResult.Inprogress;

    }

    protected override E_NodeResult Update()
    {
        if (target == null) 
        {
            return E_NodeResult.Failure;
        }
        agent.SetDestination(target.transform.position);
        if (IsTargetInAcceptableDistance())
        {
            return E_NodeResult.Success;
        }
        return E_NodeResult.Inprogress;
    }

    /// <summary>
    /// 判断距离是否够近
    /// </summary>
    /// <returns></returns>
    private bool IsTargetInAcceptableDistance()
    {
        if (Vector3.Distance(target.transform.position,tree.transform.position)< acceptDistance)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 观察者：当目标发生变化时，执行这个方法更新目标
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    private void BlackboardValueChange(string key, object value)
    {
        if (key!=targetkey)
        {
            return;
        }
        blackboard.GetBlackbaordData(targetkey, out target);
    }

    protected override void End()
    {
        base.End();
        agent.isStopped = true;
    }
}
