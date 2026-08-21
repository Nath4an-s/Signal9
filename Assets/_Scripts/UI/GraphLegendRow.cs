using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Une ligne de la légende du graphe : une pastille de couleur + le nom du type.
// Prefab à créer dans l'Éditeur : un petit rectangle avec un enfant "Swatch" (Image, ~10x10)
// et un enfant "Label" (TMP_Text), en Horizontal Layout Group.
public class GraphLegendRow : MonoBehaviour
{
    public Image swatch;
    public TMP_Text label;

    public void Setup(string displayName, Color color)
    {
        swatch.color = color;
        label.text = displayName;
    }
}