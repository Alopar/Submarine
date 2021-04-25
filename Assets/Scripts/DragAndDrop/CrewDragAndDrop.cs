using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CrewDragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] private Canvas _canvas;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private Crew _crew;

    private CrewCellPlace _beginCell;

    void Awake()
    {
        _crew = GetComponent<Crew>();
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _beginCell = _crew.cell;
        _canvasGroup.alpha = 0.6f;
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;

        if(_beginCell == _crew.cell)
        {
            transform.position = _beginCell.transform.position;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
    }

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
    }
}
