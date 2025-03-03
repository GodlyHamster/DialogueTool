using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;

public class FileManager : MonoBehaviour
{
    [SerializeField]
    private DialogueGraph dialogueGraph;

    [ContextMenu("Load File")]
    public void LoadFile()
    {
        string datapath = EditorUtility.OpenFilePanel("Open Dialogue File", Application.dataPath, "json");

        if (string.IsNullOrEmpty(datapath)) return;
    }

    [ContextMenu("Save File")]
    public void SaveFile()
    {
        string datapath = EditorUtility.SaveFilePanel("Save or Export Dialogue File", Application.dataPath, "DefaultDialogue", "json");

        if (string.IsNullOrEmpty(datapath)) return;

        Savefile file = new Savefile(dialogueGraph.Nodes);
        Debug.Log(JsonUtility.ToJson(file, true));
    }

    [Serializable]
    public class Savefile
    {
        [SerializeReference]
        public List<DialogueBaseNode> testNodes;

        public Savefile(List<DialogueBaseNode> nodes)
        {
            this.testNodes = nodes;
        }
    }
}
