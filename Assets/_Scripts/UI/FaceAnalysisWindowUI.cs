using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class FaceAnalysisWindowUI : MonoBehaviour
{
    public static FaceAnalysisWindowUI Instance { get; private set; }

    public GameObject windowRoot;
    public GameObject analyzingPanel;
    public GameObject resultPanel;
    public TMP_Text matchPercentText;
    public TMP_Text identityText;
    public TMP_Text notesText;

    [Header("Scan bar")]
    [Tooltip("Barre de progression (Image, Image Type = Filled, Horizontal).")]
    public Image scanBar;

    [Tooltip("Petit repère lumineux qui se déplace le long de la barre (optionnel).")]
    public RectTransform scanHead;

    [Tooltip("Texte affichant le pourcentage (ex: 00% -> 100%).")]
    public TMP_Text percentText;

    [Tooltip("Texte de statut qui change de phrase par palier (remplace l'ancien texte à points).")]
    public TMP_Text statusText;

    public float analysisDuration = 3f;

    // Phrases affichées par palier de progression — ton "logiciel professionnel", pas de fioritures.
    private static readonly string[] StatusPhrases =
    {
        "ANALYSE DES POINTS FACIAUX...",
        "RECOUPEMENT BASE DE DONNÉES...",
        "CALCUL DU TAUX DE CORRESPONDANCE..."
    };

    void Awake()
    {
        Instance = this;
    }

    public void Open(string faceId)
    {
        windowRoot.SetActive(true);
        windowRoot.transform.SetAsLastSibling();
        analyzingPanel.SetActive(true);
        resultPanel.SetActive(false);

        if (scanBar != null)
            scanBar.fillAmount = 0f;

        if (percentText != null)
            percentText.text = "00%";

        if (statusText != null)
            statusText.text = StatusPhrases[0];

        StartCoroutine(RunAnalysis(faceId));
    }

    private IEnumerator RunAnalysis(string faceId)
    {
        float elapsed = 0f;

        while (elapsed < analysisDuration)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsed / analysisDuration);

            if (scanBar != null)
                scanBar.fillAmount = progress;

            if (scanHead != null)
            {
                float trackWidth = scanBar.rectTransform.rect.width;
                Vector2 pos = scanHead.anchoredPosition;
                pos.x = trackWidth * progress;
                scanHead.anchoredPosition = pos;
            }

            if (percentText != null)
                percentText.text = $"{Mathf.RoundToInt(progress * 100f):00}%";

            if (statusText != null)
            {
                int phraseIndex = Mathf.Min((int)(progress * StatusPhrases.Length), StatusPhrases.Length - 1);
                statusText.text = StatusPhrases[phraseIndex];
            }

            yield return null;
        }

        FaceRecord record = FaceDatabaseManager.Instance.GetMatch(faceId);

        analyzingPanel.SetActive(false);
        resultPanel.SetActive(true);

        if (record == null)
        {
            matchPercentText.text = "MATCH: 0%";
            identityText.text = "IDENTITY: UNKNOWN";
            notesText.text = "Aucune correspondance trouvée.";
        }
        else
        {
            matchPercentText.text = $"MATCH: {record.matchPercent}%";
            identityText.text = $"IDENTITY: {record.identityName}";
            notesText.text = record.notes;

            // Consigne la découverte pour la validation du case (cf. DiscoveryManager).
            DiscoveryManager.Instance?.Unlock(faceId == "reflet_salon" ? "reflection_face" : faceId);
        }
    }

    public void Close()
    {
        windowRoot.SetActive(false);
    }
}