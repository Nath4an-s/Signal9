using UnityEngine;
using UnityEngine.UI;
using TMPro;

// À poser sur DocumentWindowRoot (même pattern que GraphWindowRoot / ImageWindowRoot /
// FaceAnalysisWindowRoot : le script vit sur le Root, qui est le vrai frère de CaseWindow/
// DatabaseWindowFrame sous WindowsLayer ; DocumentWindowFrame, lui, est fils unique du Root).
public class DocumentViewerController : MonoBehaviour
{
    public static DocumentViewerController Instance { get; private set; }

    [Tooltip("DocumentWindowFrame — le panel visuel à afficher/masquer.")]
    public GameObject windowRoot;

    public TMP_Text titleText;

    [Tooltip("Texte du corps du document, dans un Content sous un ScrollRect (contenu potentiellement long).")]
    public TMP_Text bodyText;

    void Awake()
    {
        Instance = this;

        // Vérifie une bonne fois pour toutes que les 3 références sont bien câblées dans
        // l'Inspector — un champ oublié plante silencieusement plus loin sinon.
        if (windowRoot == null) Debug.LogError("[DocumentViewer] AWAKE : windowRoot non assigné dans l'Inspector.", this);
        if (titleText == null) Debug.LogError("[DocumentViewer] AWAKE : titleText non assigné dans l'Inspector.", this);
        if (bodyText == null) Debug.LogError("[DocumentViewer] AWAKE : bodyText non assigné dans l'Inspector.", this);
        else if (bodyText.font == null) Debug.LogError("[DocumentViewer] AWAKE : bodyText n'a AUCUN Font Asset assigné — le texte ne peut pas s'afficher, quel que soit le layout.", bodyText);
    }

    public void Open(string fileName, string content)
    {
        Debug.Log($"[DocumentViewer] Open('{fileName}') appelé. Longueur du content reçu : " +
                   (content == null ? "null" : content.Length.ToString()));

        if (string.IsNullOrEmpty(content))
        {
            Debug.LogWarning($"[DocumentViewer] Aucun contenu texte disponible pour {fileName} — " +
                              "vérifiez CaseWindowUI > Text Mappings (entrée manquante ou champ Content vide). " +
                              "La fenêtre ne s'ouvre pas du tout dans ce cas (return anticipé).");
            return;
        }

        titleText.text = fileName;
        bodyText.text = content;

        Debug.Log($"[DocumentViewer] bodyText.text assigné, {bodyText.text.Length} caractères. " +
                   $"enabled={bodyText.enabled}, color={bodyText.color}, fontAsset={(bodyText.font != null ? bodyText.font.name : "NULL")}, " +
                   $"fontSize={bodyText.fontSize}");

        windowRoot.SetActive(true);
        transform.SetAsLastSibling();

        Debug.Log($"[DocumentViewer] windowRoot.activeInHierarchy={windowRoot.activeInHierarchy}, " +
                   $"DocumentWindowRoot sibling index={transform.GetSiblingIndex()}");

        // Les ContentSizeFitter imbriqués (Label_DocBody -> Content) ne se recalculent pas
        // toujours de façon fiable en une seule frame quand le texte change par script — bug
        // connu d'Unity. On force explicitement le recalcul, du plus profond (le texte) vers
        // le parent (Content), pour garantir la bonne hauteur dès l'ouverture.
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(bodyText.rectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)bodyText.transform.parent);

        RectTransform bodyRect = bodyText.rectTransform;
        RectTransform contentRect = (RectTransform)bodyText.transform.parent;

        Debug.Log($"[DocumentViewer] APRÈS rebuild — Label_DocBody: " +
                   $"sizeDelta={bodyRect.sizeDelta}, anchoredPos={bodyRect.anchoredPosition}, " +
                   $"pivot={bodyRect.pivot}, rect={bodyRect.rect}, " +
                   $"lossyScale={bodyRect.lossyScale}");

        Debug.Log($"[DocumentViewer] APRÈS rebuild — Content: " +
                   $"sizeDelta={contentRect.sizeDelta}, rect={contentRect.rect}");

        Vector3[] corners = new Vector3[4];
        bodyRect.GetWorldCorners(corners);
        Debug.Log($"[DocumentViewer] Coins monde de Label_DocBody : " +
                   $"bas-gauche={corners[0]}, haut-gauche={corners[1]}, haut-droite={corners[2]}, bas-droite={corners[3]}. " +
                   "Comparez avec les coins de DocumentWindowFrame/Screen — si ces coordonnées sont " +
                   "loin hors de l'écran ou toutes identiques (rect plat), le texte est mal positionné " +
                   "ou de taille nulle malgré sizeDelta.");
    }

    public void Close()
    {
        windowRoot.SetActive(false);
    }
}