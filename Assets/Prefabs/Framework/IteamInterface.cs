using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_TeamRelation
{
    enemy,
    neutral,
    teamate
}
public interface IteamInterface
{
    /// <summary>
    /// 获取ID
    /// </summary>
    /// <returns></returns>
    public virtual int GetId() { return -1; }
    /// <summary>
    /// 判断本物体和传入物体的敌友关系
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public E_TeamRelation GetRelationToward(GameObject other)
    {
        IteamInterface iteamInterface=other.GetComponent<IteamInterface>();
        if (iteamInterface==null||GetId()==-1||iteamInterface.GetId()==-1)
        {
            return E_TeamRelation.neutral;
        }

        IteamInterface team = other.GetComponent<IteamInterface>();
        return team.GetId() == GetId() ? E_TeamRelation.teamate:E_TeamRelation.enemy;
    }
    
}
