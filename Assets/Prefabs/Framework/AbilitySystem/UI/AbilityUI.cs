using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 能力UI的逻辑
/// </summary>
public class AbilityUI : MonoBehaviour
{
    public Image abilityIcon;
    public Image CooldownWheel;
    public RectTransform offsetPivot;

    public float hightLightSize = 1.5f;
    public float hightOffset = 200f;
    public float scaleSpeed = 20f;
    Ability ability;

    Vector3 GoalScale = Vector3.one;
    Vector3 GoalOffset = Vector3.zero;
    float coolDownDuration = 2f;
    private void Start()
    {
        ability.onCoolDownStarted += StartCoolDown;
    }
    private void Update()
    {
        this.transform.localScale = Vector3.Lerp(this.transform.localScale, GoalScale, scaleSpeed * Time.deltaTime);
        offsetPivot.localPosition = Vector3.Lerp(offsetPivot.localPosition, GoalOffset, scaleSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="ability"></param>
    public void Init(Ability ability)
    {
        this.ability = ability;
        this.abilityIcon.sprite = ability.sprite;
    }
    /// <summary>
    /// 动效
    /// </summary>
    /// <param name="amt"></param>
    public void SetScaleAmt(float amt)
    {
        GoalScale =Vector3.one*(1+(hightLightSize-1)*amt);
        GoalOffset = Vector3.left* hightOffset*amt;
        //Debug.Log("测试333333333333333333+GoalScale" + GoalScale + "GoalOffset" + GoalOffset);
    }

    /// <summary>
    /// 激活技能
    /// </summary>
    public void ActivateAbility()
    {
        //Debug.Log("测试22222222222222AbilityUI已经激活");
        ability.ActivateAbility();
    }

    /// <summary>
    /// 观察者：冷却函数
    /// </summary>
    public void StartCoolDown()
    {
        StartCoroutine(CoolDownCoroutine());
    }

    IEnumerator CoolDownCoroutine()
    {
        coolDownDuration = ability.coolDownDuration;
        CooldownWheel.gameObject.SetActive(true);
        CooldownWheel.fillAmount = coolDownDuration / ability.coolDownDuration;
        while ((coolDownDuration / ability.coolDownDuration) > 0.01f)
        {
            CooldownWheel.fillAmount = coolDownDuration / ability.coolDownDuration;
            coolDownDuration -= Time.deltaTime;
            yield return null;
        }
        CooldownWheel.gameObject.SetActive(false);
        yield return null;
    }
}
