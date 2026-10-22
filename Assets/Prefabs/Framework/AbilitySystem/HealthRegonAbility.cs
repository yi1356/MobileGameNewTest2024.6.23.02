using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Ability/HeathRegionBoost")]
public class HealthRegonAbility : Ability
{
    public float healthRegonDuration;
    public float healthAmt;
    HealthComponent healthComponent;

    public override void ActivateAbility()
    {
        if (CommitAbility())
        {
            healthComponent = abilityComponent.transform.GetComponent<HealthComponent>();
            abilityComponent.StartCoroutine(IHealthRegon(healthRegonDuration,healthAmt,healthComponent));
        }
    }

    IEnumerator IHealthRegon(float healthRegonDuration,float healthAmt,HealthComponent healthComponent)
    {
        float duration = healthRegonDuration;
        float healthDelt = healthAmt/ healthRegonDuration;
        while (duration>0)
        {
            duration -= Time.deltaTime;
            healthComponent.ChangeHealth(Time.deltaTime * healthDelt,abilityComponent.gameObject);
            yield return new WaitForEndOfFrame();
        }
        yield return null;

    }
}

