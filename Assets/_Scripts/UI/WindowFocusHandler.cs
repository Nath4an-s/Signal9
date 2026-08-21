using UnityEngine;
using UnityEngine.EventSystems;

// À attacher sur l'objet racine visuel de CHAQUE fenêtre (CaseWindow, DatabaseWindowFrame,
// GraphWindowFrame, ImageWindowFrame, FaceAnalysisWindowFrame...).
//
// Grâce au bubbling natif d'Unity : si l'objet cliqué (ex. la TitleBar, un fond de panel)
// n'implémente pas IPointerDownHandler lui-même, l'événement remonte automatiquement jusqu'au
// premier ancêtre qui l'implémente. Ce script sur la racine suffit donc à capter un clic
// n'importe où dans la fenêtre — pas besoin de le dupliquer sur chaque enfant, et aucun
// conflit avec WindowDragHandler (qui ne gère que le drag, pas OnPointerDown).
public class WindowFocusHandler : MonoBehaviour, IPointerDownHandler
{
    [Tooltip("Objet dont l'ORDRE PARMI SES FRÈRES SOUS WindowsLayer détermine le premier plan.\n" +
             "— CaseWindow / DatabaseWindowFrame : laisser vide, c'est cet objet lui-même.\n" +
             "— ImageWindowFrame / FaceAnalysisWindowFrame / GraphWindowFrame : PAS le Frame — " +
             "assignez le wrapper (ImageWindowRoot / FaceAnalysisWindowRoot / GraphWindowRoot), " +
             "car le Frame est fils unique de son Root et SetAsLastSibling() n'aurait aucun effet dessus.")]
    public Transform frontTarget;

    [Tooltip("Onglet Sidebar associé à cette fenêtre (ex: Btn_Dossiers pour CaseWindow, " +
             "Btn_Graphe pour GraphWindowFrame). Laisser vide pour les fenêtres sans onglet " +
             "dédié (Image Viewer, Analyse faciale) : elles passent juste au premier plan " +
             "localement, sans changer l'onglet actif de la Sidebar.")]
    public SidebarTabButton linkedTab;

    void Awake()
    {
        if (frontTarget == null) frontTarget = transform;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (linkedTab != null)
        {
            SidebarTabController.Instance.Activate(linkedTab);
        }
        else
        {
            frontTarget.SetAsLastSibling();
        }
    }
}