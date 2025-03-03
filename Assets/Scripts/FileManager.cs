using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

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
        Debug.Log(savedata.testNodes);
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

    [Serializable]
    public class SaveData
    {
        [SerializeReference]
        public List<DialogueBaseNode> testNodes;

        public SaveData(List<DialogueBaseNode> nodes)
        {
            this.testNodes = nodes;
        }
    }
}
