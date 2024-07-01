using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    public static GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform startParent;



    private void Awake()
    {

        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .6f;
        //So the ray cast will ignore the item itself.
        canvasGroup.blocksRaycasts = false;
        startPosition = transform.position;
        startParent = transform.parent;
        transform.SetParent(transform.root);
        itemBeingDragged = gameObject;

    }

    public void OnDrag(PointerEventData eventData)
    {
        //So the item will move with our mouse (at same speed)  and so it will be consistant if the canvas has a different scale (other then 1);
        rectTransform.anchoredPosition += eventData.delta;
    }



    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeingDragged = null;

        GameObject newBagHoll = GetBagHollUnderMouse();
        if (newBagHoll != null)
        {
            newSortID(newBagHoll);
            rectTransform.anchoredPosition = Vector3.zero;
            transform.SetParent(newBagHoll.transform, false);
        }
        else
        {
            transform.position = startPosition;
            transform.SetParent(startParent);
        }

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    private void newSortID(GameObject newBagHoll)
    {
        bagSortID bSID = newBagHoll.GetComponent<bagSortID>();
        ItemData iData = GetComponent<ItemData>();
        if (iData != null)
        {
            iData.bagItem.sortID = bSID.sortID;
        }
   
    }

    private GameObject GetBagHollUnderMouse()
    {
        // 从鼠标位置发射射线，找到 bagHoll
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        RaycastResult result = Raycast(pointerEventData);

        if (result.gameObject != null && result.gameObject.CompareTag("BagHoll"))
        {
            return result.gameObject;
        }

        return null;
    }

    private RaycastResult Raycast(PointerEventData pointerEventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);
        return results.Count > 0 ? results[0] : new RaycastResult();
    }

}
