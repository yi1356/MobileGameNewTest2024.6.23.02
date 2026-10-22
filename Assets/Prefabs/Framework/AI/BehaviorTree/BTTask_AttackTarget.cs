using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_AttackTarget : BTNode
{
    GameObject target;
    float acceptDegress;
    BehaviorTree tree;
    string targetkey;
    Blackboard blackboard;
    IBehaviorTreeInterface behaviorTreeInterface;
    public BTTask_AttackTarget(BehaviorTree tree, string targetkey)
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
        behaviorTreeInterface = tree.GetbehaviorTreeInterface();
        blackboard = tree.BlackBoard;
        if (blackboard == null || behaviorTreeInterface == null)
        {
            return E_NodeResult.Failure;
        }
        blackboard.GetBlackbaordData(targetkey, out target);
        if (target == null)
        {
            return E_NodeResult.Failure;
        }

        behaviorTreeInterface.Attack(target);
        return E_NodeResult.Success;

    }
}
