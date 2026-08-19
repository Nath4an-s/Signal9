using UnityEngine;
using TMPro;

public class DatabaseWindowUI : MonoBehaviour
{
    public TMP_InputField searchInput;

    public TMP_Text resultPlate;
    public TMP_Text resultOwner;
    public TMP_Text resultAddress;
    public TMP_Text resultVehicle;
    public TMP_Text resultNotes;
    public GameObject resultNotFound;
    public GameObject resultGroup;

    void Start()
    {
        ClearResults();
    }

    public void OnSearchButtonClicked()
    {
        DatabaseRecord result = DatabaseManager.Instance.Search(searchInput.text);

        if (result == null)
        {
            resultGroup.SetActive(false);
            resultNotFound.SetActive(true);
            return;
        }

        resultNotFound.SetActive(false);
        resultGroup.SetActive(true);

        resultPlate.text = $"Plaque : {result.plate}";
        resultOwner.text = $"Propriétaire : {result.ownerName}";
        resultAddress.text = $"Adresse : {result.ownerAddress}";
        resultVehicle.text = $"Véhicule : {result.vehicleModel}";
        resultNotes.text = $"Notes : {result.notes}";
    }

    void ClearResults()
    {
        resultGroup.SetActive(false);
        resultNotFound.SetActive(false);
    }
}
