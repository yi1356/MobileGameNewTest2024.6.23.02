using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test000 : MonoBehaviour
{
    private PerceptionComponent perceptionComponent;
    // Start is called before the first frame update
    void Start()
    {
        perceptionComponent = this.GetComponent<PerceptionComponent>();
        perceptionComponent.onperceptionTargetChange += Testfind;
    }

    private void Testfind(PerceptionStimuli targetStimuli, bool isfound)
    {
        if (isfound)
        {
            //Debug.Log(targetStimuli.name+"Ω¯»Îº‡≤‚");
        }
        else
        {
            //Debug.Log("Õ—¿Îº‡≤‚");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
