using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Ability/SpeedBoost")]
public class SpeedBoostAbility : Ability
{
    public float speedDelt=2f;
    public float persistTime = 5f;
    public override void ActivateAbility()
    {
        //Debug.Log("测试4444444444444444444正要检查条件是否满足");
        if (CommitAbility())
        {
            //Debug.Log("测试4444444444444444444条件满足能力已经激活");
            Player player = abilityComponent.GetComponent<Player>();
            player.AbilitySpeedBoost(speedDelt);
            abilityComponent.StartCoroutine(abilityReset(player));
        }
    }

    IEnumerator abilityReset(Player player)
    {
        yield return new WaitForSeconds(persistTime);
        //Debug.Log("测试2222222222222222222222abilityReset已经执行");
        player.AbilitySpeedBoost(-speedDelt);
    }
}
