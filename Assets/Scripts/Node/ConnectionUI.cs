using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConnectionUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private LineRenderer lineRenderer;
    public RectTransform thisRect { get; private set; }

    private NodeUIInteraction nodeParent;
    private ConnectionUIInput currentConnection = null;

    private bool isDragging = false;

    /// <summary>
    /// Invokes event with connectionID as parameter
    /// </summary>
    public event Action<string> OnConnectionUpdated;

    private void Awake()
    {
        nodeParent = GetComponentInParent<NodeUIInteraction>();
        thisRect = GetComponent<RectTransform>();
    }

    private void Start()
    {
        nodeParent.OnPositionUpdated += NodePosUpdated;
    }

    private void Update()
    {
        lineRenderer.SetPosition(0, thisRect.position);
        if (currentConnection == null || isDragging) return;
        lineRenderer.SetPosition(1, currentConnection.thisRect.position);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, thisRect.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 endPos = Camera.main.ScreenToWorldPoint(eventData.position);
        lineRenderer.SetPosition(1, endPos);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        foreach (var obj in eventData.hovered)
        {
            if (obj.TryGetComponent<ConnectionUIInput>(out ConnectionUIInput connection))
            {
                currentConnection = connection;
                currentConnection.OnPositionUpdated += ConnectionPosUpdated;
                lineRenderer.SetPosition(1, currentConnection.thisRect.position);
                string connectionID = currentConnection.nodeParent.nodeData.NodeID;
                OnConnectionUpdated?.Invoke(connectionID);
                return;
            }
        }
        currentConnection.OnPositionUpdated -= ConnectionPosUpdated;
        OnConnectionUpdated?.Invoke("");
        lineRenderer.enabled = false;
    }

    public void SetConnection(string nodeID)
    {
        NodeUIInteraction connectedNode = NodeGraph.Instance.GetNodeFromID(nodeID);
        if (connectedNode == null) return;
        currentConnection = connectedNode.gameObject.GetComponentInChildren<ConnectionUIInput>();
        currentConnection.OnPositionUpdated += ConnectionPosUpdated;
        lineRenderer.enabled = true;
    }

    private void NodePosUpdated(Vector2 pos)
    {
        lineRenderer.SetPosition(0, thisRect.position);
    }

    private void ConnectionPosUpdated(Vector2 position)
    {
        lineRenderer.SetPosition(1, position);
    }
}
