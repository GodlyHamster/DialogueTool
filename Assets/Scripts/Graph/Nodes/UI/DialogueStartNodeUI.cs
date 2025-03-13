using UnityEngine;

public class DialogueStartNodeUI : DialogueBaseNodeUI
{
    public override void Setup()
    {
        base.Setup();
        nodeData.NodeType = NodeTypes.StartNode;
        if (nodeData != null) return;
        nodeData = new DialogueStartNodeData();
    }
}
