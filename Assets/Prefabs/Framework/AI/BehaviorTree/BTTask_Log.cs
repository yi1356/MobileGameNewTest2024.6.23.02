using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_Log : BTNode
{
    protected override E_NodeResult Excute()
    {
        Debug.Log("≤‚ ‘1111111111111111111111111111111111111111111111111111111111Log");

        return E_NodeResult.Success;
    }
}
