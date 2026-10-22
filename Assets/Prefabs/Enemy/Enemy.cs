using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour,IBehaviorTreeInterface,IteamInterface,IspawnInterface
{
    public MovementComponent movementComponent;
    public PerceptionComponent perceptionComponent;
    public BehaviorTree tree;
    public int iteamId = -1;
    [Header("Reward")]
    public Reward reward;
    private HealthComponent healthComponent;
    protected Animator Ani;
    private Vector3 prePos;
    private  Vector3 currentPos;
    GameObject target;
    float delt;
    float moveSpeed;
    private void Awake()
    {
        healthComponent = this.GetComponent<HealthComponent>();
        Ani = this.GetComponent<Animator>();
        prePos = this.transform.position;

        healthComponent.onTakeDamage += TakeDamage;
        healthComponent.onHealthEmpty += StartDeath;
        perceptionComponent.onperceptionTargetChange += TargetChanged;
    }
    void Start()
    {
       
    }

   

    // Update is called once per frame
    void Update()
    {
        CalCulateSpeed();
    }

    /// <summary>
    /// 计算移动速度用于动画混合树
    /// </summary>
    public void CalCulateSpeed()
    {
        currentPos = this.transform.position;
        delt = (currentPos - prePos).magnitude;
        moveSpeed = delt / Time.deltaTime;
        prePos = currentPos;
        Ani.SetFloat("Speed", moveSpeed);
    }

    /// <summary>
    /// 观察者：监测到目标发生改变
    /// </summary>
    /// <param name="targetStimuli"></param>
    /// <param name="isfound"></param>
    private void TargetChanged(PerceptionStimuli targetStimuli, bool isfound)
    {
       // Debug.Log("测试aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaTargetChanged函数执行" + targetStimuli.gameObject.name+ isfound);
        if (isfound)
        {
            Debug.Log("测试bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb"+ targetStimuli.gameObject.name);
            tree.BlackBoard.SetOrAddData("Target", targetStimuli.gameObject);
        }
        else
        {
            tree.BlackBoard.RemoveBlackbaordData("Target");
            tree.BlackBoard.SetOrAddData("LastPoint",transform.position);
        }
    }

    /// <summary>
    /// 观察者：受到攻击
    /// </summary>
    /// <param name="health"></param>
    /// <param name="delta"></param>
    /// <param name="maxHealth"></param>
    /// <param name="inStigator"></param>
    private void TakeDamage(float health, float delta, float maxHealth, GameObject inStigator)
    {
        //受攻击动画
    }
    /// <summary>
    /// 观察者：死亡
    /// </summary>
    /// <param name="Killer"></param>
    private void StartDeath(GameObject Killer)
    {
        //死亡动画
        if (Ani != null)
        {
            Ani.SetTrigger("Death");
        }
        IRewardListenner[] rewardListenners = Killer.transform.GetComponents<IRewardListenner>();
        foreach (IRewardListenner  item in rewardListenners)
        {
            item.Reward(reward);
        }
    }
    /// <summary>
    ///转向
    /// </summary>
    /// <param name="target"></param>
    /// <param name="vertialAim"></param>
    void IBehaviorTreeInterface.RotateToWaords(GameObject target, bool vertialAim)
    {
        Vector3 Dir = target.transform.position - this.transform.position;
        if (!vertialAim)
        {
            Dir.y = 0;
        }
        movementComponent.RotateToward(Dir);
    }
    /// <summary>
    /// 攻击
    /// </summary>
    /// <param name="target"></param>
    public  virtual void Attack(GameObject target)
    {
    }

    /// <summary>
    /// 记录母体对象，传递感知结果
    /// </summary>
    /// <param name="gameObject"></param>
    public void SpaweBy(GameObject gameObject)
    {
        Debug.Log("测试333333333333333333333333333333333333记录母体对象执行");
        BehaviorTree gametree = gameObject.GetComponent<BehaviorTree>();
        if (gametree != null && gametree.BlackBoard.GetBlackbaordData<GameObject>("Target",out  target))
        {
            PerceptionStimuli stimuli = target.GetComponent<PerceptionStimuli>();
            perceptionComponent.AssignPercievedStimuli(stimuli);
            Debug.Log("测试4444444444444444444444444444444444444传递感知结果执行");
        }
       
    }
    //***************************************************
    //死亡动画方法
    public void OnDeathAnimationFinished()
    {
        Debug.Log("怪物死亡");
        Death();
        Destroy(this.gameObject);
    }
    protected virtual void Death()
    {

    }
    public int GetId() 
    {
        return iteamId;
    }

    
}
