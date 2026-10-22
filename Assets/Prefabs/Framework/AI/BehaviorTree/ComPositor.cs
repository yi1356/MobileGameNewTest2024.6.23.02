using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComPositor : BTNode
{

    LinkedList<BTNode> BTlist = new LinkedList<BTNode>();
    public LinkedListNode<BTNode> currentNode;
    /// <summary>
    /// 向复合节点中加入新节点
    /// </summary>
    /// <param name="item"></param>
    public void AddChild(BTNode item)
    {
        BTlist.AddLast(item);
    }
   /// <summary>
   /// 重写执行节点
   /// </summary>
   /// <returns></returns>
    protected override E_NodeResult Excute()
    {
        if (BTlist.Count == 0)
        {
            return E_NodeResult.Success;
        }
        currentNode = BTlist.First;
        return E_NodeResult.Inprogress;

    }
    /// <summary>
    /// 获取当下正在执行的子节点
    /// </summary>
    /// <returns></returns>
    public BTNode GetCurrentChild()
    {
        return currentNode.Value;
    }
    /// <summary>
    /// 判断是否有下一个子节点
    /// </summary>
    /// <returns></returns>
    protected bool Next()
    {
        if (currentNode.Next != null)
        {
            return true;
        }
        return false;
    }
    protected override void End()
    {
        //base.End();
        ////需要结束正执行的子节点的逻辑
        //if (currentNode != null)
        //{
        //    GetCurrentChild().Abort();
        //}
        //currentNode = null;

        if (currentNode == null)
            return;
        currentNode.Value.Abort();
        //currentNode = null;
    }

    public override BTNode Get()
    {
        if (currentNode==null)
        {
            return this;
        }
        return  GetCurrentChild();
    }
    public override void SortPriority(ref int priorityConter)
    {
        base.SortPriority(ref priorityConter);
        LinkedListNode<BTNode> currBTNode = BTlist.First;
        while (currBTNode!=null)
        {
            currBTNode.Value.SortPriority(ref priorityConter);
            currBTNode = currBTNode.Next;
        }

    }
    public override void Initlize()
    {
        base.Initlize();
        LinkedListNode<BTNode> currBTNode = BTlist.First;
        while (currBTNode != null)
        {
            currBTNode.Value.Initlize();
            currBTNode = currBTNode.Next;
        }
    }
}
