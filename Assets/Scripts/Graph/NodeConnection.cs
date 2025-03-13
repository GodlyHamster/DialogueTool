using System;
using UnityEngine;

[Serializable]
public class NodeConnection
{
    public string nodeGuid;
    public NodeConnection connectedOutput;
    public NodeConnectionType connectionType;

    public NodeConnection()
    {
    }
}

public enum NodeConnectionType
{
    OUTPUT,
    INPUT,
}
