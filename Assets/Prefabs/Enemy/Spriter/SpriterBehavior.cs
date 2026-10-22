using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriterBehavior : BehaviorTree
{
    protected override void ConstructTree(out BTNode Root)
    {
        Selector selectRoot = new Selector();

        //攻击总节点
        BTTask_Group_AttackTarget atkBlackboard = new BTTask_Group_AttackTarget(this, "Target", 6f,3f);

        //最后记忆点节点
        BTTask_Group_MoveToLastSeenLoc lastPointSeq = new BTTask_Group_MoveToLastSeenLoc(this, "LastPoint", 1.5f, 3);

        //巡逻的总节点

        BTTask_Group_Patrolling patrolSeq = new BTTask_Group_Patrolling(this, "PatrolPoint", 1.5f, 2);

        selectRoot.AddChild(atkBlackboard);
        selectRoot.AddChild(lastPointSeq);
        selectRoot.AddChild(patrolSeq);
        Root = selectRoot;
    }
}
