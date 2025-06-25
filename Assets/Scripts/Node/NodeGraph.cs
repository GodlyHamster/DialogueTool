using UnityEngine;
using System.Collections.Generic;

public class NodeGraph : MonoBehaviour
{
    public static NodeGraph Instance { get; private set; }

    [SerializeField]
    private GameObject nodePrefab;

    public List<NodeUIInteraction> nodes { get; private set; } = new List<NodeUIInteraction>();

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnNode()
    {
        GameObject nodeObj = Instantiate(nodePrefab, transform);
        NodeUIInteraction nodeUI = nodeObj.GetComponent<NodeUIInteraction>();

        Vector3 camPos = Camera.main.transform.position;
        nodeObj.transform.position = new Vector3(camPos.x, camPos.y, 0);

        nodes.Add(nodeUI);
    }

    public void RemoveNode(NodeUIInteraction node)
    {
        int nodeIndex = nodes.IndexOf(node);
        Destroy(nodes[nodeIndex].gameObject);
        nodes.RemoveAt(nodeIndex);
    }

    public void ClearGraph()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            Destroy(nodes[i].gameObject);
        }
        nodes.Clear();
    }

    public void LoadNodes(NodeData[] nodeArray)
    {
        ClearGraph();
        //spawn in nodes and set their data
        foreach (NodeData node in nodeArray)
        {
            GameObject newNodeObj = Instantiate(nodePrefab, transform);
            NodeUIInteraction nodeUI = newNodeObj.GetComponent<NodeUIInteraction>();

            nodeUI.LoadFromData(node);

            nodes.Add(nodeUI);
        }
        //connect all the nodes
        foreach (NodeUIInteraction node in nodes)
        {
            node.ApplyOptionConnections();
        }
    }

    public NodeUIInteraction GetNodeFromID(string id)
    {
        foreach (NodeUIInteraction node in nodes)
        {
            if (node.nodeData.NodeID == id)
            {
                return node;
            }
        }
        return null;
    }
}
