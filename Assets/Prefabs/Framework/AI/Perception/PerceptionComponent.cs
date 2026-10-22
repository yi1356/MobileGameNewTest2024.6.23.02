using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionComponent : MonoBehaviour
{
    public SenseComp[] senseComp;
    [Header("音效")]
    public AudioClip audioClip;

    private LinkedList<PerceptionStimuli> currentPerception=new LinkedList<PerceptionStimuli>();//存储所有感官检测到的刺激对象
    PerceptionStimuli targetStimuli;

    public delegate void OnperceptionTargetChange(PerceptionStimuli targetStimuli,bool isfound);
    public event OnperceptionTargetChange onperceptionTargetChange;
    private void Awake()
    {
        for (int i = 0; i < senseComp.Length; i++)
        {
            senseComp[i].onPerceptionUpdated += SenseUpdate;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    ///  将检测到的刺激加入列表，得到真正的目标
    /// </summary>
    /// <param name="stimuli"></param>
    /// <param name="isStimuli"></param>
    private void SenseUpdate(PerceptionStimuli stimuli, bool isStimuli)
    {
        //Debug.Log("测试7777777777777777777777777777777777777777777777777777777777777SenseUpdate执行isStimuli" + isStimuli);
        // 将检测到的刺激加入列表
        if (isStimuli)
        {
            LinkedListNode<PerceptionStimuli> intrimStimuli = currentPerception.Find(stimuli);
            if (intrimStimuli == null)
            {
                currentPerception.AddLast(stimuli);
            }
            else
            {
                currentPerception.AddAfter(intrimStimuli, stimuli);
            }
        }
        else
        {
            currentPerception.Remove(stimuli);
        }

        //得到真正的目标
        if (currentPerception.Count == 0)
        {
            //Debug.Log("测试9999999999999999999999999999999999999999999999999SenseUpdate999999999currentPerception.Count == 0");
            targetStimuli = null;
            //if (onperceptionTargetChange==null)
            //{
            //    Debug.Log("测试---------------------------------------onperceptionTargetChange==null");
            //}
            //else
            //{
            //    Debug.Log("测试---------------------------------------onperceptionTargetChange!=null");
            //}
            onperceptionTargetChange?.Invoke(targetStimuli, false);

        }
        else
        {
            //Debug.Log("测试1010101010101010101010101010100101010101010SenseUpdate10101001010101010currentPerception.Count ！= 0");
            PerceptionStimuli higLightStimuli = currentPerception.First.Value;
            if (higLightStimuli!=targetStimuli)
            {
                //Debug.Log("测试888888888888888888888888888888888888888888888888888888888888888888888SenseUpdate执行目标变化");
                GameplayerStatics.PlayOnLoc(audioClip,this.transform.position);
                targetStimuli = higLightStimuli;
                onperceptionTargetChange?.Invoke(targetStimuli, true);
            }
        }
    }
    /// <summary>
    /// 手动设置感知目标
    /// </summary>
    /// <param name="targetStimuli"></param>
    public void AssignPercievedStimuli(PerceptionStimuli targetStimuli)
    {
        //Debug.Log("测试5555555555555555555555PerceptionComponent的手动设置感知目标已经执行");
        senseComp[0].AssignPercievedStimuli(targetStimuli);
    }
    private void OnDrawGizmos()
    {
        if (targetStimuli != null)
        {
            Gizmos.DrawLine(transform.position, targetStimuli.transform.position);
        }
    }

}
