using UnityEngine;

public class FaceDatabaseManager : MonoBehaviour
{
    public static FaceDatabaseManager Instance { get; private set; }

    private FaceRecord[] records;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        LoadDatabase();
    }

    private void LoadDatabase()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("face_database");

        if (jsonFile == null)
        {
            Debug.LogError("FaceDatabaseManager : face_database.json introuvable dans un dossier Resources.");
            records = new FaceRecord[0];
            return;
        }

        FaceRecordList list = JsonUtility.FromJson<FaceRecordList>(jsonFile.text);
        records = list.records;
    }

    // Retourne le FaceRecord correspondant, ou null si aucune correspondance.
    public FaceRecord GetMatch(string faceId)
    {
        foreach (FaceRecord record in records)
        {
            if (record.faceId == faceId)
            {
                return record;
            }
        }
        return null;
    }
}