using UnityEngine;
using System.Collections.Generic;
using System;

public class DialogueGraph : MonoBehaviour
{
    public static DialogueGraph Instance;

    public event Action OnNodesLoaded;

    [SerializeField]
    private GameObject StartNodePrefab;
    [SerializeField]
    private GameObject TextNodePrefab;

    private List<DialogueBaseNodeUI> nodes = new List<DialogueBaseNodeUI>();
    public List<DialogueBaseNodeUI> Nodes { get { return nodes; } }

    private void Awake()
    {
        Instance = this;
    }

    public void CreateStartNode()
    {
        foreach (DialogueBaseNodeUI item in nodes)
        {
            if (item.GetType() == typeof(DialogueStartNodeUI))
            {
                Debug.LogWarning("A start node already exists!");
                return;
            }
        }
        GameObject node = Instantiate(StartNodePrefab, transform);
        InstantiateNode(new DialogueStartNodeData(), node);
    }

    public void CreateTextNode()
    {
        GameObject node = Instantiate(TextNodePrefab, transform);
        InstantiateNode(new DialogueTextNodeData(), node);
    }

    public void LoadGraphFromArray(DialogueBaseNodeData[] nodeList)
    {
        ClearGraph();

        foreach (DialogueBaseNodeData nodeData in nodeList)
        {
            GameObject nodePrefab = null;
            switch (nodeData.NodeType)
            {
                case NodeTypes.StartNode:
                    nodePrefab = Instantiate(StartNodePrefab, transform);
                    break;
                case NodeTypes.TextNode:
                    nodePrefab = Instantiate(TextNodePrefab, transform);
                    break;
                default:
                    Debug.LogWarning("Node type does not exist");
                    break;
            }
            if (nodePrefab == null) continue;
            DialogueBaseNodeUI nodeUI = nodePrefab.GetComponent<DialogueBaseNodeUI>();
            nodeUI.nodeData = nodeData;
            nodeUI.Load();
            InstantiateNode(nodeData, nodePrefab);
        }
        OnNodesLoaded.Invoke();
    }

    public bool InstantiateNode(DialogueBaseNodeData nodeData, GameObject linkedObject)
    {
        DialogueBaseNodeUI dialogueNode = linkedObject.GetComponent<DialogueBaseNodeUI>();
        dialogueNode.nodeData = nodeData;
        dialogueNode.Setup();

        nodes.Add(dialogueNode);

        if (linkedObject.TryGetComponent<RectTransform>(out RectTransform rect))
        {
            rect.anchoredPosition = nodeData.NodePosition;
        }
        else
        {
            return false;
        }
        return true;
    }

    private void ClearGraph()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            Destroy(nodes[i].gameObject);
        }
        nodes.Clear();
    }

    public DialogueBaseNodeUI GetNodeFromGuid(string guid)
    {
        foreach (DialogueBaseNodeUI node in nodes)
        {
            if (node.nodeData.NodeID == guid)
            {
                return node;
            }
        }
        return null;
    }
}
