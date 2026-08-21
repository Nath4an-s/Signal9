using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GraphEdgeUI : MonoBehaviour
{
    public RectTransform RectTransform => (RectTransform)transform;

    public Image line;

    [Tooltip("Texte affiché le long du lien (ex: \"possède le véhicule\"). Enfant du même objet, " +
             "positionné/orienté automatiquement par Setup(). Peut être laissé vide dans le prefab.")]
    public TMP_Text relationLabel;

    private const float Thickness = 2f;
    private const float RevealDuration = 0.4f;

    private static readonly Color CorrectColor    = new Color32(0x4A, 0x7F, 0xB5, 0xFF); // accent bleu-gris
    private static readonly Color RedHerringColor = new Color32(0xC9, 0x8A, 0x4B, 0xFF); // orange sourd
    private static readonly Color AmbiguousColor  = new Color32(0x5A, 0x5C, 0x5F, 0xFF); // gris neutre

    // from/to : les RectTransform des GraphNodeUI déjà instanciés, dans le même espace
    // de coordonnées (EdgesLayer et NodesLayer doivent être positionnés de façon identique
    // dans GraphViewport — voir la fiche de montage Unity).
    public void Setup(RectTransform from, RectTransform to, string correctness, string relationshipLabel, bool animate)
    {
        line.color = ColorForCorrectness(correctness);
        line.raycastTarget = false; // ne doit pas intercepter le zoom/pan (GraphViewController)

        Vector2 fromPos = from.anchoredPosition;
        Vector2 toPos = to.anchoredPosition;
        Vector2 diff = toPos - fromPos;
        float targetLength = diff.magnitude;
        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        RectTransform.anchoredPosition = fromPos;
        RectTransform.localRotation = Quaternion.Euler(0, 0, angle);

        SetupLabel(relationshipLabel, targetLength, angle);

        if (animate && gameObject.activeInHierarchy)
        {
            RectTransform.sizeDelta = new Vector2(0f, Thickness);
            StartCoroutine(GrowLine(targetLength));
        }
        else
        {
            RectTransform.sizeDelta = new Vector2(targetLength, Thickness);
        }
    }

    // Place le label au milieu du lien et le contre-tourne pour qu'il reste horizontal et
    // lisible à l'écran, quel que soit l'angle du trait (jamais affiché "tête en bas").
    private void SetupLabel(string relationshipLabel, float length, float angle)
    {
        if (relationLabel == null) return;

        if (string.IsNullOrEmpty(relationshipLabel))
        {
            relationLabel.gameObject.SetActive(false);
            return;
        }

        relationLabel.gameObject.SetActive(true);
        relationLabel.text = relationshipLabel;

        RectTransform labelRect = relationLabel.rectTransform;
        labelRect.anchoredPosition = new Vector2(length * 0.5f, 0f);

        // counterAngle = -angle suffit : rotation totale affichée = angle (ligne) + counterAngle
        // (label) = 0, donc le texte reste toujours parfaitement horizontal à l'écran, quel que
        // soit l'angle du lien. Ne PAS ajouter de flip conditionnel ici : ça retournait le texte
        // à l'envers pour tous les liens orientés vers la gauche (bug corrigé).
        labelRect.localRotation = Quaternion.Euler(0, 0, -angle);
    }

    private IEnumerator GrowLine(float targetLength)
    {
        float t = 0f;
        while (t < RevealDuration)
        {
            t += Time.deltaTime;
            float width = Mathf.SmoothStep(0f, targetLength, t / RevealDuration);
            RectTransform.sizeDelta = new Vector2(width, Thickness);
            yield return null;
        }
        RectTransform.sizeDelta = new Vector2(targetLength, Thickness);
    }

    private Color ColorForCorrectness(string correctness)
    {
        switch (correctness)
        {
            case "correct": return CorrectColor;
            case "red_herring": return RedHerringColor;
            case "ambiguous": return AmbiguousColor;
            default: return CorrectColor;
        }
    }
}