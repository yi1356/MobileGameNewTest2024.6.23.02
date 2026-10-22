using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour,IDragHandler,IPointerDownHandler,IPointerUpHandler
{
    public RectTransform BackgroundTrans;
    public RectTransform ThumbStickTrans;
    public RectTransform CenterTrans;

    public delegate void OnstickValueUpdated(Vector2 inputVal);
    public delegate void OnstickTaped();
    public event OnstickValueUpdated onstickValueUpdated;
    public event OnstickTaped onstickTaped;

    bool istaped=false;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        CenterTrans.position = eventData.position;
        istaped = true;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        onstickValueUpdated?.Invoke(new Vector2(0,0));
        if (istaped) { onstickTaped?.Invoke(); } istaped = false;

        ThumbStickTrans.position = BackgroundTrans.position;
        CenterTrans.anchoredPosition = new Vector3(0,0,0);
    }
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector2 touchPos= eventData.position;
        Vector2 centerPos = BackgroundTrans.position;
        Vector2 localoff = Vector2.ClampMagnitude(touchPos- centerPos, BackgroundTrans.sizeDelta.x/2);
        onstickValueUpdated?.Invoke(localoff);
        ThumbStickTrans.position = centerPos+ localoff;
        istaped = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  
}
