using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CaseWindowUI : MonoBehaviour
{
    public static CaseWindowUI Instance { get; private set; }

    private static readonly Color ResolvedColor = new Color32(0x4A, 0x7F, 0xB5, 0xFF); // accent bleu-gris, cf. Section 32 du GDD

    [Header("Title bar")]
    public TMP_Text titleText;

    [Header("Column left")]
    public TMP_Text locationValue;
    public TMP_Text statusValue;
    public TMP_Text agentValue;

    [Header("Column right")]
    public Transform filesContainer;
    public GameObject fileEntryPrefab;

    [System.Serializable]
    public class ImageMapping
    {
        public string fileName;
        public Sprite sprite;
    }

    [Header("Image mapping (temporary manual mapping)")]
    public ImageMapping[] imageMappings;

    [System.Serializable]
    public class TextMapping
    {
        public string fileName;
        [TextArea(4, 20)]
        public string content;
    }

    [Header("Text mapping (rapports, documents — POLICE_REPORT.pdf, etc.)")]
    public TextMapping[] textMappings;

    private CaseData currentCaseData;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (CaseManager.Instance == null || CaseManager.Instance.CurrentCase == null)
        {
            Debug.LogError("No case loaded yet.");
            return;
        }

        currentCaseData = CaseManager.Instance.CurrentCase;
        DisplayCase(currentCaseData);
    }

    public void DisplayCase(CaseData data)
    {
        titleText.text = $"CASE #{ExtractCaseNumber(data.caseId)} — {data.title}";
        locationValue.text = data.location;
        statusValue.text = data.status;
        agentValue.text = data.assignedAgent;

        foreach (Transform child in filesContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (string fileName in data.attachedFiles)
        {
            GameObject entry = Instantiate(fileEntryPrefab, filesContainer);

            TMP_Text entryText = entry.GetComponentInChildren<TMP_Text>();
            if (entryText != null)
            {
                entryText.text = fileName;
            }

            FileEntryUI entryUI = entry.GetComponent<FileEntryUI>();
            if (entryUI != null)
            {
                Sprite matchingSprite = GetSpriteForFile(fileName);
                entryUI.Setup(fileName, matchingSprite);
            }
        }
    }

    // Rendue publique (était privée) : ImageViewerController en a besoin pour résoudre le
    // sprite d'un fichier ouvert autrement que par un clic sur FileEntryUI (sélecteur du
    // header, ouverture par défaut depuis Btn_AnalysePhoto).
    public Sprite GetSpriteForFile(string fileName)
    {
        foreach (ImageMapping mapping in imageMappings)
        {
            if (mapping.fileName == fileName)
            {
                return mapping.sprite;
            }
        }
        return null;
    }

    // Symétrique de GetSpriteForFile, pour DocumentViewerController (POLICE_REPORT.pdf, etc.).
    public string GetTextForFile(string fileName)
    {
        foreach (TextMapping mapping in textMappings)
        {
            if (mapping.fileName == fileName)
            {
                return mapping.content;
            }
        }
        return null;
    }

    // Fichiers image du dossier courant, dans l'ordre de attachedFiles — alimente le
    // sélecteur du header de l'Image Viewer.
    public string[] GetImageFileNames()
    {
        if (currentCaseData == null) return new string[0];

        var result = new List<string>();
        foreach (string fileName in currentCaseData.attachedFiles)
        {
            if (fileName.EndsWith(".jpg") || fileName.EndsWith(".png"))
                result.Add(fileName);
        }
        return result.ToArray();
    }

    // Utilisée par Btn_AnalysePhoto (Sidebar) pour ouvrir une image par défaut.
    public string GetFirstImageFileName()
    {
        string[] images = GetImageFileNames();
        return images.Length > 0 ? images[0] : null;
    }

    // Appelé par DiscoveryManager une fois toutes les requiredDiscoveries débloquées.
    public void MarkCaseResolved()
    {
        if (currentCaseData == null) return;

        currentCaseData.status = "RÉSOLU";
        statusValue.text = currentCaseData.status;
        statusValue.color = ResolvedColor;
    }

    string ExtractCaseNumber(string caseId)
    {
        string[] parts = caseId.Split('_');
        return parts.Length > 1 ? parts[1] : caseId;
    }
}