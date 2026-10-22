using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelyPush : MonoBehaviour
{
    
    void Start()
    {
       
    }
    private void OnEnable()
    {
        Invoke("push", 1f);
    }
    private void push()
    {
        PoolMgr.Instance.PushObj(this.gameObject.name,this.gameObject);
    }
   
}
