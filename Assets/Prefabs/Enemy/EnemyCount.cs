using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 记录敌人数量，当数量=0时结束游戏
/// </summary>
public class EnemyCount : MonoBehaviour
{
    public static int enemyCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        enemyCount++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        enemyCount--;
        if (enemyCount<=0)
        {
            LeverManager.LevelFinished();
        }
    }
}
