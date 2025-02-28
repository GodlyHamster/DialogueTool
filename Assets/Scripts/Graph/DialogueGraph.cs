using UnityEngine;
using System.Collections.Generic;

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
        Debug.Log($"Created new start node");
        GameObject node = Instantiate(StartNodePrefab, transform);
        InstantiateNode(new DialogueStartNode(), node);
    }

    public void CreateTextNode()
    {
        Debug.Log($"Created new text node");
        GameObject node = Instantiate(TextNodePrefab, transform);
        InstantiateNode(new DialogueTextNode(), node);
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
