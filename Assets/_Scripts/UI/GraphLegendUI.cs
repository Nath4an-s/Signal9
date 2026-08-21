using UnityEngine;

// Génère automatiquement la légende (couleur -> type de nœud) à l'ouverture du graphe,
// à partir de GraphNodeUI.TypePalette — source unique, ne peut pas se désynchroniser
// des couleurs réellement affichées sur les nœuds.
public class GraphLegendUI : MonoBehaviour
{
    [Tooltip("Prefab GraphLegendRow (pastille + texte).")]
    public GraphLegendRow rowPrefab;

    [Tooltip("Conteneur avec un Vertical Layout Group, où les lignes sont instanciées.")]
    public RectTransform container;

    private bool built;

    void OnEnable()
    {
        if (built) return; // la légende est statique, pas besoin de la reconstruire à chaque ouverture
        built = true;

        foreach (var entry in GraphNodeUI.TypePalette)
        {
            GraphLegendRow row = Instantiate(rowPrefab, container);
            row.Setup(entry.displayName, entry.color);
        }
    }
}