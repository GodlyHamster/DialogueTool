using System;
using System.Text;
using UnityEngine;

public class DialogueTextNodeData : DialogueBaseNodeData, IOutputConnection
{
    [SerializeField]
    private string text;
    [SerializeField]
    private NodeConnection inputNode;
    [SerializeField]
    private NodeConnection outputNode;

    public NodeConnection nodeOutput { 
        get { return outputNode; } 
        set { outputNode = value; }
    }

    public void SetText(string text)
    {
        this.text = text;
    }

    public string GetText()
    {
        return text;
    }

    public override string GetExportData()
    {
        StringBuilder jsonString = new StringBuilder();

        jsonString.Append(JsonUtility.ToJson(new TextNodeDataWrapper(NodeID, text, outputNode?.nodeGuid), true));

        return jsonString.ToString();
    }
}

[Serializable]
public class TextNodeDataWrapper
{
    public string nodeID;
    public string text;
    public string outputNode;

    public TextNodeDataWrapper(string nodeID, string text, string outputNode)
    {
        this.nodeID = nodeID;
        this.text = text;
        this.outputNode = outputNode;
    }
}
