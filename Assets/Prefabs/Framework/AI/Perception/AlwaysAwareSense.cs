using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysAwareSense : SenseComp
{
    public float senseRaduis = 1f;
    protected override bool IsStimuliSensable(PerceptionStimuli stimuli)
    {
        if (Vector3.Distance(stimuli.transform.position,this.transform.position)< senseRaduis)
        {
            return true;
        }
        return false;
    }
    public override void DrawFunc()
    {
        Gizmos.DrawWireSphere(transform.position, senseRaduis);
    }
}
