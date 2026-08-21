using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// À attacher sur GraphViewport (le même GameObject que le RectMask2D).
// GraphViewport doit aussi porter une Image (couleur alpha = 0, Raycast Target = true) :
// sans Graphic, le GraphicRaycaster d'Unity n'envoie aucun événement souris à ce RectTransform.
//
// Structure attendue :
//   GraphViewport            <- ce script + RectMask2D + Image (transparente, raycast target)
//     GraphContent           <- anchorMin/Max = (0.5,0.5), pivot = (0.5,0.5), pos = (0,0)
//       EdgesLayer           <- idem, enfant de GraphContent
//       NodesLayer           <- idem, enfant de GraphContent, APRÈS EdgesLayer
[RequireComponent(typeof(RectTransform))]
public class GraphViewController : MonoBehaviour, IScrollHandler, IDragHandler
{
    [Tooltip("Le conteneur pannable/zoomable (GraphContent), enfant de ce Viewport.")]
    public RectTransform content;

    [Header("Zoom")]
    public float minZoom = 0.4f;
    public float maxZoom = 2.5f;
    public float zoomSpeed = 0.1f;

    [Header("Fit to view")]
    [Tooltip("Marge en pixels (espace graphe) ajoutée autour des nœuds lors du fit-to-view.")]
    public float fitPadding = 80f;
    [Tooltip("Ne jamais dézoomer plus que ce facteur même si les nœuds sont très éloignés les uns des autres.")]
    public float fitMinZoom = 0.4f;
    [Tooltip("Ne jamais zoomer plus que ce facteur lors du fit initial, même si un seul nœud est visible.")]
    public float fitMaxZoom = 1f;

    [Header("Pan")]
    [Tooltip("Marge de pan autorisée au-delà des bords du graphe, en pixels écran.")]
    public float panPadding = 150f;

    private RectTransform viewport;
    private Vector2 boundsMin;
    private Vector2 boundsMax;
    private bool boundsValid;

    void Awake()
    {
        viewport = (RectTransform)transform;
    }

    public void OnScroll(PointerEventData eventData)
    {
        float prevScale = content.localScale.x;
        float newScale = Mathf.Clamp(prevScale + eventData.scrollDelta.y * zoomSpeed, minZoom, maxZoom);
        if (Mathf.Approximately(newScale, prevScale)) return;

        // Zoom centré sur le curseur : le point du graphe sous la souris reste fixe à l'écran.
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            viewport, eventData.position, eventData.pressEventCamera, out Vector2 localPoint);

        Vector2 contentSpacePoint = (localPoint - content.anchoredPosition) / prevScale;

        content.localScale = new Vector3(newScale, newScale, 1f);
        content.anchoredPosition = localPoint - contentSpacePoint * newScale;

        ClampPosition();
    }

    public void OnDrag(PointerEventData eventData)
    {
        content.anchoredPosition += eventData.delta;
        ClampPosition();
    }

    // Recadre la vue pour que tous les RectTransform passés (nœuds actuellement révélés)
    // soient visibles, centrés, avec une marge. À appeler à l'ouverture de la fenêtre
    // et, si besoin, depuis un bouton "Ajuster la vue".
    public void FitToView(IEnumerable<RectTransform> nodes)
    {
        bool any = false;
        Vector2 min = Vector2.zero, max = Vector2.zero;

        foreach (RectTransform node in nodes)
        {
            Vector2 pos = node.anchoredPosition;
            if (!any) { min = max = pos; any = true; }
            else
            {
                min = Vector2.Min(min, pos);
                max = Vector2.Max(max, pos);
            }
        }

        if (!any)
        {
            content.localScale = Vector3.one;
            content.anchoredPosition = Vector2.zero;
            boundsValid = false;
            return;
        }

        Vector2 padding = new Vector2(fitPadding, fitPadding);
        boundsMin = min - padding;
        boundsMax = max + padding;
        boundsValid = true;

        Vector2 boundsSize = boundsMax - boundsMin;
        Vector2 boundsCenter = (boundsMin + boundsMax) * 0.5f;
        Vector2 viewportSize = viewport.rect.size;

        float scaleX = boundsSize.x > 1f ? viewportSize.x / boundsSize.x : fitMaxZoom;
        float scaleY = boundsSize.y > 1f ? viewportSize.y / boundsSize.y : fitMaxZoom;
        float fitScale = Mathf.Clamp(Mathf.Min(scaleX, scaleY), fitMinZoom, fitMaxZoom);

        content.localScale = new Vector3(fitScale, fitScale, 1f);
        content.anchoredPosition = -boundsCenter * fitScale;
    }

    private void ClampPosition()
    {
        if (!boundsValid) return;

        float scale = content.localScale.x;
        Vector2 halfBounds = (boundsMax - boundsMin) * 0.5f * scale;
        Vector2 center = (boundsMin + boundsMax) * 0.5f * scale;

        Vector2 pos = content.anchoredPosition;
        pos.x = Mathf.Clamp(pos.x, -center.x - halfBounds.x - panPadding, -center.x + halfBounds.x + panPadding);
        pos.y = Mathf.Clamp(pos.y, -center.y - halfBounds.y - panPadding, -center.y + halfBounds.y + panPadding);
        content.anchoredPosition = pos;
    }
}