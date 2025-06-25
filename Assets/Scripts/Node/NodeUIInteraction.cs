using System.Collections.Generic;
using UnityEngine;

public class NodeUIInteraction : MonoBehaviour
{
    [SerializeField]
    private GameObject optionPrefab;
    [SerializeField]
    private Transform optionContainer;

    private RectTransform thisRect;
    private Stack<GameObject> options = new Stack<GameObject>();

    public NodeData nodeData { get; private set; }

    private void Awake()
    {
        nodeData = new NodeData();
        thisRect = GetComponent<RectTransform>();
    }

    public void SetPositionData(Vector2 position)
    {
        nodeData.Position = position;
    }

    public void LoadFromData(NodeData data)
    {
        nodeData = new NodeData(data);
        thisRect.transform.position = nodeData.Position;
        //re-add all options correctly
    }

    public void AddOption()
    {
        options.Push(Instantiate(optionPrefab, optionContainer));
    }
    public void AddOption(string text)
    {
        options.Push(Instantiate(optionPrefab, optionContainer));
        //enter text
    }
    public void RemoveOption()
    {
        if (options.Count == 0) return;
        Destroy(options.Peek());
        options.Pop();
    }
}
