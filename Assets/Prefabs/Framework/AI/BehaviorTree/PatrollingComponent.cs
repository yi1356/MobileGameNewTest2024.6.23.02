using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingComponent : MonoBehaviour
{
    public Transform[] patrollingPoints;
    int currentPointIndex = -1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 获取下一个巡逻点
    /// </summary>
    /// <param name="patrollingPoint"></param>
    /// <returns></returns>
    public bool GetNextPatrollingPoint(out Vector3 patrollingPoint)
    {
        if (patrollingPoints.Length==0)
        {
            patrollingPoint = Vector3.zero;
            return false;
        }
        currentPointIndex++;
        currentPointIndex = currentPointIndex % patrollingPoints.Length;
        patrollingPoint = patrollingPoints[currentPointIndex].position;
        return true;
    }
}
