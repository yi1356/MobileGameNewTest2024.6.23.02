using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 装饰节点基类，作用是：能够中断节点，执行其它任务
/// </summary>
public class Decorator : BTNode
{
    BTNode child;
    public Decorator(BTNode child)
    {
        this.child = child;
    }

    public BTNode  GetChild()
    {
        return child;
    }

    protected override E_NodeResult Excute()
    {
        return E_NodeResult.Inprogress;
    }

    protected override E_NodeResult Update()
    {
       
        E_NodeResult A= child.UpdateNode();
        //Debug.Log("测试22222222222222222222222222222222blackbordDecorator" +A);
        return A;
    }
  
    protected override void End()
    {
        base.End();
        if (child != null) { child.Abort(); }
    }

    public override BTNode Get()
    {
        if (GetChild()==null)
        {
           
            return this;
        }
        else
        {
            //Debug.Log("测试6666666666666666666666BlackBord子对象不为空");
            return GetChild().Get();
        }
       
    }
    public override void SortPriority(ref int priorityConter)
    {
        base.SortPriority(ref priorityConter);
        if (child != null) { child.SortPriority(ref priorityConter); }
    }
    public override void Initlize()
    {
        base.Initlize();
        if (child != null) { child.Initlize(); }
    }
}
