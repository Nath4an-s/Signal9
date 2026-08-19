using UnityEngine;
using TMPro;

public class CaseWindowUI : MonoBehaviour
{
    [Header("Title bar")]
    public TMP_Text titleText;

    [Header("Column left")]
    public TMP_Text locationValue;
    public TMP_Text statusValue;
    public TMP_Text agentValue;

    [Header("Column right")]
    public Transform filesContainer;
    public GameObject fileEntryPrefab;

    [Header("Image mapping (temporary manual mapping)")]
    public string testImageFileName = "HOUSE_PHOTO.jpg";
    public Sprite testImageSprite;

    void Start()
    {
        if (CaseManager.Instance == null || CaseManager.Instance.CurrentCase == null)
        {
            Debug.LogError("No case loaded yet.");
            return;
        }

        DisplayCase(CaseManager.Instance.CurrentCase);
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
                Sprite matchingSprite = (fileName == testImageFileName) ? testImageSprite : null;
                entryUI.Setup(fileName, matchingSprite);
            }
        }
    }

    string ExtractCaseNumber(string caseId)
    {
        string[] parts = caseId.Split('_');
        return parts.Length > 1 ? parts[1] : caseId;
    }
}