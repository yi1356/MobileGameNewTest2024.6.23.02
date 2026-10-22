using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 接口，让大嘴怪和喷射怪继承，转向和攻击
/// </summary>
public interface IBehaviorTreeInterface 
{
    /// <summary>
    ///  转向功能
    /// </summary>
    /// <param name="target"></param>
    /// <param name="vertialAim">决定是否y=0</param>
    public void RotateToWaords(GameObject target, bool vertialAim = false);
    /// <summary>
    /// 攻击功能
    /// </summary>
    /// <param name="target"></param>
    public void Attack(GameObject target);
}
