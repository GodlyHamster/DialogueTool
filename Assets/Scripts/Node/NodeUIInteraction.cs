using System.Collections.Generic;
using UnityEngine;

public class NodeUIInteraction : MonoBehaviour
{
    [SerializeField]
    private GameObject optionPrefab;
    [SerializeField]
    private Transform optionContainer;

    private Stack<GameObject> options = new Stack<GameObject>();

    public NodeData nodeData { get; private set; }

    private void Awake()
    {
        nodeData = new NodeData();
    }

    public void AddOption()
    {
        options.Push(Instantiate(optionPrefab, optionContainer));
    }
    public void RemoveOption()
    {
        if (options.Count == 0) return;
        Destroy(options.Peek());
        options.Pop();
    }
}
