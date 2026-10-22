using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// 面板基类
/// 功能:
/// 1、找到自己面板下的控件对象
/// 2、提供隐藏 和 显示自己的行为
/// </summary>
public class BasePanel : MonoBehaviour
{
    //通过里氏替换原则，来存储所有控件
    private Dictionary<string, List<UIBehaviour>> controlDic = new Dictionary<string, List<UIBehaviour>>();

   protected virtual void Awake()
    {
        FindChildrenControl<Button>();
        FindChildrenControl<Image>();
        FindChildrenControl<Text>();
        FindChildrenControl<Toggle>();
        FindChildrenControl<Slider>();
        FindChildrenControl<ScrollRect>();
        FindChildrenControl<InputField>();
    }
    /// <summary>
    /// 找到子对象的对应控件（包括孙对象等）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    private void FindChildrenControl<T>()where T : UIBehaviour
    {
        T[] controls = this.GetComponentsInChildren<T>();
        //string objName;注意，这个不能写在这里
        for (int i=0;i< controls.Length;i++)
        {
            string objName = controls[i].gameObject.name;
            if (controlDic.ContainsKey(objName))
            { controlDic[objName].Add(controls[i]); }//这样就解决了如：既挂载了Image又挂载了Button的情况
            else
            { controlDic.Add(objName,new List<UIBehaviour>() {controls[i] }); }
            //判断是按钮就为他添加监听的函数（在子类重写）
            if(controls[i] is Button) 
            { (controls[i] as Button).onClick.AddListener(()=> { Onclick(objName); }); }
        }
    }
    /// <summary>
    /// 得到对应名字的对应控件脚本
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ControlName"></param>
    /// <returns></returns>
    protected T GetControl<T>(string ControlName) where T : UIBehaviour
    {
        if (controlDic.ContainsKey(ControlName))
        {
            for (int i=0;i< controlDic[ControlName].Count;i++)
            {
                if (controlDic[ControlName][i] is T)
                { return controlDic[ControlName][i] as T; }
            }
        }
        return null;
    }
    #region 显示/隐藏相关(需要子类重写)
    public virtual void ShowMe()
    {

    }
    public virtual void HidMe()
    {

    }
    #endregion
    /// <summary>
    /// 按钮监听的函数
    /// </summary>
    /// <param name="name">具体的按钮名称</param>
    public virtual void Onclick(string name)
    {

    }
   
}
