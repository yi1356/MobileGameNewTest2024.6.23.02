using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    private Transform attachPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Camera.main.WorldToScreenPoint(attachPoint.position);
    }
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="attachPoint"></param>
    public void Init(Transform attachPoint)
    {
        this.attachPoint = attachPoint;
    }
    /// <summary>
    /// 设置血条值
    /// </summary>
    /// <param name="valu"></param>
    public  void SetHeathSliderValue(float health, float delta, float maxHealth)
    {
        slider.value = health / maxHealth;
    }
    /// <summary>
    /// 销毁
    /// </summary>
    public void onDesHealthBar(GameObject a)
    {
        Destroy(this.gameObject);
    }

}
