using UnityEngine;
using UnityEngine.EventSystems;

public class WindowDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerDownHandler
{
    public RectTransform windowToMove;

    private Canvas canvas;

    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        if (windowToMove == null)
        {
            windowToMove = transform.parent.GetComponent<RectTransform>();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        windowToMove.SetAsLastSibling();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        windowToMove.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        windowToMove.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}