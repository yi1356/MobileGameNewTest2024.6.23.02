using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTTask_MoveToLoc :BTNode
{
    Vector3 target;
    float acceptDistance;
    BehaviorTree tree;
    string targetkey;
    Blackboard blackboard;
    NavMeshAgent agent;
    public BTTask_MoveToLoc(BehaviorTree tree, string targetkey, float acceptDistance = 1.5f)
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
        if (blackboard == null|| agent==null)
        {
            return E_NodeResult.Failure;
        }
        if (!blackboard.GetBlackbaordData(targetkey, out target)||target == null)
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
        agent.SetDestination(target);
        if (IsTargetInAcceptableDistance())
        {
            return E_NodeResult.Success;
        }
        return E_NodeResult.Inprogress;
    }

    /// <summary>
    /// ÅÐ¶Ï¾àÀëÊÇ·ñ¹»½ü
    /// </summary>
    /// <returns></returns>
    private bool IsTargetInAcceptableDistance()
    {
        if (Vector3.Distance(target, tree.transform.position) < acceptDistance)
        {
            return true;
        }
        return false;
    }

    protected override void End()
    {
        base.End();
        agent.isStopped = true;
    }
}
