using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 节点功能：获取下一个巡逻点
/// </summary>
public class BTTask_GetNextPatrolPoint : BTNode
{
    BehaviorTree tree;
    string targetkey;
    Blackboard blackboard;
    PatrollingComponent patrollingComponent;
    Vector3 nextPatrolPoint;
    public BTTask_GetNextPatrolPoint(BehaviorTree tree, string targetkey)
    {
        this.tree = tree;
        this.targetkey = targetkey;
    }

    protected override E_NodeResult Excute()
    {
        if (tree == null || targetkey == null)
        {
            return E_NodeResult.Failure;
        }
        blackboard = tree.BlackBoard;
        patrollingComponent = tree.patrollingComponent;
        if (blackboard==null || patrollingComponent==null|| patrollingComponent.patrollingPoints.Length==0)
        {
            return E_NodeResult.Failure;
        }
        if (patrollingComponent.GetNextPatrollingPoint(out nextPatrolPoint))
        {
            blackboard.SetOrAddData(targetkey, nextPatrolPoint);
        }
        else
        {
            return E_NodeResult.Failure;
        }
        return E_NodeResult.Success;
    }

}
