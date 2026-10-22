using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,IteamInterface
{
    [Header("摇杆")]
    public JoyStick movejoyStick;
    public JoyStick aimjoyStick;
    public float moveSpeed = 5f;
    public float maxmoveSpeed = 15f;
    public float turnLerp = 1f;
    public float animTurnSpeed = 1f;
    [Header("武器")]
    public InventoryComponent inventoryComponent;
    [Header("提取出的旋转功能")]
    public MovementComponent movementComponent;
    [Header("血量相关")]
    public HealthComponent healthComponennt;
    public PlayerHealthBar healthBar;
    [Header("能量相关")]
    public AbilityComponent abilityComponent;
    public PlayerStaminBar staminBar; 
    [Header("用于判断敌友")]
    public int iteamId = -1;
    private Animator animator;
    CharacterController characterController;
    private CameraController cameraController;
    Camera cameramia;
    float currentTurnSpeed = 0f;
    float animatorTurnSpeed = 0f;
    Vector2 moveinputVal;
    Vector2 aiminputVal;
    // Start is called before the first frame update
    void Start()
    {
        //cameraController
        cameramia = Camera.main;
        cameraController = FindAnyObjectByType<CameraController>();
        characterController = this.GetComponent<CharacterController>();
        animator = this.GetComponent<Animator>();
        movejoyStick.onstickValueUpdated += moveStickmoveStick;

        aimjoyStick.onstickValueUpdated += aimStickmoveStick;
        aimjoyStick.onstickTaped += startSwitchWeapon;

        healthComponennt.onHealthChange += healthBar.UpdateHealthUI;
        healthComponennt.UpdateUIImmadietly();
        healthComponennt.onHealthEmpty += PlayerStartDead;

        abilityComponent.onStaminUpdate += staminBar.UpdateStaminUI;
        abilityComponent.UpdateStaminUIImadietly();


    }
    void Update()
    {
        PerformMoveAndAim();
        UpdateCamera();
    }

    /// <summary>
    /// 观察者：攻击
    /// </summary>
    /// <param name="inputVal"></param>
    private void StartAttack(Vector2 inputVal)
    {
        if (inputVal.magnitude > 0)
        {
            animator.SetBool("Attacking", true);
        }
        else
        {
            animator.SetBool("Attacking", false);
        }
    }

    /// <summary>
    /// 切换武器
    /// </summary>
    private void startSwitchWeapon()
    {
        animator.SetTrigger("SwitchWeapon");
    }

    /// <summary>
    /// 移动和瞄准
    /// </summary>
    private void PerformMoveAndAim()
    {
        Vector3 moveDir = StickInputToworld(moveinputVal);
        characterController.Move(moveDir * Time.deltaTime * moveSpeed);
        float forward = Vector3.Dot(moveDir, characterController.transform.forward);
        float right = Vector3.Dot(moveDir, characterController.transform.right);
        animator.SetFloat("forwardSpeed", forward);
        animator.SetFloat("rightSpeed", right);


        UpdateAim(moveDir);
    }
    /// <summary>
    /// 观察者：玩家死亡
    /// </summary>
    /// <param name="Killer"></param>
    private void PlayerStartDead(GameObject Killer)
    {
        animator.SetLayerWeight(2, 1);
        animator.SetTrigger("Death");
    }
    /// <summary>
    /// 摄像机旋转相关
    /// </summary>
    private void UpdateCamera()
    {
        if (moveinputVal.magnitude != 0 && aiminputVal == null && cameraController != null) { cameraController.AddYawInput(-moveinputVal.x); }
    }

    #region 能力系统：加速
    public void AbilitySpeedBoost(float speedDelt)
    {
        moveSpeed = Mathf.Clamp(moveSpeed + speedDelt,1,maxmoveSpeed);
        //Debug.Log("此次变化量+speedDelt" + speedDelt);
        //Debug.Log("加速能力+moveSpeed="+moveSpeed);
    }
    #endregion
    //**********************************************************************************

    private void moveStickmoveStick(Vector2 inputVal)
    {
        this.moveinputVal = inputVal;
    }
    private void aimStickmoveStick(Vector2 inputVal)
    {
        this.aiminputVal = inputVal;
        StartAttack(inputVal);
    }

    void UpdateAim(Vector3 moveDir)
    {
        Vector3 aimDir = moveDir;
        if (aiminputVal.magnitude != 0) { aimDir = StickInputToworld(aiminputVal); }
        RotateTo(aimDir);
    }

    public void SwitchWeapon()
    {
        inventoryComponent.NextWeapon();
    }
    public void AttackPoint()
    {
        //Debug.Log("测试1111111111111111攻击");
        inventoryComponent.GetActiveWeapon().Attack();
    }
    void RotateTo(Vector3 aimDir)
    {
        currentTurnSpeed = 0f;
        if (moveinputVal.magnitude != 0 || aiminputVal.magnitude != 0)
        {
            currentTurnSpeed = movementComponent.RotateToward(aimDir);
        }
        animatorTurnSpeed = Mathf.Lerp(animatorTurnSpeed, currentTurnSpeed,Time.deltaTime* animTurnSpeed);
        animator.SetFloat("turningspeed", animatorTurnSpeed);

    }

    Vector3  StickInputToworld(Vector2 aimInput)
    {
        Vector3 rightDir = cameramia.transform.right;
        Vector3 upDir = Vector3.Cross(Vector3.up,-rightDir);
        Vector3 moveDir =Vector3.Normalize(rightDir * aimInput.x + upDir * aimInput.y);
        //Debug.Log(moveDir);
        return moveDir;
    }

    public  int GetId() { return iteamId; }

    public void DeathFinished()
    {
        //Debug.Log("1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111玩家死亡，游戏结束");

    }
}
