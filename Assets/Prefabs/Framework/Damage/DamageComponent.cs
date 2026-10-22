using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageComponent :MonoBehaviour,IteamInterface
{
    public bool DamageTeamate= false;
    public bool DamageEmeny = true;
    public bool DamageNeutrual = false;
    public GameObject teamInterface;

    IteamInterface iteamInterface;

    public  int GetId()
    {
        return iteamInterface.GetId();
    }
 
    /// <summary>
    /// 手动设置iteamInterface(其实就是发动攻击的角色Iteam)
    /// </summary>
    /// <param name="iteamInterface"></param>
    public void SetTeamIterface(IteamInterface iteamInterface)
    {
        this.iteamInterface = iteamInterface;
    }
    /// <summary>
    /// 是否应被攻击
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool ShouldDamage(GameObject other)
    {
        if (iteamInterface==null)
        {
            iteamInterface = teamInterface.GetComponent<IteamInterface>();
        }
        if (iteamInterface == null)
        {
            return false;
        }
        E_TeamRelation relation = iteamInterface.GetRelationToward(other);

        if(DamageTeamate&&relation == E_TeamRelation.teamate)
        {
            return true;
        }
        if (DamageEmeny && relation == E_TeamRelation.enemy)
        {
            return true;
        }
        if (DamageNeutrual && relation == E_TeamRelation.neutral)
        {
            return true;
        }

        return false;
    }
}
