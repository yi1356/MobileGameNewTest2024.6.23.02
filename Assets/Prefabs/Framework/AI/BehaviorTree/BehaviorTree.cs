using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 行为树基类
/// </summary>
public abstract class BehaviorTree : MonoBehaviour
{
    public PatrollingComponent patrollingComponent;
    BTNode Root;
    Blackboard blackboard = new Blackboard();
    IBehaviorTreeInterface behaviorTreeInterface;
    public Blackboard BlackBoard
    {
        get{ return blackboard; }
    }
    // Start is called before the first frame update
    void Start()
    {
        behaviorTreeInterface = this.GetComponent<IBehaviorTreeInterface>();
        ConstructTree( out Root);//构造行为树，得到根节点
        int priorityConter = -1;
        Root.Initlize();
        Root.SortPriority(ref priorityConter);
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (blackboard.HasKey("Target"))
        //{
        //    Debug.Log("测试000000000000000000000000000Target------------存在");
        //}
        //else
        //{
        //    Debug.Log("测试000000000000000000000000000Target------------不存在");
        //}
        Root.UpdateNode();
    }

    /// <summary>
    /// 构造树的方法
    /// </summary>
    /// <param name="Root"></param>
    protected abstract void ConstructTree(out BTNode Root);
  

    /// <summary>
    /// 结束序号大于传入值的节点
    /// </summary>
    /// <param name="priority"></param>
    public void AbortLowerThan(int priority)
    {
       
        int interimPriority = Root.Get().GetPriority();
        if (priority< interimPriority)
        {
            Debug.Log("测试AbortLowerThan已经执行" + Root.Get());
            Debug.Log("传入的priority"+ priority);
            Debug.Log("结束的interimPriority" + interimPriority);

            Root.Abort();
        }
    }

    //*************************************************************************
    public IBehaviorTreeInterface GetbehaviorTreeInterface()
    {
        return behaviorTreeInterface;
    }
}
