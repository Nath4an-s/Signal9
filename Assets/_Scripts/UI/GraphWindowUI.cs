using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GraphWindowUI : MonoBehaviour
{
    [Tooltip("Le panel visuel de la fenêtre (GraphWindowFrame), PAS le GameObject qui porte ce script.")]
    public GameObject windowFrame;

    [Tooltip("Composant sur GraphViewport, gère le zoom/pan et le fit-to-view.")]
    public GraphViewController viewController;

    [Header("Focus Sidebar")]
    [Tooltip("Btn_Graphe — activé/mis en avant à l'ouverture du graphe. Laisser vide pour désactiver.")]
    public SidebarTabButton linkedTab;

    [Header("Layers (EdgesLayer doit être AVANT NodesLayer dans la Hierarchy, tous deux enfants de GraphContent)")]
    public RectTransform edgesLayer;
    public RectTransform nodesLayer;

    [Header("Prefabs")]
    public GraphNodeUI nodePrefab;
    public GraphEdgeUI edgePrefab;

    private readonly Dictionary<string, GraphNodeUI> nodesById = new Dictionary<string, GraphNodeUI>();

    void Awake()
    {
        GraphManager.Instance.RegisterWindowUI(this);
    }

    // Le TIMING de l'ouverture (délai avant d'ouvrir, avant d'animer les révélations) est décidé
    // par GraphManager, pas ici — voir GraphManager.RevealFor(). Ce script ne fait qu'exécuter
    // ce qu'on lui demande, sans logique de délai propre.
    public void Open()
    {
        windowFrame.SetActive(true);

        // Ce script est posé sur GraphWindowRoot, le vrai frère de CaseWindow/DatabaseWindowFrame
        // sous WindowsLayer (GraphWindowFrame est fils unique de GraphWindowRoot — le réordonner
        // n'aurait aucun effet). Si un onglet Sidebar est lié, Activate() gère déjà ce
        // SetAsLastSibling ; sinon on le fait nous-même.
        if (linkedTab != null && SidebarTabController.Instance != null)
            SidebarTabController.Instance.Activate(linkedTab);
        else
            transform.SetAsLastSibling();

        // Vue par défaut : tout le graphe actuellement révélé visible, centré.
        FitToView();
    }

    public void Close()
    {
        windowFrame.SetActive(false);
    }

    // Recentre/redimensionne la vue sur les nœuds actuellement révélés.
    // Appelée automatiquement à l'ouverture ; peut aussi être branchée sur un bouton
    // "Ajuster la vue" si vous en ajoutez un à la TitleBar plus tard.
    public void FitToView()
    {
        if (viewController == null)
        {
            Debug.LogWarning("[Graph] viewController non assigné sur GraphWindowUI — zoom/pan désactivé.");
            return;
        }

        viewController.FitToView(nodesById.Values.Select(n => n.RectTransform));
    }

    // Appelée par GraphManager — jamais directement par autre chose.
    public void RevealNode(GraphNodeData data, bool animate)
    {
        if (nodesById.ContainsKey(data.id)) return;

        GraphNodeUI instance = Instantiate(nodePrefab, nodesLayer);
        instance.Setup(data, animate);
        nodesById[data.id] = instance;
    }

    // Appelée par GraphManager — jamais directement par autre chose.
    public void RevealEdge(GraphEdgeData data, bool animate)
    {
        if (!nodesById.TryGetValue(data.from, out var fromNode) ||
            !nodesById.TryGetValue(data.to, out var toNode))
        {
            Debug.LogWarning($"[Graph] Impossible d'afficher le lien {data.id} : nœud UI introuvable ({data.from} / {data.to}).");
            return;
        }

        GraphEdgeUI instance = Instantiate(edgePrefab, edgesLayer);
        instance.Setup(fromNode.RectTransform, toNode.RectTransform, data.correctness, data.label, animate);
    }
}