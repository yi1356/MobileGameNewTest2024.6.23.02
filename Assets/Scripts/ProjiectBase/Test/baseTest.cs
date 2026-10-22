using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ttt
{
    public void monoTest()
    {
        Debug.Log("Mono测试方法已经执行");
    }
   public IEnumerator b()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("Mono测试协程已经执行");
    }
}

public class baseTest : MonoBehaviour
{
    float v = 0;
    #region 音乐模块测试
    private void OnGUI()
    {
        #region 音效相关模块
        //if (GUI.Button(new Rect(0, 0, 100, 100), "播放音乐"))
        //{ MusicMgr.Instance.PlayerBkMusic("A"); }
        //if (GUI.Button(new Rect(0, 100, 100, 100), "暂停音乐"))
        //{ MusicMgr.Instance.PauseBkMusic(); }
        //if (GUI.Button(new Rect(0, 200, 100, 100), "停止音乐"))
        //{ MusicMgr.Instance.StopBkMusic(); }
        //MusicMgr.Instance.ChangeBKValue(v);
        //v += Time.deltaTime / 100f;
        //if (GUI.Button(new Rect(0, 300, 100, 100), "播放音效"))
        //{ MusicMgr.Instance.PlayerSound("B", false); }
        #endregion

    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {

        #region 输入控制模块（pc）
        //InputMgr.Instance.StartOrEndCheck(true);
        //EventCenterManager.Instance.AddEventListener<KeyCode>("按下按键", CheckInputDown);
        //EventCenterManager.Instance.AddEventListener<KeyCode>("抬起按键", CheckInputUp);
        #endregion
        #region 事件中心模块测试(有参无参)
        ////EventCenterManager.Instance.AddEventListener<string>("www", borthFunc);
        //EventCenterManager.Instance.AddEventListener("www", () => { Debug.Log("无参数已经测试"); });
        ////EventCenterManager.Instance.EventTrigger<string>("www", "hellow wolrd1111111111111111111111111");
        //EventCenterManager.Instance.EventTrigger("www");

        #endregion

    }
    void CheckInputDown(KeyCode key)
    {
        KeyCode code = (KeyCode)key;
        switch (code)
        {
            case KeyCode.W:Debug.Log("前进");break;
            case KeyCode.S: Debug.Log("后退"); break;
            case KeyCode.A: Debug.Log("左转"); break;
            case KeyCode.D: Debug.Log("右转"); break;
            default:break;
        }
    }
    void CheckInputUp(KeyCode key)
    {
        KeyCode code = (KeyCode)key;
        switch (code)
        {
            case KeyCode.W: Debug.Log("前进停止"); break;
            case KeyCode.S: Debug.Log("后退停止"); break;
            case KeyCode.A: Debug.Log("左转停止"); break;
            case KeyCode.D: Debug.Log("右转停止"); break;
            default: break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        #region 缓存池 测试
        //if (Input.GetMouseButtonDown(0)) { PoolMgr.Instance.GetObj("Test/Cube", (obj) => { obj.transform.localScale = new Vector3(2, 2, 2); }); }
        //if (Input.GetMouseButtonDown(1)) { PoolMgr.Instance.GetObj("Test/Sphere", (obj) => { obj.transform.localScale = new Vector3(3, 3, 3); }); }
        #endregion
        #region 场景切换和资源加载 测试
        //if (Input.GetKeyDown(KeyCode.Alpha1)) { ScenceMgr.Instance.LoadScence("SampleScene", SSS1); }
        //if (Input.GetKeyDown(KeyCode.Alpha2)) { ScenceMgr.Instance.LoadScenceAsyn("SampleScene", SSS2); }
        //if (Input.GetKeyDown(KeyCode.Alpha3)) { ResourcesMgr.Instance.Load<GameObject>("Test/Cube"); }
        //if (Input.GetKeyDown(KeyCode.Alpha4)) { ResourcesMgr.Instance.LoadAsyn<GameObject>("Test/Sphere", (obj) => { Debug.Log(obj.name); }); }
        #endregion
        #region UI模块测试
        //if (Input.GetKeyDown(KeyCode.Alpha5)) { UIManager.Instance.ShowPanel<LoginPanelTest>("LoginPanelTest", E_UILayer.Mid); }
        //if (Input.GetKeyDown(KeyCode.Alpha6)) { UIManager.Instance.HidePanel("LoginPanelTest"); }
        #endregion

    }
    void SSS1()
    {
        Debug.Log("0111111111111111111");
        PoolMgr.Instance.Clear();
    }
    void SSS2()
    {
        Debug.Log("022222222222222222222");
        PoolMgr.Instance.Clear();
    }
    void borthFunc(string name)
    {
        print("事件中心模块已经执行传入的参数名称"+ name);
    }
  
}
