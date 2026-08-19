using UnityEngine;
using System.Linq;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance { get; private set; }

    private DatabaseRecord[] allRecords;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        LoadDatabase();
    }

    void LoadDatabase()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("database_plates");

        if (jsonFile == null)
        {
            Debug.LogError("Database file not found: database_plates");
            return;
        }

        DatabaseRecordList wrapper = JsonUtility.FromJson<DatabaseRecordList>(jsonFile.text);
        allRecords = wrapper.records;
        Debug.Log($"Database loaded: {allRecords.Length} records.");
    }

    public DatabaseRecord Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query) || allRecords == null)
            return null;

        string normalizedQuery = NormalizePlate(query);

        return allRecords.FirstOrDefault(r => NormalizePlate(r.plate) == normalizedQuery);
    }

    string NormalizePlate(string plate)
    {
        return plate.Trim().ToUpper().Replace("-", "").Replace(" ", "");
    }
}