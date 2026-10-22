using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    public ParticleSystem BulletVfx;
    public override void Attack()
    {

        GameObject target = aimComponent.GetAimTarget(out Vector3 aimDir);
        if (target != null)
        {
            DamageGameobject(target, Damage);
        }
        audioSource.PlayOneShot(audioClip,1f);
        BulletVfx.transform.rotation = Quaternion.LookRotation(aimDir);
        BulletVfx.Emit(BulletVfx.emission.GetBurst(0).maxCount);//代码控制粒子发射+发射多少粒子（emission的count中设置）


        ////攻击逻辑：获取到对象，有Healthcomponent就减血
        //GameObject target = aimComponent.GetAimTarget(out Vector3 AimDir);
        //if (target!=null)
        //{
        //    //Debug.Log("检测到的对象为："+ target.name);
        //    DamageGameobject(target, Damage);
        //}
        ////发射粒子特效
        //BulletVfx.Emit(BulletVfx.emission.GetBurst(0).maxCount);
        ////声音
    }
}
