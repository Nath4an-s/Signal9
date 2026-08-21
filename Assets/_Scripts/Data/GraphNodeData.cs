using System;

[Serializable]
public class GraphNodeData
{
    public string id;
    public string label;
    public string type;       // Person, Company, Place, Vehicle, Case, File...
    public float x;
    public float y;

    // Id du discovery (DiscoveryManager) qui révèle ce nœud. Vide = visible dès l'ouverture du graphe.
    public string revealedBy;
}