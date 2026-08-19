using UnityEngine;
using UnityEngine.UI;

public class PhotoHotspot : MonoBehaviour
{
    public string faceId;

    [Tooltip("Nom du fichier image sur lequel ce hotspot doit être cliquable (ex: SALON_PHOTO.jpg).")]
    public string requiredFileName;

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnClick);
            button.interactable = false; // inactif tant qu'on n'a pas confirmé qu'on est sur la bonne image
        }
    }

    // Appelé par ImageViewerController à chaque ouverture d'image.
    public void Refresh(string currentFileName)
    {
        if (button == null) return;
        button.interactable = !string.IsNullOrEmpty(requiredFileName) && currentFileName == requiredFileName;
    }

    void OnClick()
    {
        if (button != null && !button.interactable) return; // garde-fou si appelé autrement que via clic UI
        FaceAnalysisWindowUI.Instance.Open(faceId);
    }
}