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

    public NodeConnection(string nodeGuid, NodeConnection connection, NodeConnectionType type)
    {
        this.nodeGuid = nodeGuid;
        this.connectedOutput = connection;
        this.connectionType = type;
    }
}

public enum NodeConnectionType
{
    OUTPUT,
    INPUT,
}
