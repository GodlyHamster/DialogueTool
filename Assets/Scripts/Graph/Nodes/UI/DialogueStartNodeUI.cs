using UnityEngine;

public class DialogueStartNodeUI : DialogueBaseNodeUI
{
    public override void Setup()
    {
        nodeData = new DialogueStartNodeData();
        nodeData.NodeType = NodeTypes.StartNode;
    }
}
