using System;
using UnityEditor;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Graph
{
    public class NodeConnectionUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public DialogueBaseNodeUI parent;
        public NodeConnection nodeConnection = new NodeConnection();
        public LineRenderer lineRenderer;

        [SerializeField]
        private RectTransform rectTransform;
        public RectTransform GetRect {  get { return rectTransform; } }

        [HideInInspector]
        public UnityEvent<Vector2> OnPositionUpdated = new UnityEvent<Vector2>();

        private void OnEnable()
        {
            if (parent == null)
            {
                parent = GetComponentInParent<DialogueBaseNodeUI>();
            }
            DialogueGraph.Instance.OnNodesLoaded += NodeLoaded;
        }

        private void OnDisable()
        {
            DialogueGraph.Instance.OnNodesLoaded -= NodeLoaded;
        }

        private void NodeLoaded()
        {
            if (parent.nodeData is IOutputConnection nodeOutput)
            {
                nodeConnection = nodeOutput.nodeOutput;

                //find connected node and set the line renderers positions correctly
                DialogueBaseNodeUI connectedUI = DialogueGraph.Instance.GetNodeFromGuid(nodeConnection.nodeGuid);
                if (connectedUI == null) return;
                NodeConnectionUI nodeConnectionUI = null;
                foreach(var item in connectedUI.GetComponentsInChildren<NodeConnectionUI>())
                {
                    nodeConnectionUI = item.nodeConnection.connectionType == NodeConnectionType.INPUT ? item : null;
                    if (nodeConnectionUI != null) break;
                }
                if (nodeConnectionUI == null) return;

                nodeConnectionUI.OnPositionUpdated.AddListener(PositionUpdated);
                lineRenderer.SetPosition(0, (Vector2)Camera.main.ScreenToWorldPoint(rectTransform.position));
                lineRenderer.SetPosition(1, (Vector2)Camera.main.ScreenToWorldPoint(nodeConnectionUI.GetRect.position));
                lineRenderer.enabled = true;
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (nodeConnection.connectedOutput != null)
            {
                nodeConnection.connectedOutput = null;
            }
            lineRenderer.SetPosition(0, (Vector2)Camera.main.ScreenToWorldPoint(rectTransform.position));
            lineRenderer.enabled = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            lineRenderer.SetPosition(1, (Vector2)Camera.main.ScreenToWorldPoint(eventData.position));
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            foreach (var obj in eventData.hovered)
            {
                if (obj.TryGetComponent<NodeConnectionUI>(out NodeConnectionUI connection))
                {
                    if (connection == this) continue;
                    if (nodeConnection.connectionType == connection.nodeConnection.connectionType) break;
                    nodeConnection.connectedOutput = connection.nodeConnection;
                    connection.nodeConnection.connectedOutput = nodeConnection;
                    if (parent.nodeData is IOutputConnection nodeOutput)
                    {
                        nodeOutput.nodeOutput = nodeConnection.connectedOutput;
                    }
                    connection.OnPositionUpdated.AddListener(PositionUpdated);
                    return;
                }
            }
            lineRenderer.enabled = false;
        }

        public void PositionUpdated(Vector2 position)
        {
            lineRenderer.SetPosition(1, position);
        }

        private void Start()
        {
            nodeConnection.nodeGuid = parent.nodeData.NodeID;
            switch (nodeConnection.connectionType)
            {
                case NodeConnectionType.INPUT:
                    lineRenderer.startColor = Color.green;
                    lineRenderer.endColor = Color.red;
                    break;
                case NodeConnectionType.OUTPUT:
                    lineRenderer.startColor = Color.red;
                    lineRenderer.endColor = Color.green;
                    break;
                default:
                    lineRenderer.startColor = Color.white;
                    lineRenderer.endColor = Color.white;
                    break;
            }
        }

        private void Update()
        {
            if (!parent.isBeingDragged) return;

            Vector2 newPosition = (Vector2)Camera.main.ScreenToWorldPoint(rectTransform.position);
            lineRenderer.SetPosition(0, newPosition);
            OnPositionUpdated.Invoke(newPosition);
        }
    }
}
