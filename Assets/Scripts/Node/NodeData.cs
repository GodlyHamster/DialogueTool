using System;
using UnityEngine;
using System.Collections.Generic;
using System.Text;

[Serializable]
public class NodeData
{
    [SerializeField] private string nodeID;
    [SerializeField] private Vector2 position;
    [SerializeField] private string dialogueText;
    [SerializeField] private List<DialogueOption> dialogueOptions;

    public string NodeID { 
        get { return nodeID; }
    }
    public Vector2 Position { 
        get { return position; }
        set { position = value; }
    }
    public string DialogueText
    {
        get { return dialogueText; }
        set {  dialogueText = value; }
    }
    public List<DialogueOption> DialogueOptions { 
        get { return dialogueOptions; }
    }

    public NodeData()
    {
        nodeID = Guid.NewGuid().ToString();
        position = Vector2.zero;
        dialogueOptions = new List<DialogueOption>();
    }

    public NodeData(NodeData data)
    {
        this.nodeID = data.nodeID;
        this.position = data.position;
        this.dialogueText = data.dialogueText;
        this.dialogueOptions = data.dialogueOptions;
    }

    public string GetExportData()
    {
        StringBuilder jsonString = new StringBuilder();

        jsonString.Append(JsonUtility.ToJson(new NodeDataWrapper(this), true));

        return jsonString.ToString();
    }
}

[Serializable]
public class NodeDataWrapper
{
    public string nodeID;
    public string dialogueText;
    public List<DialogueOption> dialogueOptions;

    public NodeDataWrapper(NodeData nodeData)
    {
        nodeID = nodeData.NodeID;
        dialogueText = nodeData.DialogueText;
        dialogueOptions = nodeData.DialogueOptions;
    }
}

[Serializable]
public struct DialogueOption
{
    public string optionText;
    public string outputNodeID;
}