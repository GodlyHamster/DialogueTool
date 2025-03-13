using TMPro;
using UnityEngine;

public class DialogueTextNodeUI : DialogueBaseNodeUI
{
    public override void Setup()
    {
        base.Setup();
        gameObject.GetComponentInChildren<TMP_InputField>().onValueChanged.AddListener(OnTextChanged);

        nodeData.NodeType = NodeTypes.TextNode;
        if (nodeData != null) return;
        nodeData = new DialogueTextNodeData();
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
