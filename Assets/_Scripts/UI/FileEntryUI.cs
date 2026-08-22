using UnityEngine;
using UnityEngine.UI;

public class FileEntryUI : MonoBehaviour
{
    public Sprite associatedImage;
    private string fileName;
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        if (button != null)
            button.onClick.AddListener(OnClick);
    }

    public void Setup(string name, Sprite sprite)
    {
        fileName = name;
        associatedImage = sprite;
    }

    void OnClick()
    {
        if (fileName.EndsWith(".jpg") || fileName.EndsWith(".png"))
        {
            ImageViewerController.Instance.Open(fileName, associatedImage);
        }
        else if (fileName.EndsWith(".pdf") || fileName.EndsWith(".txt"))
        {
            string content = CaseWindowUI.Instance != null ? CaseWindowUI.Instance.GetTextForFile(fileName) : null;
            DocumentViewerController.Instance.Open(fileName, content);
        }
        else
        {
            Debug.Log($"Ouverture de {fileName} pas encore implémentée (vidéo/audio).");
        }
    }
}