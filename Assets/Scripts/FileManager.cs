using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using SFB;

public class FileManager : MonoBehaviour
{
    ExtensionFilter[] extensions = new[] {
        new ExtensionFilter("Dialogue Export File", "json")
    };

    public void LoadFile()
    {
        string datapath = StandaloneFileBrowser.OpenFilePanel("Load Dialogue File", Application.dataPath, extensions, false)[0];

        if (string.IsNullOrEmpty(datapath)) return;

        StreamReader reader = new StreamReader(datapath);
        string jsonString = reader.ReadToEnd();

        SaveData savedata = JsonUtility.FromJson<SaveData>(jsonString);
        NodeGraph.Instance.LoadNodes(savedata.nodes.ToArray());
    }

    public void SaveFile()
    {
        string datapath = StandaloneFileBrowser.SaveFilePanel("Save Dialogue File", Application.dataPath, "DefaultDialogue", extensions);

        if (string.IsNullOrEmpty(datapath)) return;

        SaveData saveData = new SaveData(NodeGraph.Instance.nodes);
        string jsonString = JsonUtility.ToJson(saveData, true);

        StreamWriter sw = new StreamWriter(datapath);
        sw.Write(jsonString);
        sw.Flush();
        sw.Close();
    }

    public void ExportFile()
    {
        string datapath = StandaloneFileBrowser.SaveFilePanel("Export Dialogue File", Application.dataPath, "ExportedDialogue", extensions);

        if (string.IsNullOrEmpty(datapath)) return;

        StringBuilder jsonString = new StringBuilder();
        foreach (var node in NodeGraph.Instance.nodes)
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
        public List<NodeData> nodes;

        public SaveData(List<NodeUIInteraction> nodes)
        {
            this.nodes = new List<NodeData>();
            foreach (var node in nodes)
            {
                this.nodes.Add(node.nodeData);
            }
        }
    }
}
