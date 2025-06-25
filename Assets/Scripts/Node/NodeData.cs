using System;
using UnityEngine;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Assets.Scripts.Graph;

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
public class DialogueOption
{
    public string optionText;
    public string outputNodeID;

    private ConnectionUI connector;

    private TMP_InputField.OnChangeEvent onChangeEvent;

    public DialogueOption(ConnectionUI connector)
    {
        this.connector = connector;
        this.connector.OnConnectionUpdated += NodeConnectionUpdated;
    }

    private void NodeConnectionUpdated(string outputNodeID)
    {
        this.outputNodeID = outputNodeID;
    }

    ~DialogueOption()
    {
        onChangeEvent.RemoveAllListeners();
        this.connector.OnConnectionUpdated -= NodeConnectionUpdated;
    }

    public void AddInputListenEvent(TMP_InputField.OnChangeEvent changeEvent)
    {
        onChangeEvent = changeEvent;
        onChangeEvent.AddListener(UpdateText);
    }

    public void UpdateText(string newText)
    {
        optionText = newText;
    }
}