using System;
using UnityEngine;

[Serializable]
public abstract class DialogueBaseNodeData
{
    public NodeTypes NodeType = NodeTypes.None;
    public Vector2 NodePosition;
}
