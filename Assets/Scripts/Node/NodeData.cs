using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class NodeData
{
    [SerializeField] private string nodeID;
    [SerializeField] private Vector2 position;
    /// <summary>
    /// An array of the connected node's ids
    /// </summary>
    [SerializeField] private List<string> connectionIDs;

    public string NodeID { 
        get { return nodeID; }
    }
    public Vector2 Position { 
        get { return position; }
        set { position = value; }
    }
    public List<string> ConnectionIDs { 
        get { return connectionIDs; }
    }

    public NodeData()
    {
        nodeID = Guid.NewGuid().ToString();
        position = Vector2.zero;
        connectionIDs = new List<string>();
    }

    public NodeData(NodeData data)
    {
        this.nodeID = data.nodeID;
        this.position = data.position;
        this.connectionIDs = data.connectionIDs;
    }
}
