using System.Collections.Generic;
using System.Linq;
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
        DialogueOption option = new DialogueOption();
        option.AddInputListenEvent(options.Peek().GetComponentInChildren<TMP_InputField>().onValueChanged);
        nodeData.DialogueOptions.Add(option);
    }
    private void AddOptionFromData(DialogueOption optionData)
    {
        options.Push(Instantiate(optionPrefab, optionContainer));
        TMP_InputField inputField = options.Peek().GetComponentInChildren<TMP_InputField>();
        inputField.text = optionData.optionText;
        optionData.AddInputListenEvent(inputField.onValueChanged);
    }
    public void RemoveOption()
    {
        if (options.Count == 0) return;
        Destroy(options.Peek());
        options.Pop();
        nodeData.DialogueOptions.RemoveAt(nodeData.DialogueOptions.Count - 1);
    }
}
