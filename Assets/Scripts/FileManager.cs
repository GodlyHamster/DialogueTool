using System.IO;
using UnityEditor;
using UnityEngine;

public class FileManager : MonoBehaviour
{
    [SerializeField]
    private DialogueGraph dialogueGraph;

    [ContextMenu("Load File")]
    public void LoadFile()
    {
        string datapath = EditorUtility.OpenFilePanel("Open Dialogue File", Application.dataPath, "");
        Debug.Log(datapath);
    }

    [ContextMenu("Save File")]
    public void SaveFile()
    {
        string datapath = EditorUtility.SaveFilePanel("Save or Export Dialogue File", Application.dataPath, "DefaultDialogue", "json");

        if (string.IsNullOrEmpty(datapath)) return;

        StreamWriter streamWriter = new StreamWriter(datapath, false);
        streamWriter.Write("kaas");
        streamWriter.Flush();
        streamWriter.Close();
    }
}
