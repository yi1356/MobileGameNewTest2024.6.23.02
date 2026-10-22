using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Mono的管理者
/// 知识点：周期函数、事件、协程
/// </summary>
public class MonoController : MonoBehaviour
{
    private event UnityAction updateEvent;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (updateEvent != null)
            updateEvent.Invoke();
    }
    public void AddupdateListener(UnityAction func)
    {
        updateEvent+=func;
    }
    public void RemoveupdateListener(UnityAction func)
    {
        if (updateEvent!=null) { updateEvent -= func; } 
    }
}
