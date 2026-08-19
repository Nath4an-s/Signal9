using UnityEngine;
using UnityEngine.EventSystems;

public class ImageZoomPan : MonoBehaviour, IDragHandler, IScrollHandler
{
    public RectTransform target;
    public RectTransform viewport;
    public float zoomSpeed = 0.1f;
    public float minZoom = 1f;
    public float maxZoom = 5f;

    public void OnScroll(PointerEventData eventData)
    {
        float scrollDelta = eventData.scrollDelta.y;
        float newScale = target.localScale.x + scrollDelta * zoomSpeed;
        newScale = Mathf.Clamp(newScale, minZoom, maxZoom);
        target.localScale = new Vector3(newScale, newScale, 1f);

        ClampPosition();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (target.localScale.x <= minZoom) return;

        target.anchoredPosition += eventData.delta / target.GetComponentInParent<Canvas>().scaleFactor;

        ClampPosition();
    }

    void ClampPosition()
    {
        float scale = target.localScale.x;

        float targetWidth = target.rect.width * scale;
        float targetHeight = target.rect.height * scale;

        float viewportWidth = viewport.rect.width;
        float viewportHeight = viewport.rect.height;

        float maxX = Mathf.Max(0, (targetWidth - viewportWidth) / 2f);
        float maxY = Mathf.Max(0, (targetHeight - viewportHeight) / 2f);

        Vector2 pos = target.anchoredPosition;
        pos.x = Mathf.Clamp(pos.x, -maxX, maxX);
        pos.y = Mathf.Clamp(pos.y, -maxY, maxY);
        target.anchoredPosition = pos;
    }
}