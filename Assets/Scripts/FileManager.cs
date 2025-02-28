using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class FileManager : MonoBehaviour
{
    [SerializeField]
    private DialogueGraph dialogueGraph;

    [ContextMenu("Load File")]
    public void LoadFile()
    {
        string datapath = EditorUtility.OpenFilePanel("Open Dialogue File", Application.dataPath, "json");
        Debug.Log(datapath);
    }

    [ContextMenu("Save File")]
    public void SaveFile()
    {
        string datapath = EditorUtility.SaveFilePanel("Save or Export Dialogue File", Application.dataPath, "DefaultDialogue", "json");

        if (string.IsNullOrEmpty(datapath)) return;

        StringBuilder sb = new StringBuilder();

        foreach (var item in dialogueGraph.Nodes)
        {
            string jsonNode = JsonUtility.ToJson(item, true);
            sb.AppendLine(jsonNode);
        }

        StreamWriter streamWriter = new StreamWriter(datapath, false);
        streamWriter.Write(sb);
        streamWriter.Flush();
        streamWriter.Close();
    }
}
