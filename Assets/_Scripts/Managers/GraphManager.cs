using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// À attacher sur le même GameObject que GameManager / CaseManager / DatabaseManager / FaceDatabaseManager / DiscoveryManager.
public class GraphManager : MonoBehaviour
{
    public static GraphManager Instance { get; private set; }

    [Tooltip("Nom du fichier JSON (sans extension) dans _Data/Resources/.")]
    public string graphFileName = "graph_case_0017";

    public GraphData Data { get; private set; }

    private readonly HashSet<string> revealedNodes = new HashSet<string>();
    private readonly HashSet<string> revealedEdges = new HashSet<string>();

    // Assignée par GraphWindowUI elle-même via RegisterWindowUI() — GraphManager ne connaît
    // aucun objet de scène en dur, il attend juste que la fenêtre s'annonce.
    private GraphWindowUI windowUI;

    void Awake()
    {
        Instance = this;
        LoadGraph();
    }

    void Start()
    {
        RevealInitialNodesAndEdges();
    }

    private void LoadGraph()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(graphFileName);

        if (jsonFile == null)
        {
            Debug.LogError($"Graph file not found: {graphFileName}");
            Data = new GraphData { nodes = new GraphNodeData[0], edges = new GraphEdgeData[0] };
            return;
        }

        Data = JsonUtility.FromJson<GraphData>(jsonFile.text);
    }

    // Appelée par GraphWindowUI dans son propre Awake(). Rejoue tout ce qui a déjà été
    // révélé avant que la fenêtre existe (le joueur peut avoir avancé dans l'affaire
    // avant d'ouvrir le Graphe pour la première fois).
    public void RegisterWindowUI(GraphWindowUI ui)
    {
        windowUI = ui;

        foreach (var node in Data.nodes)
        {
            if (revealedNodes.Contains(node.id))
                windowUI.RevealNode(node, animate: false);
        }

        foreach (var edge in Data.edges)
        {
            if (revealedEdges.Contains(edge.id))
                windowUI.RevealEdge(edge, animate: false);
        }
    }

    private void RevealInitialNodesAndEdges()
    {
        foreach (var node in Data.nodes)
        {
            if (string.IsNullOrEmpty(node.revealedBy))
                RevealNode(node);
        }

        foreach (var edge in Data.edges)
        {
            if (string.IsNullOrEmpty(edge.revealedBy))
                TryRevealEdge(edge);
        }
    }

    [Header("Délais (déclenchés par une découverte en cours de partie — pas au lancement)")]
    [Tooltip("Temps d'attente après une découverte (Unlock) avant d'ouvrir/focus la fenêtre du graphe.")]
    public float delayBeforeOpen = 2f;
    [Tooltip("Temps d'attente supplémentaire, fenêtre déjà ouverte, avant de lancer l'animation des nouveaux nœuds/liens.")]
    public float delayBeforeReveal = 1f;

    // Point d'entrée appelé par DiscoveryManager.Unlock(). Différé : la fenêtre du graphe ne
    // doit pas s'ouvrir instantanément quand le joueur trouve une plaque ou réussit une analyse
    // faciale — on lui laisse le temps de terminer ce qu'il regardait avant de basculer son
    // attention vers le graphe qui se dessine.
    public void RevealFor(string discoveryId)
    {
        if (Data == null) return;

        List<GraphNodeData> nodesToReveal = Data.nodes.Where(n => n.revealedBy == discoveryId).ToList();
        List<GraphEdgeData> edgesToReveal = Data.edges.Where(e => e.revealedBy == discoveryId).ToList();

        if (nodesToReveal.Count == 0 && edgesToReveal.Count == 0) return;

        StartCoroutine(RevealForDelayed(nodesToReveal, edgesToReveal));
    }

    private IEnumerator RevealForDelayed(List<GraphNodeData> nodes, List<GraphEdgeData> edges)
    {
        yield return new WaitForSeconds(delayBeforeOpen);

        // Ouvre/focus la fenêtre AVANT que les nouveaux éléments existent : le joueur voit
        // l'état actuel du graphe d'abord, puis les nouveautés apparaître juste après.
        windowUI?.Open();

        yield return new WaitForSeconds(delayBeforeReveal);

        foreach (GraphNodeData node in nodes)
            RevealNode(node);

        // Les liens dépendent de leurs deux extrémités déjà révélées — nodes puis edges, dans
        // cet ordre, exactement comme RevealInitialNodesAndEdges().
        foreach (GraphEdgeData edge in edges)
            TryRevealEdge(edge);
    }

    private void RevealNode(GraphNodeData node)
    {
        if (!revealedNodes.Add(node.id)) return; // déjà révélé, rien à faire

        Debug.Log($"[Graph] Nœud révélé : {node.id}");
        windowUI?.RevealNode(node, animate: true);
    }

    private void TryRevealEdge(GraphEdgeData edge)
    {
        if (revealedEdges.Contains(edge.id)) return;

        if (!revealedNodes.Contains(edge.from) || !revealedNodes.Contains(edge.to))
        {
            Debug.LogWarning($"[Graph] Lien {edge.id} ignoré : nœud(s) {edge.from}/{edge.to} pas encore révélé(s). " +
                              "Vérifie l'ordre des champs 'revealedBy' dans le JSON (un lien ne peut apparaître qu'après ses deux extrémités).");
            return;
        }

        revealedEdges.Add(edge.id);
        Debug.Log($"[Graph] Lien révélé : {edge.id}");
        windowUI?.RevealEdge(edge, animate: true);
    }
}