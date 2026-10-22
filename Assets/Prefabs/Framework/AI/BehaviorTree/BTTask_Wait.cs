using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_Wait : BTNode
{
    float timeElapsed = 0f;
    float waitTime=2f;
    public BTTask_Wait(float waitTime)
    {
        this.waitTime = waitTime;
    }
    protected override E_NodeResult Excute()
    {
        if (waitTime<=0)
        {
            return E_NodeResult.Success;
        }
        timeElapsed = 0;
        return E_NodeResult.Inprogress;
    }
    protected override E_NodeResult Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed>= waitTime)
        {
            //Debug.Log("BTTask_Wait----------µÈ´ýÍê³É");
            return E_NodeResult.Success;
        }
        return E_NodeResult.Inprogress;
    }
}
