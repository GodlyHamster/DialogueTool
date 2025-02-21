using Assets.Scripts.Graph;
using UnityEngine;

public class DialogueTextNode : DialogueBaseNode
{
    public override Vector2 Size { get; set; } = new Vector2(3, 2);

    private NodeInput inputNode;
    private NodeOutput outputNode;
}
