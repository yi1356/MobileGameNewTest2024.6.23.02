using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 用于火焰能力的，对象检测
/// </summary>
public class Scaner : MonoBehaviour
{
    public Transform sccanerPivot;//检测特效的挂载点
    float scanDuration=1f;
    float scanRange=20f;
    public  delegate void OnScannerUpdate(GameObject obj);
    public event OnScannerUpdate onScannerUpdate;
   public void SetScanDuration(float scanDuration)
    {
        this.scanDuration = scanDuration;
    }

   public void SetScanRange(float scanRange)
    {
        this.scanRange = scanRange;
    }

    /// <summary>
    /// 设置生成检测特效的父对象
    /// </summary>
    /// <param name="newChild">检测特效</param>
    public void AddChildAttached(Transform newChild)
    {
        newChild.SetParent(sccanerPivot);
        newChild.localPosition = Vector3.zero;
    }

    /// <summary>
    /// 开始检测
    /// </summary>
    public void StartScan()
    {
        StartCoroutine(IScannerUpdate());
    }
    IEnumerator IScannerUpdate()
    {
        float curDurationTime = scanDuration;
        Vector3 range = new Vector3(scanRange,scanRange,scanRange);
        while (curDurationTime>0)
        {
            curDurationTime -= Time.deltaTime;
            sccanerPivot.transform.localScale = Vector3.Lerp(sccanerPivot.transform.localScale, range,Time.deltaTime*(1/ scanDuration));
            yield return new WaitForEndOfFrame();
        }
        yield return null;
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        onScannerUpdate?.Invoke(other.gameObject);
    }
}
