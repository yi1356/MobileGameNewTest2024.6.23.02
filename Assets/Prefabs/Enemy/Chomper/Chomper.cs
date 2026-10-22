using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chomper : Enemy
{
    public TriggerDamageComponent trgerDamage;
    public override void Attack(GameObject target)
    {
        Ani.SetTrigger("Attack");
    }

    public void AttackPoint()
    {
        //¹¥»÷¿ªÊ¼
        trgerDamage.SetDamageEnable(true);
    }
    public void AttackPointEnd()
    {
        //¹¥»÷½áÊø
        trgerDamage.SetDamageEnable(false);
    }
}
