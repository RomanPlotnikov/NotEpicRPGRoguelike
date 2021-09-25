using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup _canvasGroup;
    private Canvas _uiCanvas;
    private RectTransform _rectTransform;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _uiCanvas = GetComponentInParent<Canvas>();
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Transform slotTransform = _rectTransform.parent;
        slotTransform.SetAsFirstSibling();
        _canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _uiCanvas.scaleFactor; 
    }
     
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = Vector3.zero;
        _canvasGroup.blocksRaycasts = true;
    }

}
