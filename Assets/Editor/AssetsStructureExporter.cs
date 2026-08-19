using UnityEngine;
using UnityEditor;
using System.Text;
using System.IO;
using System.Linq;

public class AssetsStructureExporter : EditorWindow
{
    [MenuItem("Tools/Export Assets Structure")]
    public static void ExportAssetsStructure()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"# Assets structure export");
        sb.AppendLine($"# Generated: {System.DateTime.Now}");
        sb.AppendLine();

        string assetsPath = Application.dataPath;
        WriteFolder(assetsPath, sb, 0, "Assets");

        string outputPath = Path.Combine(Application.dataPath, "..", "assets_structure_export.txt");
        File.WriteAllText(outputPath, sb.ToString());

        Debug.Log($"Assets structure exported to: {outputPath}");
        EditorUtility.RevealInFinder(outputPath);
    }

    static void WriteFolder(string fullPath, StringBuilder sb, int depth, string displayName)
    {
        string indent = new string(' ', depth * 2);
        sb.AppendLine($"{indent}{displayName}/");

        // List files (skip .meta files, they're Unity's internal tracking)
        var files = Directory.GetFiles(fullPath)
            .Where(f => !f.EndsWith(".meta"))
            .OrderBy(f => f)
            .ToList();

        foreach (string file in files)
        {
            string fileName = Path.GetFileName(file);
            sb.AppendLine($"{indent}  - {fileName}");
        }

        // Recurse into subfolders
        var subfolders = Directory.GetDirectories(fullPath)
            .OrderBy(d => d)
            .ToList();

        foreach (string subfolder in subfolders)
        {
            string folderName = Path.GetFileName(subfolder);
            WriteFolder(subfolder, sb, depth + 1, folderName);
        }
    }
}