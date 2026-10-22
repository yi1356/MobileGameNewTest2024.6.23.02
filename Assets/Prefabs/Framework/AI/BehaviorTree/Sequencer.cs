using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequencer : ComPositor
{
    /// <summary>
    /// 重写每帧节点逻辑
    /// </summary>
    /// <returns></returns>
    protected override E_NodeResult Update()
    {
        E_NodeResult result = GetCurrentChild().UpdateNode();
        //Get.UpdateNode();
        if (result == E_NodeResult.Failure)
        {
            return result;
        }
        if (result == E_NodeResult.Success)
        {
            if (Next())
            {
                currentNode = currentNode.Next;
                //Debug.Log("测试77777777777777777777777777777---------------------------------------------------------Sequencer.currentNode" + currentNode.Value);
                // Get();
                return E_NodeResult.Inprogress;
            }
            return E_NodeResult.Success;

        }
        return E_NodeResult.Inprogress;
    }

    public override BTNode Get()
    {
       
        if (currentNode != null)
        {
            Debug.Log("测试5555555555555555555555Sequer得到运行的子对象");
        }
        else
        {
            Debug.Log("测试5555555555555555555555Sequer没有得到运行的子对象");
        }
        return base.Get();
       
    }
}
