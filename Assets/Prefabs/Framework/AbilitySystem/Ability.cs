using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 能力配置基类（检查、激活、冷却）
/// </summary>
public abstract class Ability : ScriptableObject
{
    public Sprite sprite;
    public float StamminaToConSum = 50f;
    public float coolDownDuration=5f;
    public AudioClip audioClip;
    public AbilityComponent abilityComponent;

    private bool abilityOnCooldown = false;

    public delegate void OnCoolDownStarted();
    public event OnCoolDownStarted onCoolDownStarted;//开始冷却事件

    /// <summary>
    /// 初始化，赋予AbilityComponent
    /// </summary>
    /// <param name="abilityComponent"></param>
    public void InitLize(AbilityComponent abilityComponent)
    {
        this.abilityComponent = abilityComponent;
    }

    /// <summary>
    /// 激活能力的方法
    /// </summary>
    public abstract void ActivateAbility();

    /// <summary>
    /// 用于在能力激活前进行检查是否满足条件(是否在冷却状态)
    /// </summary>
    /// <returns></returns>
    protected bool CommitAbility()
    {
        if (abilityOnCooldown) return false;
        if (abilityComponent == null ||!abilityComponent.TryConsumeStamin(StamminaToConSum))
        {
            return false;
        }
        StartAbilityCoolDown();
        GameplayerStatics.PlayOnLoc(audioClip,Camera.main.transform.position);
        return true;
    }

    /// <summary>
    /// 开始冷却
    /// </summary>
    private void StartAbilityCoolDown()
    {
        onCoolDownStarted?.Invoke();
        abilityComponent.StartCoroutine(CoolDownStarted());
    }
    IEnumerator CoolDownStarted()
    {
        abilityOnCooldown = true;
        yield return new WaitForSeconds(coolDownDuration);
        abilityOnCooldown = false;
    }
}
