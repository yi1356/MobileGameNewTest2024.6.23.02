using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIComponent : MonoBehaviour
{
    public HealthComponent healthComponent;
    public HealthBar healthBar;
    public Transform attachPoint;
    // Start is called before the first frame update
    /// <summary>
    /// 生成血条 初始化 监听
    /// </summary>
    void Start()
    {
        InGameUI inGameUI = FindAnyObjectByType<InGameUI>();
        HealthBar newHealthBar = Instantiate<HealthBar>(healthBar,inGameUI.transform);
        newHealthBar.Init(attachPoint);
        healthComponent.onHealthChange += newHealthBar.SetHeathSliderValue;
        healthComponent.onHealthEmpty += newHealthBar.onDesHealthBar;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
