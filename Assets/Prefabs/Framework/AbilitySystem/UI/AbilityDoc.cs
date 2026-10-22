using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 能力UI容器组件
/// </summary>
public class AbilityDoc : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public AbilityComponent abilityComponent;
    public AbilityUI abilityUIPrefab;
    public RectTransform root;

    //public float highLightSize = 1.5f;
    public float scalRange = 200f;

    private List<AbilityUI> abilityUIs=new List<AbilityUI>();
    private PointerEventData touchData;//注意，这个是引用类型，down,drag,up中的都是同一个

    private Vector3 GoalScale = Vector3.one;
    private float ScaleSpeed = 20f;

    private void Awake()
    {
        abilityComponent.onAbilityAdd += AddAbility;
      
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (touchData!=null)
        {
            ArrangeScale(touchData);
        }
        transform.localScale = Vector3.Lerp(transform.localScale, GoalScale, Time.deltaTime * ScaleSpeed);
    }
    /// <summary>
    /// 观察者：向容器中增加能力UI
    /// </summary>
    /// <param name="ability"></param>
    private void AddAbility(Ability ability)
    {
        AbilityUI interimAbilityUI= GameObject.Instantiate<AbilityUI>(abilityUIPrefab,root);
        interimAbilityUI.Init(ability);
        abilityUIs.Add(interimAbilityUI);
        //interimAbilityUI
    }

    /// <summary>
    /// 能力UI根据与手指的距离进行缩放
    /// </summary>
    /// <param name="touchData"></param>
    private void ArrangeScale(PointerEventData touchData)
    {
        for (int i=0;i<abilityUIs.Count;i++)
        {
            float abilityUIVeticalPos = abilityUIs[i].GetComponent<RectTransform>().position.y;
            float distance = Mathf.Abs(touchData.position.y - abilityUIVeticalPos);
            float scaleAmt = (scalRange - distance) / scalRange;
            abilityUIs[i].SetScaleAmt(scaleAmt);
        }
    }

    /// <summary>
    /// 检测手指处的UI是否有AbilityUI，有就得到，且返回true
    /// </summary>
    /// <param name="touchData"></param>
    /// <param name="abilityUI"></param>
    /// <returns></returns>
    private bool GetUIUnderPoint(PointerEventData touchData,out AbilityUI abilityUI)
    {
        abilityUI = null;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(touchData, raycastResults);
        if (raycastResults.Count==0)
        {
            return false;
        }
        for (int i = 0; i < raycastResults.Count; i++)
        {
            if (raycastResults[i].gameObject.GetComponentInParent<AbilityUI>())
            {
                abilityUI = raycastResults[i].gameObject.GetComponentInParent<AbilityUI>();
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 按下，抬起相应的逻辑
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        touchData = eventData;
        GoalScale = Vector3.one * 1.5f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (GetUIUnderPoint(touchData, out AbilityUI abilityUI))
        {
            abilityUI.ActivateAbility();
        }
        ResetAbilityUI();
        touchData = null;
        GoalScale = Vector3.one;
    }
    //***********************************************************************
    //能力UI回复大小
    private void ResetAbilityUI()
    {
        for (int i = 0; i < abilityUIs.Count; i++)
        {  
            abilityUIs[i].SetScaleAmt(0f);
        }
    }


}
