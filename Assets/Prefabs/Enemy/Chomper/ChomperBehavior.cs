using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperBehavior : BehaviorTree
{

    protected override void ConstructTree(out BTNode root)
    {
        Selector selectRoot = new Selector();

        //攻击总节点
        //Sequencer atkSeq = new Sequencer();
        //BTTaskMoveToTarget atkMoveToTarget = new BTTaskMoveToTarget(this,"Target",1.5f);
        //BTTask_AttackTarget atkAttack = new BTTask_AttackTarget(this, "Target");
        //BTTask_Wait atkCool = new BTTask_Wait(1f);
        //atkSeq.AddChild(atkMoveToTarget);
        //atkSeq.AddChild(atkAttack);
        //atkSeq.AddChild(atkCool);
        //BlackboardDecorator atkBlackboard = new BlackboardDecorator(atkSeq,this, "Target",E_RunCondition.KeyExit,E_NotifyRule.RunConditionChange,E_NotigyAbort.AbortBoth);
        BTTask_Group_AttackTarget atkBlackboard = new BTTask_Group_AttackTarget(this, "Target", 1.5f);

        //最后记忆点节点
        //Sequencer lastPointSeq = new Sequencer();
        //BTTask_MoveToLoc lastMoveToLoc = new BTTask_MoveToLoc(this, "LastPoint");
        //BTTask_Wait lastCool = new BTTask_Wait(3f);
        //BTTask_RemoveBlackBoardData lastRemove = new BTTask_RemoveBlackBoardData(this, "LastPoint");
        //lastPointSeq.AddChild(lastMoveToLoc);
        //lastPointSeq.AddChild(lastCool);
        //lastPointSeq.AddChild(lastRemove);
        BTTask_Group_MoveToLastSeenLoc lastPointSeq = new BTTask_Group_MoveToLastSeenLoc(this, "LastPoint",1.5f,3);

        //巡逻的总节点
        //Sequencer patrolSeq = new Sequencer();
        //BTTask_GetNextPatrolPoint patrolGetNextPoint = new BTTask_GetNextPatrolPoint(this,"PatrolPoint");
        //BTTask_MoveToLoc patrolMoveToLoc = new BTTask_MoveToLoc(this, "PatrolPoint");
        //BTTask_Wait patrooltCool = new BTTask_Wait(3f);
        //patrolSeq.AddChild(patrolGetNextPoint);
        //patrolSeq.AddChild(patrolMoveToLoc);
        //patrolSeq.AddChild(patrooltCool);

        BTTask_Group_Patrolling patrolSeq = new BTTask_Group_Patrolling(this, "PatrolPoint",1.5f,2);

        selectRoot.AddChild(atkBlackboard);
        selectRoot.AddChild(lastPointSeq);
        selectRoot.AddChild(patrolSeq);




       root = selectRoot;
    }
}
