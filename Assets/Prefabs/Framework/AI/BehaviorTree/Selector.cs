using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : ComPositor
{
    /// <summary>
    /// 重写每帧节点逻辑
    /// </summary>
    /// <returns></returns>
    protected override E_NodeResult Update()
    {
        E_NodeResult result = currentNode.Value.UpdateNode();
        if (result==E_NodeResult.Failure)
        {
            //Debug.Log("测试3333333333333333333333333333333Selector 子已经返回failue" + currentNode.Value.Get());
            if (Next())
            {
                currentNode = currentNode.Next;
                return E_NodeResult.Inprogress;
            }
            else
            {
                return E_NodeResult.Failure;
            }
           
        }
        if (result == E_NodeResult.Success)
        {
            return E_NodeResult.Success;

        }
        return E_NodeResult.Inprogress;
    }

    internal void AddChild()
    {
        throw new NotImplementedException();
    }
}
