using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//摄像机跟随+控制旋转的实现方法
public class CameraController : MonoBehaviour
{
    public Transform followTrans;
    public float turnSpeed = 0.1f;

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
        transform.position = followTrans.position;
    }
    /// <summary>
    /// 传入值，摄像机围绕玩家转动的方法
    /// </summary>
    /// <param name="input"></param>
    public void AddYawInput(float input)
    {
        transform.Rotate(Vector3.up, turnSpeed*input*Time.deltaTime);
    }
}
