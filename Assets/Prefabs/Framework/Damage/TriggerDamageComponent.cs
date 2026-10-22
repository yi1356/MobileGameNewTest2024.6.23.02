using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamageComponent :DamageComponent
{
    public bool isStartEnabel = false;
    public BoxCollider trigger;
    public float damage;
    private void Start()
    {
        trigger.enabled = isStartEnabel;
    }
    /// <summary>
    /// 控制碰撞体的激活失活
    /// </summary>
    /// <param name="isEnable"></param>
    public void SetDamageEnable(bool isEnable)
    {
        trigger.enabled = isEnable;
    }
    /// <summary>
    /// 触发的方式攻击（有血条就减血）
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        HealthComponent healthComponent = other.GetComponent<HealthComponent>();
        if (healthComponent == null)
        {
            //Debug.Log("测试--------没有找到血量组件");
            return;
        }
        if (ShouldDamage(other.gameObject))
        {
           // Debug.Log("测试--------监测到攻击的是敌人");
            healthComponent.ChangeHealth(-damage,gameObject);
        }
    }
}
