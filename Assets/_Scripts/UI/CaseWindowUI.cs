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

    Sprite GetSpriteForFile(string fileName)
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