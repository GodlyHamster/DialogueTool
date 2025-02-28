using Assets.Scripts.Graph;

public class DialogueStartNode : DialogueBaseNode
{
    private NodeOutput outputNode;

    public override void Setup()
    {
        NodeType = NodeTypes.StartNode;
    }
}
