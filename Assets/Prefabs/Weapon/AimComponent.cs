using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimComponent : MonoBehaviour
{
    public Transform Muzze;
    public LayerMask layMask;
    /// <summary>
    /// 获取射击到的对象
    /// </summary>
    /// <param name="AimDir"></param>
    /// <returns></returns>
    public GameObject GetAimTarget(out Vector3 AimDir)
    {
        AimDir = (new Vector3(Muzze.transform.forward.x,0, Muzze.transform.forward.z)).normalized;
       RaycastHit target;
        if(Physics.Raycast(Muzze.position, AimDir, out target, 1000, layMask))
        {
            return target.collider.gameObject;
        }
        return null;
    }

}
