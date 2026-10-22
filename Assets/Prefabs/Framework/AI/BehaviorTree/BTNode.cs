using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_NodeResult
{ 
    Success,
    Failure,
    Inprogress
}

public abstract class BTNode
{
    private bool Isstarted = false;//标志这个节点是否在被执行
    /// <summary>
    /// 获取正在执行的节点的逻辑
    /// </summary>
    /// <returns></returns>
    public virtual BTNode Get()
    {
        return this;
    }
    /// <summary>
    /// 用于每帧更新此节点的逻辑
    /// </summary>
    public E_NodeResult UpdateNode()
    {
        if (!Isstarted)//如果Excute();的结果不是继续，就不能执行节点的每帧逻辑
        {
            Isstarted = true;
            E_NodeResult nodeResult = Excute();
            if (nodeResult!=E_NodeResult.Inprogress)
            {
                EndNode();
                return nodeResult;
            }
        }

        E_NodeResult nodeResultUP = Update();
        if (nodeResultUP!=E_NodeResult.Inprogress)
        {
            EndNode();
        }
        return nodeResultUP;
    }
    /// <summary>
    /// 节点开始时执行的逻辑(决定了节点是否能执行)
    /// </summary>
    protected virtual E_NodeResult Excute()
    {
        return E_NodeResult.Success;
    } 

    /// <summary>
    /// 节点的每帧的逻辑
    /// </summary>
    protected virtual E_NodeResult Update()
    {
        return E_NodeResult.Success;
    }
    /// <summary>
    /// 结束节点的逻辑
    /// </summary>
    public void EndNode()
    {
        Isstarted = false;
        End();
    }
    protected virtual void End()
    {

    }
    public void Abort()//用于外部调用结束这个节点
    {
        EndNode();
    }

    int priority;
    /// <summary>
    /// 为节点排序（结束部节点时会用到）
    /// </summary>
    /// <param name="priorityConter"></param>
    public  virtual void SortPriority(ref int priorityConter)
    {
        priorityConter++;
        this.priority = priorityConter;
        Debug.Log($"{this} has priorty {priority}");
        //Debug.Log($"{this} has priorty {priority}");
    }

    public int GetPriority()
    {
        return priority;
    }
    /// <summary>
    /// 初始化(终于知道它的作用了，是为了方便节点组能能更方便构造节点)
    /// </summary>
    public  virtual void  Initlize() { }

}
