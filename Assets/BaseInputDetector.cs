using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections;


/// <summary>
/// Base class for all input detectors
/// </summary>
public class BaseInputDetector : MonoBehaviour, IPointerClickHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{

    // Public Variables
    public int inputDetectorID;
    public Action<PointerEventData> onPointerEnterAction = null;
    public Action<PointerEventData> onPointerExitAction = null;

    public Action<PointerEventData> onPointerDownAction = null;
    public Action<PointerEventData> onPointerUpAction = null;
    public Action<PointerEventData> onDragAction = null;

    public Action<PointerEventData> onPointerClickAction = null;




    public void InitActions(Action<PointerEventData> pointerEnter, Action<PointerEventData> pointerExit, Action<PointerEventData> pointerDown, Action<PointerEventData> pointerUp, Action<PointerEventData> drag, Action<PointerEventData> click)
    {
        onPointerEnterAction = pointerEnter;
        onPointerExitAction = pointerExit;

        onPointerDownAction = pointerDown;
        onPointerUpAction = pointerUp;
        onDragAction = drag;

        onPointerClickAction = click;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        CommonLog("Down");
        if (onPointerDownAction != null)
        {
            onPointerDownAction(eventData);
        }
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        CommonLog("Up");
        if (onPointerUpAction != null)
        {
            onPointerUpAction(eventData);
        }
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        CommonLog("Drag");
        if (onDragAction != null)
        {
            onDragAction(eventData);
        }
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        CommonLog("Enter");
        if (onPointerEnterAction != null)
        {
            onPointerEnterAction(eventData);
        }
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        CommonLog("Exit");
        if (onPointerExitAction != null)
        {
            onPointerExitAction(eventData);
        }
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        CommonLog("OnPointerClick");
        if (onPointerClickAction != null)
        {
            onPointerClickAction(eventData);
        }
    }

    private void CommonLog(string str)
    {
        //DebugLogger.Log (gameObject.name + "." + this.GetType ().Name + ": " + str, gameObject);
    }
}