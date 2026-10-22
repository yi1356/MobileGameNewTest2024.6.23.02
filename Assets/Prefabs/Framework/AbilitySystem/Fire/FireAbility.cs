using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Ability/FireAbility")]
public class FireAbility : Ability
{
    [Header("检测")]
    public Scaner scanerPrefab;

    public float scanDuration = 2f;
    public float scanRange = 20f;
    public GameObject scanerVFX;
    [Header("火焰伤害")]
    public float fireDuration = 3f;
    public float fireAmt = 60f;
    public GameObject fireVFX;
    IteamInterface iteamInterface;
    public override void ActivateAbility()
    {
        if (CommitAbility())
        {
            //检测到对象
            Scaner  curSan=GameObject.Instantiate<Scaner>(scanerPrefab, this.abilityComponent.transform.position,this.abilityComponent.transform.rotation, this.abilityComponent.transform);
            curSan.SetScanDuration(scanDuration);
            curSan.SetScanRange(scanRange);
            curSan.onScannerUpdate += DetectionUpdated;
            GameObject obj=GameObject.Instantiate<GameObject>(scanerVFX);
            curSan.AddChildAttached(obj.transform);
            curSan.StartScan();
        }
    }
    /// <summary>
    /// 观察者：判断是否是应被攻击对象
    /// </summary>
    /// <param name="gameobj"></param>
    private void DetectionUpdated(GameObject gameobj)
    {
        iteamInterface = abilityComponent.transform.GetComponent<IteamInterface>();
        if (iteamInterface.GetRelationToward(gameobj)==E_TeamRelation.enemy)
        {
            HealthComponent healthComponent = gameobj.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                //生成特效等的逻辑

                abilityComponent.StartCoroutine(ApplyDamageTo(healthComponent));
            }
        }
    }

    /// <summary>
    /// 减血的逻辑
    /// </summary>
    /// <param name="healthComponent"></param>
    /// <returns></returns>
    IEnumerator ApplyDamageTo(HealthComponent healthComponent)
    {
        float duration = fireDuration;
        float delt = fireAmt / fireDuration;
        GameObject fireVFXobj = GameObject.Instantiate<GameObject>(fireVFX, healthComponent.transform);
        //Debug.Log("测试11111111111111111111111111111111111111火焰攻击到的目标 healthComponent" + healthComponent.gameObject.name);
        while (duration>0)
        {
            duration -= Time.deltaTime;
            if (healthComponent==null) { break; }
            healthComponent.ChangeHealth(-delt*Time.deltaTime,abilityComponent.gameObject);
            yield return new WaitForEndOfFrame();
        }
        if (fireVFXobj != null) { Destroy(fireVFXobj); }
        //Debug.Log("测试22222222222222222222222222222222火焰攻击结束");
        yield return null;

    }

}
