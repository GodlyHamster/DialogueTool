using System;
using UnityEngine;

public class ConnectionUIInput : MonoBehaviour
{
    public RectTransform thisRect { get; private set; }
    public NodeUIInteraction nodeParent {  get; private set; }

    public event Action<Vector2> OnPositionUpdated;

    private void Awake()
    {
        thisRect = GetComponent<RectTransform>();
        nodeParent = GetComponentInParent<NodeUIInteraction>();
    }

    private void Start()
    {
        nodeParent.OnPositionUpdated += PositionUpdated;
    }

    private void PositionUpdated(Vector2 obj)
    {
        OnPositionUpdated?.Invoke(thisRect.position);
    }
}
