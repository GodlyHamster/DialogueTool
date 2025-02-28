using Assets.Scripts.Graph;

public class DialogueTextNode : DialogueBaseNode
{
    private NodeInput inputNode;
    private NodeOutput outputNode;

    public override void Setup()
    {
        NodeType = NodeTypes.TextNode;
    }
}
