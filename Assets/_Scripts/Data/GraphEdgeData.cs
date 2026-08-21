using System;

[Serializable]
public class GraphEdgeData
{
    public string id;
    public string from;   // id d'un GraphNodeData
    public string to;     // id d'un GraphNodeData
    public string correctness; // "correct" / "red_herring" / "ambiguous"

    // Texte affiché le long du lien, ex: "possède le véhicule", "présent sur les lieux",
    // "correspondance faciale 97.8%". Sans ça, un lien ne dit rien de sa nature au joueur.
    public string label;

    // Id du discovery (DiscoveryManager) qui révèle ce lien. Vide = visible dès l'ouverture du graphe.
    public string revealedBy;
}