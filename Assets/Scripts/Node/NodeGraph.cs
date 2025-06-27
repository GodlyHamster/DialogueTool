using UnityEngine;
using System.Collections.Generic;
using Unity.Mathematics;
using System.Threading.Tasks;

public class NodeGraph : MonoBehaviour
{
    public static NodeGraph Instance { get; private set; }

    [SerializeField]
    private GameObject nodePrefab;
    [SerializeField]
    private ConfirmationPopup startingNodePopup;

    public List<NodeUIInteraction> nodes { get; private set; } = new List<NodeUIInteraction>();
    public NodeUIInteraction startingNode { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnNode()
    {
        GameObject nodeObj = Instantiate(nodePrefab, transform);
        NodeUIInteraction newNode = nodeObj.GetComponent<NodeUIInteraction>();

        Vector3 camPos = Camera.main.transform.position;
        nodeObj.transform.position = new Vector3(camPos.x, camPos.y, 0);

        if (startingNode != null)
        {
            startingNode = newNode;
        }
        nodes.Add(newNode);
    }

    public void RemoveNode(NodeUIInteraction node)
    {
        if (startingNode == node) startingNode = null;
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

    public async Task<bool> SetStartingNode(NodeUIInteraction node, bool setTrue)
    {
        if (startingNode != node && setTrue == false)
        {
            Debug.Log("action1");
            return false;
        }
        if (startingNode == null && setTrue)
        {
            Debug.Log("action2");
            startingNode = node;
            return true;
        }
        if (startingNode == node && setTrue == false)
        {
            Debug.Log("action3");
            startingNode = null;
            return false;
        }
        if (startingNode != node && setTrue)
        {
            Debug.Log("action4");
            bool result = await startingNodePopup.GetPopupResult();
            if (result)
            {
                startingNode.SetAsStartingNode(false);
                startingNode = node;
                return true;
            }
        }

        Debug.Log("action5");
        return false;
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
