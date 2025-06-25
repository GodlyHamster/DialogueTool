using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NodeUIInteraction : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField dialogueInput;
    [SerializeField]
    private GameObject optionPrefab;
    [SerializeField]
    private Transform optionContainer;

    private RectTransform thisRect;
    private Stack<GameObject> options = new Stack<GameObject>();

    public NodeData nodeData { get; private set; }

    private void OnEnable()
    {
        dialogueInput.onValueChanged.AddListener((string param) => { nodeData.DialogueText = param; });
    }

    private void OnDisable()
    {
        dialogueInput.onValueChanged.RemoveAllListeners();
    }

    private void Awake()
    {
        nodeData = new NodeData();
        thisRect = GetComponent<RectTransform>();
    }

    public void LoadFromData(NodeData data)
    {
        nodeData = new NodeData(data);
        thisRect.transform.position = nodeData.Position;
        dialogueInput.text = nodeData.DialogueText;
        foreach (DialogueOption option in nodeData.DialogueOptions)
        {
            AddOptionFromData(option);
        }
    }

    public void AddOption()
    {
        options.Push(Instantiate(optionPrefab, optionContainer));
        nodeData.DialogueOptions.Add(new DialogueOption());
    }
    private void AddOptionFromData(DialogueOption optionData)
    {
        options.Push(Instantiate(optionPrefab, optionContainer));
        //enter text
    }
    public void RemoveOption()
    {
        if (options.Count == 0) return;
        Destroy(options.Peek());
        options.Pop();
        nodeData.DialogueOptions.RemoveAt(nodeData.DialogueOptions.Count - 1);
    }
}
