using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IteamInterface
{
    public ParticleSystem exploreVFX;
    public DamageComponent damage;
    public Rigidbody rigidbody;
    public float hight=4f;
    private IteamInterface iteamInterface;
    /// <summary>
    /// 喷射抛物线喷吐物
    /// </summary>
    /// <param name="instigation">发射者</param>
    /// <param name="Destination">目的地</param>
    public void Launch(GameObject instigation,Vector3 Destination)
    {
        if (instigation.GetComponent<IteamInterface>() != null)
        {
            iteamInterface = instigation.GetComponent<IteamInterface>();
            damage.SetTeamIterface(iteamInterface);
        }

        //计算应给予的速度
        float halfTime = Mathf.Sqrt(hight*2/Physics.gravity.magnitude);
        float Vh = Physics.gravity.magnitude * halfTime;
        float Vx = (instigation.transform.position - Destination).magnitude / 2 * halfTime;
        Vector3 Volicity = instigation.transform.forward * Vx + instigation.transform.up * Vh;
        rigidbody.AddForce(Volicity,ForceMode.VelocityChange);
    }

    public  int GetId() 
    {
        if (iteamInterface!=null)
        {
            return iteamInterface.GetId();
        }
        return -1;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (iteamInterface.GetRelationToward(other.gameObject)!=E_TeamRelation.teamate)
        {
            Explore();
        }
    }

    private void Explore()
    {
        //TODO:这里可以用对象池
        GameObject.Instantiate<ParticleSystem>(exploreVFX,this.transform.position,this.transform.rotation);
        Destroy(gameObject);
    }
}
