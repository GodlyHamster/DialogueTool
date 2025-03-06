using UnityEngine;
using System.Collections.Generic;
using System;

public class DialogueGraph : MonoBehaviour
{
    [SerializeField]
    private GameObject StartNodePrefab;
    [SerializeField]
    private GameObject TextNodePrefab;

    private List<DialogueBaseNode> nodes = new List<DialogueBaseNode>();
    public List<DialogueBaseNode> Nodes { get { return nodes; } }

    public void CreateStartNode()
    {
        foreach (DialogueBaseNode item in nodes)
        {
            if (item.GetType() == typeof(DialogueStartNode))
            {
                Debug.LogWarning("A start node already exists!");
                return;
            }
        }
        GameObject node = Instantiate(StartNodePrefab, transform);
        InstantiateNode(new DialogueStartNode(), node);
    }

    public void CreateTextNode()
    {
        GameObject node = Instantiate(TextNodePrefab, transform);
        InstantiateNode(new DialogueTextNode(), node);
    }

    public void LoadGraphFromArray(DialogueBaseNode[] nodeList)
    {
        foreach (DialogueBaseNode node in nodeList)
        {
            GameObject nodePrefab = null;
            switch (node.NodeType)
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
            InstantiateNode(node, nodePrefab);
        }
    }

    public bool InstantiateNode(DialogueBaseNode node, GameObject linkedObject)
    {
        DialogueNode dialogueNode = linkedObject.GetComponent<DialogueNode>();
        dialogueNode.node = node;
        dialogueNode.node.Setup();

        nodes.Add(node);

        if (linkedObject.TryGetComponent<RectTransform>(out RectTransform rect))
        {
            rect.anchoredPosition = node.NodePosition;
        }
        else
        {
            return false;
        }
        return true;
    }
}
