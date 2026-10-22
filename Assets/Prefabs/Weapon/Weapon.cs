using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public string slotTages;
    public int Damage=1;
    public float AttackRateMult=1.0f;
    public AnimatorOverrideController animatorOverideController;
    public AimComponent aimComponent;
    public AudioClip audioClip;

    internal AudioSource audioSource;
    private GameObject owner;
    public GameObject Owner
    {
        get { return owner; }
        set { }
    }

    // Start is called before the first frame update
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="owner"></param>
    public void Init(GameObject owner)
    {
        this.owner = owner;
        audioSource = this.GetComponent<AudioSource>();
        UnEquip();
    }
    
    public string  GetAttackSlotTages()
    {
        return slotTages;
    }
    /// <summary>
    /// 装备武器自身
    /// </summary>
    public void Equip()
    {
        this.gameObject.SetActive(true);
        Owner.GetComponent<Animator>().runtimeAnimatorController = animatorOverideController;
        Owner.GetComponent<Animator>().SetFloat("AttackRateMult", AttackRateMult);

    }
    /// <summary>
    /// 卸载武器自身
    /// </summary>
    public void UnEquip()
    {
        this.gameObject.SetActive(false);
    }
    /// <summary>
    /// 攻击
    /// </summary>
    public abstract void Attack();
    /// <summary>
    /// 伤害减血
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="damage"></param>
    public void DamageGameobject(GameObject obj,int damage)
    {
        //传进来的对象有血量，就减血
        HealthComponent healthComponent = obj.GetComponent<HealthComponent>();
        if (healthComponent!=null)
        {
            healthComponent.ChangeHealth(-damage, Owner);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
