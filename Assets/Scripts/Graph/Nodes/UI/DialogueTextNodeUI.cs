using TMPro;
using UnityEngine;

public class DialogueTextNodeUI : DialogueBaseNodeUI
{
    public override void Setup()
    {
        nodeData = new DialogueTextNodeData();
        nodeData.NodeType = NodeTypes.TextNode;
        gameObject.GetComponentInChildren<TMP_InputField>().onValueChanged.AddListener(OnTextChanged);
    }

    public void OnTextChanged(string text)
    {
        (nodeData as DialogueTextNodeData).SetText(text);
    }

    public override void OnLoad()
    {
        base.OnLoad();
        gameObject.GetComponentInChildren<TMP_InputField>().text = (nodeData as DialogueTextNodeData).GetText();
    }
}
