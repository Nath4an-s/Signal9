using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using System.Text;
using System.IO;

public class SceneHierarchyExporter : EditorWindow
{
    [MenuItem("Tools/Export Scene Hierarchy")]
    public static void ExportHierarchy()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"# Scene export: {UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}");
        sb.AppendLine($"# Generated: {System.DateTime.Now}");
        sb.AppendLine();

        GameObject[] rootObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (GameObject root in rootObjects)
        {
            WriteObject(root.transform, sb, 0);
        }

        string path = Path.Combine(Application.dataPath, "..", "scene_export.txt");
        File.WriteAllText(path, sb.ToString());

        Debug.Log($"Scene exported to: {path}");
        EditorUtility.RevealInFinder(path);
    }

    static void WriteObject(Transform t, StringBuilder sb, int depth)
    {
        string indent = new string(' ', depth * 2);
        string activeMarker = t.gameObject.activeSelf ? "" : " [INACTIVE]";
        sb.AppendLine($"{indent}- {t.name}{activeMarker}");

        // RectTransform info (position/size in UI)
        RectTransform rt = t.GetComponent<RectTransform>();
        if (rt != null)
        {
            sb.AppendLine($"{indent}    RectTransform: anchoredPos={rt.anchoredPosition}, sizeDelta={rt.sizeDelta}, anchorMin={rt.anchorMin}, anchorMax={rt.anchorMax}");
        }

        // Components (excluding Transform itself)
        Component[] components = t.GetComponents<Component>();
        foreach (Component c in components)
        {
            if (c == null || c is Transform) continue;

            string extra = "";

            if (c is Image img)
                extra = $" (color={ColorUtility.ToHtmlStringRGBA(img.color)}, sprite={(img.sprite != null ? img.sprite.name : "none")})";
            else if (c is TMP_Text tmp)
                extra = $" (text=\"{Truncate(tmp.text, 40)}\", fontSize={tmp.fontSize}, color={ColorUtility.ToHtmlStringRGBA(tmp.color)})";
            else if (c is Button btn)
                extra = $" (onClick listeners={btn.onClick.GetPersistentEventCount()})";
            else if (c is TMP_InputField input)
                extra = $" (placeholder present={input.placeholder != null})";
            else if (c is VerticalLayoutGroup vlg)
                extra = $" (spacing={vlg.spacing}, controlChildWidth={vlg.childControlWidth}, controlChildHeight={vlg.childControlHeight})";
            else if (c is HorizontalLayoutGroup hlg)
                extra = $" (spacing={hlg.spacing}, controlChildWidth={hlg.childControlWidth}, controlChildHeight={hlg.childControlHeight})";
            else if (c is LayoutElement le)
                extra = $" (preferredWidth={le.preferredWidth}, preferredHeight={le.preferredHeight})";
            else if (c is Canvas canvas)
                extra = $" (renderMode={canvas.renderMode})";
            else if (c is MonoBehaviour mb && !(c is Image) && !(c is TMP_Text))
            {
                // Custom script - list public fields via reflection would be verbose;
                // just note the script name, already captured by GetType().Name below.
            }

            sb.AppendLine($"{indent}    [{c.GetType().Name}]{extra}");
        }

        foreach (Transform child in t)
        {
            WriteObject(child, sb, depth + 1);
        }
    }

    static string Truncate(string s, int maxLen)
    {
        if (string.IsNullOrEmpty(s)) return s;
        return s.Length <= maxLen ? s : s.Substring(0, maxLen) + "...";
    }
}