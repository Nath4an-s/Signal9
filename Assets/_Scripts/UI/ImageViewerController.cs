using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ImageViewerController : MonoBehaviour
{
    public static ImageViewerController Instance { get; private set; }

    public GameObject windowRoot;
    public Image displayedImage;
    public TMP_Text titleText;
    public RectTransform imageRectTransform;

    public string CurrentFileName { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public void Open(string fileName, Sprite sprite)
    {
        if (sprite == null)
        {
            Debug.LogWarning($"No image available for {fileName}");
            return;
        }

        CurrentFileName = fileName;

        displayedImage.sprite = sprite;
        titleText.text = $"{fileName} — Image Viewer";

        imageRectTransform.localScale = Vector3.one;
        imageRectTransform.anchoredPosition = Vector2.zero;

        windowRoot.SetActive(true);

        RefreshHotspots();
    }

    // Réactive/désactive chaque PhotoHotspot enfant selon l'image actuellement affichée.
    private void RefreshHotspots()
    {
        var hotspots = displayedImage.GetComponentsInChildren<PhotoHotspot>(true);
        foreach (var hotspot in hotspots)
            hotspot.Refresh(CurrentFileName);
    }

    public void Close()
    {
        windowRoot.SetActive(false);
    }
}