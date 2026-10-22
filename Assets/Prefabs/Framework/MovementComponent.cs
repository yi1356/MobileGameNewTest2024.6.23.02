using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    CharacterController characterController;
    public float turnLerp = 1f;
    private void Awake()
    {
        characterController = this.GetComponent<CharacterController>();
    }
    /// <summary>
    /// 提取出的旋转功能
    /// </summary>
    /// <param name="aimDir"></param>
    /// <returns></returns>
    public float RotateToward(Vector3 aimDir)
    {
        //Debug.Log("测试------------RotateToward方法已经执行");
        float currentTurnSpeed = 0;
        Quaternion preRot = characterController.transform.rotation;
        characterController.transform.rotation = Quaternion.Lerp(characterController.transform.rotation, Quaternion.LookRotation(aimDir, Vector3.up), turnLerp);
        Quaternion currentRot = characterController.transform.rotation;

        int Dir = Vector3.Dot(characterController.transform.right, aimDir) > 0 ? 1 : -1;
        float rotationDelta = Quaternion.Angle(currentRot, preRot) * Dir;
        currentTurnSpeed = rotationDelta / Time.deltaTime;
        return currentTurnSpeed;
    }
}
