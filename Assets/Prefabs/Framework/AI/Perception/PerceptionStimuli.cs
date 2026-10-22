using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionStimuli : MonoBehaviour
{
    private void Start()
    {
        //Debug.Log("方法调用");
        SenseComp.RegisterStimuli(this);
    }
    private void OnDestroy()
    {
        SenseComp.RemoveStimuli(this);
    }
}
