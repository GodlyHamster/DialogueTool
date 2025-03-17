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
    }

    /// <summary>
    /// Should return the data as a json string
    /// </summary>
    /// <returns></returns>
    public virtual string GetExportData() { return null; }
}
