using System;
using UnityEngine;

[Serializable]
public class NodeConnection
{
    public NodeConnection connectedOutput;
    public NodeConnectionType connectionType;
}

public enum NodeConnectionType
{
    OUTPUT,
    INPUT,
}
