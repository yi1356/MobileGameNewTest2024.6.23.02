using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSense : SenseComp
{
    public HealthComponent healthComponent;
    public float forgetTime;


    Dictionary<PerceptionStimuli, Coroutine> HitForgettingRoutine=new Dictionary<PerceptionStimuli, Coroutine>();//遗忘字典(需要遗忘的刺激 加到里面)
    private void Start()
    {
        healthComponent.onTakeDamage += TookDamage;
    }

  

    protected override bool IsStimuliSensable(PerceptionStimuli stimuli)
    {
        return HitForgettingRoutine.ContainsKey(stimuli);
    }
    private void TookDamage(float health, float delta, float maxHealth, GameObject inStigator)
    {
        Debug.Log("受攻击记忆方法执行");
        PerceptionStimuli stimuli = inStigator.GetComponent<PerceptionStimuli>();
        if (stimuli!=null)
        {
            Debug.Log("射击到目标对象");
            //是否在记忆(遗忘字典)中
            if (HitForgettingRoutine.ContainsKey(stimuli)) //在 停止遗忘，重新加入记忆
            {
                StopCoroutine(HitForgettingRoutine[stimuli]);
                HitForgettingRoutine.Remove(stimuli);
                Coroutine a = StartCoroutine(ForgetStimuli(stimuli));
                HitForgettingRoutine.Add(stimuli,a);
            }
            else //不在 就加入记忆
            {
                Coroutine a = StartCoroutine(ForgetStimuli(stimuli));
                HitForgettingRoutine.Add(stimuli, a);
            }

        }
        else
        {
            Debug.Log("受攻击记忆方法执行且监测不到刺激组件");
        }
    }

    IEnumerator ForgetStimuli(PerceptionStimuli stimuli)
    {
        yield return new WaitForSeconds(forgetTime);
        HitForgettingRoutine.Remove(stimuli);
    }


}
