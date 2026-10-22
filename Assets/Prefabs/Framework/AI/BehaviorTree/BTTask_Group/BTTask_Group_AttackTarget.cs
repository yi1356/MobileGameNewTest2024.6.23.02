using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_Group_AttackTarget : BTTask_Group
{
    string targetkey;
    float acceptDistance;
    float coolTime;

    public BTTask_Group_AttackTarget(BehaviorTree tree,string targetkey, float acceptDistance = 1.5f, float coolTime = 1.5f,
        E_RunCondition runCondition=E_RunCondition.KeyExit, E_NotifyRule notifyRule=E_NotifyRule.RunConditionChange,
        E_NotigyAbort notigyAbort=E_NotigyAbort.AbortBoth) : base(tree)
    {
        this.targetkey = targetkey;
        this.acceptDistance = acceptDistance;
        this.coolTime = coolTime;
    }
    protected override void ConstructTree(out BTNode Root)
    {
        Sequencer atkSeq = new Sequencer();
        BTTaskMoveToTarget atkMoveToTarget = new BTTaskMoveToTarget(tree, targetkey, acceptDistance);
        BTTask_RotateToWardsTarget atkRoteToTarget = new BTTask_RotateToWardsTarget(tree, targetkey,15f);
        BTTask_AttackTarget atkAttack = new BTTask_AttackTarget(tree, targetkey);
        //BTTask_Wait atkCool = new BTTask_Wait(coolTime);
        CooldownDecorator atkCool = new CooldownDecorator(atkAttack,coolTime);
        atkSeq.AddChild(atkMoveToTarget);
        atkSeq.AddChild(atkRoteToTarget);
        atkSeq.AddChild(atkCool);
        BlackboardDecorator atkBlackboard = new BlackboardDecorator(atkSeq, tree, targetkey, E_RunCondition.KeyExit, E_NotifyRule.RunConditionChange, E_NotigyAbort.AbortBoth);

        Root = atkBlackboard;
    }
}
