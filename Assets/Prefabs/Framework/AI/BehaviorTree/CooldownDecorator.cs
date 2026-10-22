using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownDecorator : Decorator
{
    private float cooldownTime;
    private float lastExcuteTime=0f;
    public CooldownDecorator(BTNode child, float cooldownTime=1.5f) : base(child)
    {
        this.cooldownTime = cooldownTime;
    }
    protected override E_NodeResult Excute()
    {
        if (cooldownTime<=0)
        {
            return E_NodeResult.Inprogress;
        }
        if ((Time.timeSinceLevelLoad- lastExcuteTime)>cooldownTime)
        {
            return E_NodeResult.Inprogress;
            lastExcuteTime = Time.timeSinceLevelLoad;
        }
        return E_NodeResult.Failure;
    }

}
