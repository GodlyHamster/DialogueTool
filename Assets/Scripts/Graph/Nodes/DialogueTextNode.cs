using Assets.Scripts.Graph;
using UnityEngine;

public class DialogueTextNode : DialogueBaseNode
{
    [field: SerializeField]
    private string text;
    private NodeInput inputNode;
    private NodeOutput outputNode;

    public override void Setup()
    {
        NodeType = NodeTypes.TextNode;
    }
}
