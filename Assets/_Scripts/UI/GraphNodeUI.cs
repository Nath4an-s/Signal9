using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GraphNodeUI : MonoBehaviour
{
    public RectTransform RectTransform => (RectTransform)transform;

    public Image background;
    public TMP_Text label;

    private const float RevealDuration = 0.3f;

    // Palette Section 32 : aplats sobres, pas de couleurs saturées — chaque type de nœud
    // se distingue par une teinte de gris/bleu-gris différente, pas par une couleur vive.
    private static readonly Color ColorPerson  = new Color32(0x4A, 0x7F, 0xB5, 0xFF); // accent
    private static readonly Color ColorPlace   = new Color32(0x5A, 0x5C, 0x5F, 0xFF);
    private static readonly Color ColorCompany = new Color32(0x8A, 0x6A, 0x4A, 0xFF);
    private static readonly Color ColorVehicle = new Color32(0x4A, 0x5C, 0x5F, 0xFF);
    private static readonly Color ColorCase    = new Color32(0x2A, 0x2B, 0x2D, 0xFF);
    private static readonly Color ColorFile    = new Color32(0x3A, 0x3B, 0x3D, 0xFF);

    // Source unique pour la couleur ET le nom affiché de chaque type — consommée à la fois par
    // ColorForType() ci-dessous et par GraphLegendUI, pour que la légende ne puisse jamais
    // désynchroniser d'avec les couleurs réellement utilisées sur les nœuds.
    public static readonly (string type, string displayName, Color color)[] TypePalette =
    {
        ("Person",  "Personne",   ColorPerson),
        ("Place",   "Lieu",       ColorPlace),
        ("Company", "Entreprise", ColorCompany),
        ("Vehicle", "Véhicule",   ColorVehicle),
        ("Case",    "Affaire",    ColorCase),
        ("File",    "Fichier",    ColorFile),
    };

    public void Setup(GraphNodeData data, bool animate)
    {
        RectTransform.anchoredPosition = new Vector2(data.x, data.y);
        label.text = data.label;
        background.color = ColorForType(data.type);

        // Les nœuds ne sont pas encore cliquables (création manuelle de liens = feature future,
        // Section 12 du GDD). Tant que ce n'est pas le cas, ils ne doivent pas intercepter les
        // événements de zoom/pan destinés à GraphViewController.
        background.raycastTarget = false;
        label.raycastTarget = false;

        if (animate && gameObject.activeInHierarchy)
        {
            RectTransform.localScale = Vector3.zero;
            StartCoroutine(ScaleIn());
        }
        else
        {
            RectTransform.localScale = Vector3.one;
        }
    }

    private IEnumerator ScaleIn()
    {
        float t = 0f;
        while (t < RevealDuration)
        {
            t += Time.deltaTime;
            float scale = Mathf.SmoothStep(0f, 1f, t / RevealDuration);
            RectTransform.localScale = new Vector3(scale, scale, 1f);
            yield return null;
        }
        RectTransform.localScale = Vector3.one;
    }

    private Color ColorForType(string type)
    {
        foreach (var entry in TypePalette)
        {
            if (entry.type == type) return entry.color;
        }
        return ColorPerson;
    }
}