using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_Group_MoveToLastSeenLoc : BTTask_Group
{
    string targetkey;
    float acceptDistance;
         float coolTime;
    public BTTask_Group_MoveToLastSeenLoc(BehaviorTree tree, string targetkey, float acceptDistance = 1.5f, float coolTime = 3f) :base(tree)
    {
        this.targetkey = targetkey;
        this.acceptDistance = acceptDistance;
        this.coolTime = coolTime;
    }
    protected override void ConstructTree(out BTNode Root)
    {
        Sequencer lastPointSeq = new Sequencer();
        BTTask_MoveToLoc lastMoveToLoc = new BTTask_MoveToLoc(tree, targetkey,acceptDistance);
        BTTask_Wait lastCool = new BTTask_Wait(coolTime);
        BTTask_RemoveBlackBoardData lastRemove = new BTTask_RemoveBlackBoardData(tree, targetkey);
        lastPointSeq.AddChild(lastMoveToLoc);
        lastPointSeq.AddChild(lastCool);
        lastPointSeq.AddChild(lastRemove);

        Root = lastPointSeq;
    }
}
