using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour,IRewardListenner
{
    public delegate void OnHealthChange(float health,float delta,float maxHealth);
    public delegate void OnTakeDamage(float health,float delta,float maxHealth,GameObject inStigator);
    public delegate void OnHealthEmpty(GameObject Killer);

    public event OnHealthChange onHealthChange;
    public event OnTakeDamage onTakeDamage;
    public event OnHealthEmpty onHealthEmpty;

    public float health = 100;
    public float maxHealth = 100;
    [Header("音效")]
    public AudioClip beHitAudio;
    public AudioClip DeathAudio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 改变血量
    /// </summary>
    /// <param name="amt"></param>
    /// <param name="inStigator"></param>
    public void ChangeHealth(float amt, GameObject inStigator=null)
    {
        //Debug.Log("测试33333333333333333333333333333333减血" + amt);
        if (amt < 0)
        {
            health += amt;
            if (health < 0) { health = 0; }
            GameplayerStatics.PlayOnLoc(beHitAudio,this.transform.position);
            onTakeDamage?.Invoke(health, amt, maxHealth, inStigator);
        }
        else
        {
            health += amt;
            if (health > maxHealth) { health = maxHealth; }
            onTakeDamage?.Invoke(health, amt, maxHealth, inStigator);
        }
        onHealthChange?.Invoke(health, amt, maxHealth);

        if (health == 0)
        {
            GameplayerStatics.PlayOnLoc(DeathAudio,this.transform.position);
            onHealthEmpty?.Invoke(inStigator);
        }
    }

    public void UpdateUIImmadietly()
    {
        onHealthChange?.Invoke(health,0, maxHealth);
    }

    public void Reward(Reward reward)
    {
        ChangeHealth(reward.healthReward);
    }
}
