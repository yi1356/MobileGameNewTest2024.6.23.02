using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightSense : SenseComp
{
    public float sightRange=3f;
    public float sightAngle = 50f;
    public float sightHight = 1f;

    Vector3 origion;
    protected override bool IsStimuliSensable(PerceptionStimuli stimuli)
    {
        bool isSense = true;
        //¾àÀë
        if (Vector3.Distance(stimuli.transform.position,transform.position)> sightRange)
        {
            isSense = false;
        }
        //½Ç¶È
        if (Vector3.Angle(transform.forward, (stimuli.transform.position - transform.position).normalized) > sightAngle)
        {
            isSense = false;
        }
        ////ÕÚµ²Îï
        origion = new Vector3(transform.position.x, transform.position.y + sightHight, transform.position.z);
        Vector3 stimuliDir = (stimuli.transform.position - transform.position).normalized;
        Physics.Raycast(origion, stimuliDir, out RaycastHit hit, 1000f);
        if (!hit.collider.transform.GetComponent<PerceptionStimuli>())
        {
            return false;
        }
        return isSense;
    }
    public override void DrawFunc()
    {

        Gizmos.DrawWireSphere(origion,sightRange);
        Vector3 LeftDir = Quaternion.AngleAxis(-sightAngle/2,Vector3.up)*transform.forward;
        Vector3 RightDir = Quaternion.AngleAxis(sightAngle/2,Vector3.up)*transform.forward;
        Gizmos.DrawLine(origion, origion + (LeftDir* sightRange));
        Gizmos.DrawLine(origion, origion+(RightDir * sightRange));
    }
}
