using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spriter : Enemy
{
    public Projectile projectile;//≈Á…‰ŒÔ
    public Transform projectilePoint;
    private Vector3 destination;
    public override void Attack(GameObject target)
    {
        Ani.SetTrigger("Attack");
        destination = target.transform.position;
    }

    public void Shoot()
    {
        //∑¢…‰ª—Ê≈Á…‰ŒÔ
        Projectile proje=GameObject.Instantiate<Projectile>(projectile, projectilePoint.position, projectilePoint.rotation);
        proje.Launch(this.gameObject, destination);

    }
}
