using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTTask_Group : BTNode
{
    protected  BTNode Root;
    protected BehaviorTree tree;
    public BTTask_Group(BehaviorTree tree)
    {
        this.tree = tree;
    }
    protected abstract void ConstructTree(out BTNode Root);
    protected override E_NodeResult Excute()
    {
        return E_NodeResult.Inprogress;
    }
    protected override E_NodeResult Update()
    {
        return Root.UpdateNode();
    }
    protected override void End()
    {
        Root.Abort();
        base.End();
    }
    public override void SortPriority(ref int priorityConter)
    {
        base.SortPriority(ref priorityConter);
        Root.SortPriority(ref priorityConter);

    }
    public override void Initlize()
    {
        base.Initlize();
        Debug.Log("≤‚ ‘111111111111111111111111111111111111111initlize“—æ≠÷¥––");
        ConstructTree(out Root);
        if (Root != null) { Root.Initlize(); }
       
    }
}
