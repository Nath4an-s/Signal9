using System.Collections.Generic;
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

    [Header("Sélecteur de fichier (header)")]
    [Tooltip("Dropdown TMP dans ImageTitleBar, listant les images du dossier actuel.")]
    public TMP_Dropdown fileSelector;

    [Header("Focus Sidebar")]
    [Tooltip("Btn_AnalysePhoto — activé/mis en avant à chaque ouverture d'image, quelle que " +
             "soit la façon dont elle a été ouverte (clic sur un fichier ou bouton Sidebar). " +
             "Laisser vide pour désactiver ce comportement.")]
    public SidebarTabButton linkedTab;

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

        // Ce script est posé sur ImageWindowRoot, le vrai frère de CaseWindow/DatabaseWindowFrame
        // sous WindowsLayer (ImageWindowFrame, lui, est fils unique de ImageWindowRoot — inutile
        // de le réordonner, ça n'aurait aucun effet). Si un onglet Sidebar est lié, Activate()
        // s'occupe déjà de ce SetAsLastSibling ; sinon on le fait nous-même.
        if (linkedTab != null && SidebarTabController.Instance != null)
            SidebarTabController.Instance.Activate(linkedTab);
        else
            transform.SetAsLastSibling();

        RefreshHotspots();
        PopulateFileSelector();
    }

    // Ouvre une image en résolvant son sprite via le mapping de CaseWindowUI — utile quand on
    // n'a que le nom de fichier (sélecteur du header, ouverture par défaut), sans passer par
    // FileEntryUI qui porte déjà le sprite associé.
    public void OpenByFileName(string fileName)
    {
        Sprite sprite = CaseWindowUI.Instance != null ? CaseWindowUI.Instance.GetSpriteForFile(fileName) : null;
        Open(fileName, sprite);
    }

    // Appelée par Btn_AnalysePhoto (Sidebar, via onActivate). Rouvre l'image déjà affichée s'il
    // y en a une, sinon ouvre la première image listée dans le dossier actuel.
    public void OpenDefault()
    {
        string fileName = !string.IsNullOrEmpty(CurrentFileName)
            ? CurrentFileName
            : (CaseWindowUI.Instance != null ? CaseWindowUI.Instance.GetFirstImageFileName() : null);

        if (fileName == null)
        {
            Debug.LogWarning("[ImageViewer] Aucune image dans les fichiers attachés du dossier actuel.");
            return;
        }

        OpenByFileName(fileName);
    }

    private void PopulateFileSelector()
    {
        if (fileSelector == null || CaseWindowUI.Instance == null) return;

        string[] files = CaseWindowUI.Instance.GetImageFileNames();

        var options = new List<TMP_Dropdown.OptionData>();
        int selectedIndex = 0;
        for (int i = 0; i < files.Length; i++)
        {
            options.Add(new TMP_Dropdown.OptionData(files[i]));
            if (files[i] == CurrentFileName) selectedIndex = i;
        }

        // Évite de redéclencher OpenByFileName() en repeuplant juste après avoir nous-même ouvert l'image.
        fileSelector.onValueChanged.RemoveListener(OnFileSelectorChanged);
        fileSelector.ClearOptions();
        fileSelector.AddOptions(options);
        fileSelector.SetValueWithoutNotify(selectedIndex);
        fileSelector.onValueChanged.AddListener(OnFileSelectorChanged);
    }

    private void OnFileSelectorChanged(int index)
    {
        if (CaseWindowUI.Instance == null) return;
        string[] files = CaseWindowUI.Instance.GetImageFileNames();
        if (index < 0 || index >= files.Length) return;
        OpenByFileName(files[index]);
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