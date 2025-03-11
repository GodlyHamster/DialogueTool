using Assets.Scripts.Graph;
using UnityEngine;

public class DialogueTextNodeData : DialogueBaseNodeData
{
    [field: SerializeField]
    private string text;
    private NodeConnectionUI inputNode;
    private NodeConnectionUI outputNode;

    public void SetText(string text)
    {
        this.text = text;
    }

    public string GetText()
    {
        return text;
    }
}
