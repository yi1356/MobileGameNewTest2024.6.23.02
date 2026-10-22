using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_RemoveBlackBoardData :BTNode
{
    BehaviorTree tree;
    string targetkey;
    Blackboard blackboard;
    public BTTask_RemoveBlackBoardData(BehaviorTree tree, string targetkey)
    {
        this.tree = tree;
        this.targetkey = targetkey;
    }
    protected override E_NodeResult Excute()
    {
        if(tree == null || targetkey == null)
        {
            return E_NodeResult.Failure;
        }
        blackboard = tree.BlackBoard;
        if (blackboard == null)
        {
            return E_NodeResult.Failure;
        }
        if (!blackboard.HasKey(targetkey))
        {
            return E_NodeResult.Failure;
        }
        blackboard.RemoveBlackbaordData(targetkey);
        return E_NodeResult.Success;
    }
}
