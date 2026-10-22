using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
public class CameraArm : MonoBehaviour
{
    public Transform child;
    public float ArmLenth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        //注意这一句，实现了摄像机看向目标**********
        child.position = this.transform.position - ArmLenth* child.forward;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(this.transform.position,child.transform.position);
    }
}
