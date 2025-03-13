using System;
using UnityEngine;

[Serializable]
public abstract class DialogueBaseNodeData
{
    public string NodeID;
    public NodeTypes NodeType = NodeTypes.None;
    public Vector2 NodePosition;

    public void Init()
    {
        if (NodeID != null) return;
        NodeID = Guid.NewGuid().ToString();
        Debug.Log($"Initialized new {this} with ID: {NodeID}");
    }

    public virtual object GetExportData() { return null; }
}
