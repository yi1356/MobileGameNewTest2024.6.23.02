using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_Group_Patrolling : BTTask_Group
{
    string targetkey;
    float acceptDistance;
    float coolTime;

    public BTTask_Group_Patrolling(BehaviorTree tree, string targetkey, float acceptDistance = 1.5f,float coolTime=2f) :base(tree)
    {
        this.targetkey = targetkey;
        this.acceptDistance = acceptDistance;
        this.coolTime = coolTime;
    }
    protected override void ConstructTree(out BTNode Root)
    {
        Sequencer patrolSeq = new Sequencer();
        BTTask_GetNextPatrolPoint patrolGetNextPoint = new BTTask_GetNextPatrolPoint(tree, targetkey);
        BTTask_MoveToLoc patrolMoveToLoc = new BTTask_MoveToLoc(tree, targetkey,acceptDistance);
        BTTask_Wait patrooltCool = new BTTask_Wait(coolTime);
        patrolSeq.AddChild(patrolGetNextPoint);
        patrolSeq.AddChild(patrolMoveToLoc);
        patrolSeq.AddChild(patrooltCool);

        Root = patrolSeq;
    }
}
