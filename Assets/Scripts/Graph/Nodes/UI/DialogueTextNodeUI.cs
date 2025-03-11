using TMPro;
using UnityEngine;

public class DialogueTextNodeUI : DialogueBaseNodeUI
{
    public override void Setup()
    {
        Debug.Log("setup a text node");
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
        Debug.Log("loaded a text node");
        gameObject.GetComponentInChildren<TMP_InputField>().text = (nodeData as DialogueTextNodeData).GetText();
    }
}
