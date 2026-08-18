using UnityEngine;

public class CaseManager : MonoBehaviour
{
    public static CaseManager Instance { get; private set; }

    public CaseData CurrentCase { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        LoadCase("case_0017");
    }

    public void LoadCase(string caseFileName)
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(caseFileName);

        if (jsonFile == null)
        {
            Debug.LogError($"Case file not found: {caseFileName}");
            return;
        }

        CurrentCase = JsonUtility.FromJson<CaseData>(jsonFile.text);
        Debug.Log($"Case loaded: {CurrentCase.title} ({CurrentCase.caseId})");
    }
}