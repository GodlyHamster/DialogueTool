using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;

public class FileManager : MonoBehaviour
{
    [SerializeField]
    private DialogueGraph dialogueGraph;

    [ContextMenu("Load File")]
    public void LoadFile()
    {
        string datapath = EditorUtility.OpenFilePanel("Open Dialogue File", Application.dataPath, "json");

        if (string.IsNullOrEmpty(datapath)) return;

        StreamReader reader = new StreamReader(datapath);
        string jsonString = reader.ReadToEnd();

        SaveData savedata = JsonUtility.FromJson<SaveData>(jsonString);
        dialogueGraph.LoadGraphFromArray(savedata.nodes.ToArray());
    }

    [ContextMenu("Save File")]
    public void SaveFile()
    {
        string datapath = EditorUtility.SaveFilePanel("Save Dialogue File", Application.dataPath, "DefaultDialogue", "json");

        if (string.IsNullOrEmpty(datapath)) return;

        SaveData file = new SaveData(dialogueGraph.Nodes);
        string jsonString = JsonUtility.ToJson(file, true);

        StreamWriter sw = new StreamWriter(datapath);
        sw.Write(jsonString);
        sw.Flush();
        sw.Close();
    }

    [ContextMenu("Export File")]
    public void ExportFile()
    {
        string datapath = EditorUtility.SaveFilePanel("Export Dialogue File", Application.dataPath, "ExportedDialogue", "json");

        if (string.IsNullOrEmpty(datapath)) return;

        StringBuilder jsonString = new StringBuilder();
        foreach (var node in dialogueGraph.Nodes)
        {
            jsonString.AppendLine(JsonUtility.ToJson(node.nodeData.GetExportData(), true));
        }

        StreamWriter sw = new StreamWriter(datapath);
        sw.Write(jsonString);
        sw.Flush();
        sw.Close();
    }

    [Serializable]
    public class SaveData
    {
        [SerializeReference]
        public List<DialogueBaseNodeData> nodes;

        public SaveData(List<DialogueBaseNodeUI> nodes)
        {
            this.nodes = new List<DialogueBaseNodeData>();
            foreach (var node in nodes)
            {
                this.nodes.Add(node.nodeData);
            }
        }
    }
}
