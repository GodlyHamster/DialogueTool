using UnityEngine;

public class DialogueTextNodeData : DialogueBaseNodeData
{
    [SerializeField]
    private string text;
    [SerializeField]
    private NodeConnection inputNode;
    [SerializeField]
    private NodeConnection outputNode;

    public void SetText(string text)
    {
        this.text = text;
    }

    public string GetText()
    {
        return text;
    }

    public override object GetExportData()
    {
        return text;
    }
}
