using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum E_UILayer
{
    Bot,
    Mid,
    Top,
    System,
}

/// <summary>
/// UI管理器
/// 1、管理所有显示的面板
/// 2、提供给外部，显示和隐藏等等接口
/// 3、得到一个已经显示的面板
/// 4、得到对应层级
/// 5、给某一控件添加一种的事件监听
/// </summary>
public class UIManager2 : SingletonManager<UIManager2>
{
    public  RectTransform Canvas;
   
    private Transform bot;
    private Transform mid;
    private Transform top;
    private Transform system;

    public Dictionary<string,BasePanel> panelDic;
    #region 利用构造函数，动态创建Canvas和System，并且过场景不移除
    public UIManager2()
    {
        GameObject obj = ResourcesMgr.Instance.Load<GameObject>("UI/Canvas");
        Canvas = obj.GetComponent<RectTransform>();
        GameObject.DontDestroyOnLoad(obj);

        obj = ResourcesMgr.Instance.Load<GameObject>("UI/EventSystem");
        GameObject.DontDestroyOnLoad(obj);
        //找到各层
        bot = Canvas.Find("Bot");
        mid= Canvas.Find("Mid");
        top= Canvas.Find("Top");
        system= Canvas.Find("System");

        panelDic = new Dictionary<string, BasePanel>();

    }
    #endregion
    /// <summary>
    /// 显示面板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="penalName">面板脚本名</param>
    /// <param name="uiLayer">显示在那一层</param>
    /// <param name="callBack">当面板预设体创建成功后，你想做的事</param>
    public void ShowPanel<T>(string penalName, E_UILayer uiLayer,UnityAction<T> callBack=null)where T:BasePanel
    {
        if (panelDic.ContainsKey(penalName))
        {
            panelDic[penalName].ShowMe();
            if (callBack != null) //处理面板创建完成后的逻辑
            { callBack(panelDic[penalName] as T); }
            return;
        }
      
        ResourcesMgr.Instance.LoadAsyn<GameObject>("UI/"+ penalName,(obj)=>
        {
            //放入Canvas,作为子对象
            Transform father = mid;
            switch (uiLayer)
            {
                case E_UILayer.Bot:
                    father = bot;
                    break;
                case E_UILayer.Mid:
                    father = mid;
                    break;
                case E_UILayer.Top:
                    father = top;
                    break;
                case E_UILayer.System:
                    father = system;
                    break;
                default:
                    break;
            }
            obj.transform.SetParent(father);
            //并且设置相对位置
            (obj.transform as RectTransform).localPosition = Vector3.zero;
            (obj.transform as RectTransform).localScale = Vector3.one;
            (obj.transform as RectTransform).offsetMax = Vector2.zero;
            (obj.transform as RectTransform).offsetMin = Vector2.zero;
            //得到预设体身上的面板脚本
            T panel = obj.GetComponent<T>();
            //处理面板创建完成后的逻辑
            if (callBack!=null) 
            { callBack(panel); }
            //将显示的面板存起来
            panelDic.Add(penalName,panel);
        });
    }
    /// <summary>
    /// 隐藏面板
    /// </summary>
    /// <param name="penalName"></param>
    public void HidePanel(string penalName)
    {
        if (panelDic.ContainsKey(penalName))
        {
            GameObject.Destroy(panelDic[penalName].gameObject);
            panelDic.Remove(penalName);
        }
    }
    /// <summary>
    /// 得到一个已经显示的面板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name">要得到的面板名</param>
    /// <returns></returns>
    public T GetPanel<T>(string name) where T:BasePanel
    {
        if (panelDic.ContainsKey(name)) 
        { return panelDic[name] as T; }
        return null;
    }
    /// <summary>
    /// 4、得到对应层级
    /// </summary>
    /// <param name="uiLayer"></param>
    /// <returns></returns>
    public Transform GetLayerFather(E_UILayer uiLayer)
    {
        switch (uiLayer)
        {
            case E_UILayer.Bot:
                return this.bot;
                break;
            case E_UILayer.Mid:
                return this.mid;
                break;
            case E_UILayer.Top:
                return this.top;
                break;
            case E_UILayer.System:
                return this.system;
                break;
            default:
                return null;
                break;
               
        }
    }

    /// <summary>
    ///  5、给某一控件添加一种的事件监听
    /// </summary>
    /// <param name="control">要添加监听事件的控件</param>
    /// <param name="type">事件类型</param>
    /// <param name="callback">事件的响应函数</param>
    public static void AddCustomEventListener(UIBehaviour control, EventTriggerType type, UnityAction<BaseEventData> callback)
    {
        EventTrigger trigger = control.GetComponent<EventTrigger>();
        if (trigger==null)
        { trigger = control.gameObject.AddComponent<EventTrigger>(); }

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = type;
        entry.callback.AddListener(callback);

        trigger.triggers.Add(entry);
    }
}
