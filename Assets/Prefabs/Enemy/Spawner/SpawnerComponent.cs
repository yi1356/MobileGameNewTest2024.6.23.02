using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 作用：生产怪物
/// </summary>
public class SpawnerComponent : MonoBehaviour
{
    public GameObject[] objectToSpawn;
    public Animator animator;
    [SerializeField] Transform spawnPoint;
    /// <summary>
    /// 生产怪物
    /// </summary>
    /// <returns></returns>
    public bool StartSpawn()
    {
        if (objectToSpawn.Length==0)
        {
            return false;
        }
        if (animator != null)
        {
            animator.SetTrigger("Spawner");
        }
        else
        {
            SpawnImpl();
        }
        return true;
    }

    //**************
    public void SpawnImpl()
    {
        int index = Random.RandomRange(0, objectToSpawn.Length);
        GameObject newSpawn = GameObject.Instantiate(objectToSpawn[index], spawnPoint.transform.position,spawnPoint.transform.rotation);
        IspawnInterface ispawnInterface = newSpawn.GetComponent<IspawnInterface>();
        //if (ispawnInterface==null)
        //{
        //    Debug.Log("测试111111111111111111111111111111111111111111111111111111111111111111111-----------------生产动画已经播放ispawnInterface==null");
        //}
        //else
        //{
        //    Debug.Log("测试2222222222222222222222222222222222222222222222222222222222222222222222-----------------生产动画已经播放ispawnInterface!=null");
        //}
        
        ispawnInterface.SpaweBy(this.gameObject);

    }
}
