using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

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
        foreach (NodeData node in nodeArray)
        {
            GameObject newNodeObj = Instantiate(nodePrefab, transform);
            NodeUIInteraction nodeUI = newNodeObj.GetComponent<NodeUIInteraction>();

            nodeUI.LoadFromData(node);

            nodes.Add(nodeUI);
        }
    }
}
