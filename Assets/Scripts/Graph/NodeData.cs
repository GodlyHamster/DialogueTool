using System;
using UnityEngine;

[Serializable]
public abstract class NodeData
{
    public NodeTypes NodeType;
    public Vector2 NodePosition;

    public abstract void Setup();
}
