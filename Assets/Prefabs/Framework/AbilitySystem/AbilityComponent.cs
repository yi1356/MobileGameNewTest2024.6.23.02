using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityComponent : MonoBehaviour,IPurchaseListenner,IRewardListenner
{
    public List<Ability> ImilitialAbilities;//一开始就配置的能力
    public float stamain = 100f;
    public float maxStamain = 100f;
    private float delt = 0f;


    List<Ability> abilities=new List<Ability>();//真正拥有的能力

    public delegate void OnAbilityAdd(Ability ability);
    public delegate void OnStaminUpdate(float stamain, float delt,float maxStamain);
    public event OnAbilityAdd onAbilityAdd;//能力增加事件
    public event OnStaminUpdate onStaminUpdate;//更新能量显示事件
        
    /// <summary>
    /// 在这里初始化
    /// </summary>
    void Start()
    {
        onStaminUpdate?.Invoke(stamain,delt,maxStamain);
        for (int i=0;i< ImilitialAbilities.Count;i++)
        {
            GiveAbility(ImilitialAbilities[i]);
        }
    }
    
    /// <summary>
    /// 增加能力
    /// </summary>
    /// <param name="ability"></param>
    public void GiveAbility(Ability ability)
    {
        abilities.Add(ability);
        ability.InitLize(this);
        onAbilityAdd?.Invoke(ability);
    }

    /// <summary>
    /// 激活能力
    /// </summary>
    /// <param name="ability"></param>
    public void ActiveAbility(Ability ability)
    {
        ability.ActivateAbility();
    }

    /// <summary>
    /// 用于判断能量是否足够激活能力
    /// </summary>
    /// <param name="staminToConsume"></param>
    /// <returns></returns>
    public bool TryConsumeStamin(float staminToConsume)
    {
        if (staminToConsume<=stamain)
        {
            delt = staminToConsume;
            stamain -= staminToConsume;
            onStaminUpdate?.Invoke(stamain, staminToConsume,maxStamain);
            delt = 0f;
            return true;
        }
        delt = 0f;
        return false;
    }

    public bool HandheldPurchase(Object purchaseItem)
    {
        //Debug.Log("测试666666666666666666666666666 AbilityComponent.HandheldPurchase已经执行purchaseItem" + purchaseItem.name);
        Ability Purchaseability = purchaseItem as Ability;
        if (Purchaseability!=null)
        {
            //Debug.Log("测试666666666666666666666666666 AbilityComponent.HandheldPurchase已经执行Purchaseability!=null");
            GiveAbility(Purchaseability);
            return true;
        }
        return false;
    }
    //**********************************************
    public void UpdateStaminUIImadietly()
    {
        onStaminUpdate?.Invoke(stamain,0, maxStamain);
    }
    public void Reward(Reward reward)
    {
        changStamin(reward.staminReward);
    }
    private void changStamin(float staminToConsume)
    {
        stamain += staminToConsume;
        stamain = Mathf.Clamp(stamain,0f,maxStamain);
        onStaminUpdate?.Invoke(stamain, staminToConsume, maxStamain);
    }
}
