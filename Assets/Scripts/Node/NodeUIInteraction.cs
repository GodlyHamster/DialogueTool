using System;
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

    public event Action<Vector2> OnPositionUpdated;

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

    public void SetPosition(Vector2 position)
    {
        nodeData.Position = position;
        OnPositionUpdated?.Invoke(position);
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
        GameObject optionObj = Instantiate(optionPrefab, optionContainer);
        options.Push(optionObj);

        ConnectionUI connector = optionObj.GetComponentInChildren<ConnectionUI>();
        DialogueOption option = new DialogueOption(connector);
        option.AddInputListenEvent(optionObj.GetComponentInChildren<TMP_InputField>().onValueChanged);
        nodeData.DialogueOptions.Add(option);
    }
    private void AddOptionFromData(DialogueOption optionData)
    {
        GameObject optionObj = Instantiate(optionPrefab, optionContainer);
        options.Push(optionObj);

        TMP_InputField inputField = optionObj.GetComponentInChildren<TMP_InputField>();
        optionData.SetConnector(optionObj.GetComponentInChildren<ConnectionUI>());
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

    public void ApplyOptionConnections()
    {
        foreach (DialogueOption option in nodeData.DialogueOptions)
        {
            option.connector.SetConnection(option.outputNodeID);
        }
    }
}
