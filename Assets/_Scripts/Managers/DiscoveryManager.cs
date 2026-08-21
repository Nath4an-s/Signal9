using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// À attacher sur le même GameObject que GameManager / CaseManager / DatabaseManager / FaceDatabaseManager.
public class DiscoveryManager : MonoBehaviour
{
    public static DiscoveryManager Instance { get; private set; }

    private readonly HashSet<string> unlocked = new HashSet<string>();

    void Awake()
    {
        Instance = this;
    }

    public void Unlock(string discoveryId)
    {
        if (string.IsNullOrEmpty(discoveryId)) return;

        if (unlocked.Add(discoveryId))
        {
            Debug.Log($"[Discovery] {discoveryId} débloqué.");
            GraphManager.Instance?.RevealFor(discoveryId);
            CheckCaseCompletion();
        }
    }

    public bool IsUnlocked(string discoveryId) => unlocked.Contains(discoveryId);

    private void CheckCaseCompletion()
    {
        // Confirmé contre CaseManager.cs : CurrentCase expose bien le CaseData chargé.
        CaseData current = CaseManager.Instance.CurrentCase;
        if (current == null || current.requiredDiscoveries == null) return;

        bool allFound = current.requiredDiscoveries.All(id => unlocked.Contains(id));
        if (allFound)
        {
            Debug.Log($"[Case] {current.caseId} résolu.");
            CaseWindowUI.Instance?.MarkCaseResolved();
        }
    }
}