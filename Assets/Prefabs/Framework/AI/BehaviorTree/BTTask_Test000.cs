using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_Test000 : BTNode
{
    protected override E_NodeResult Excute()
    {
        return E_NodeResult.Inprogress;
    }

    protected override E_NodeResult Update()
    {
        //return E_NodeResult.Failure;
        return E_NodeResult.Success;
        //return E_NodeResult.Inprogress;
    }
}
