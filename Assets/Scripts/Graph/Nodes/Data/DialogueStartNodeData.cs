using System;
using System.Text;
using UnityEngine;

public class DialogueStartNodeData : DialogueBaseNodeData, IOutputConnection
{
    [SerializeField]
    private NodeConnection outputNode;

    public NodeConnection nodeOutput
    {
        get { return outputNode; }
        set { outputNode = value; }
    }

    public override string GetExportData()
    {
        StringBuilder jsonString = new StringBuilder();

        jsonString.Append(JsonUtility.ToJson(new StartNodeDataWrapper(NodeID, outputNode?.nodeGuid), true));

        return jsonString.ToString();
    }
}

[Serializable]
public class StartNodeDataWrapper
{
    public string nodeID;
    public string outputNode;

    public StartNodeDataWrapper(string nodeID, string outputNode)
    {
        this.nodeID = nodeID;
        this.outputNode = outputNode;
    }
}
