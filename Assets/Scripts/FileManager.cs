using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using SFB;

public class FileManager : MonoBehaviour
{
    [SerializeField]
    private DialogueGraph dialogueGraph;

    ExtensionFilter[] extensions = new[] {
        new ExtensionFilter("Dialogue Export File", "json")
    };

    [ContextMenu("Load File")]
    public void LoadFile()
    {
        string datapath = StandaloneFileBrowser.OpenFilePanel("Load Dialogue File", Application.dataPath, extensions, false)[0];

        StreamReader reader = new StreamReader(datapath);
        string jsonString = reader.ReadToEnd();

        SaveData savedata = JsonUtility.FromJson<SaveData>(jsonString);
        dialogueGraph.LoadGraphFromArray(savedata.nodes.ToArray());
    }

    [ContextMenu("Save File")]
    public void SaveFile()
    {
        string datapath = StandaloneFileBrowser.SaveFilePanel("Save Dialogue File", Application.dataPath, "DefaultDialogue", extensions);

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
        string datapath = StandaloneFileBrowser.SaveFilePanel("Export Dialogue File", Application.dataPath, "ExportedDialogue", extensions);

        if (string.IsNullOrEmpty(datapath)) return;

        StringBuilder jsonString = new StringBuilder();
        foreach (var node in dialogueGraph.Nodes)
        {
            jsonString.AppendLine(node.nodeData.GetExportData());
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
