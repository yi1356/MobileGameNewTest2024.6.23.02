using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoginPanelTest: BasePanel
{
    private void Start()
    {
      
        GetControl<Image>("01").color = Color.red;
        #region 测试为控件添加 监听事件
        //EventTrigger trigger = GetControl<Button>("01").gameObject.AddComponent<EventTrigger>();
        //EventTrigger.Entry entry = new EventTrigger.Entry();
        //entry.eventID = EventTriggerType.PointerEnter;
        //entry.callback.AddListener(DDD);
        //trigger.triggers.Add(entry);

        UIManager2.AddCustomEventListener(GetControl<Image>("01"),EventTriggerType.Drag, DDD);
        #endregion

    }
    void DDD(BaseEventData data)
    {
        Debug.Log("开始拖拽");
    }
    public override void Onclick(string name)
    {
        //if (name == "wer") { Debug.Log("我点击了按钮01"); }
        if (name== "01") { Debug.Log("我点击了按钮01"); }
        if (name == "wer") { Debug.Log("我点击了按钮wer"); }
    }
}
