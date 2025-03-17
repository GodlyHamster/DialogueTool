using System;
using UnityEditor;
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

        [HideInInspector]
        public UnityEvent<Vector2> OnPositionUpdated = new UnityEvent<Vector2>();

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
                    if (connection.nodeConnection.connectionType == nodeConnection.connectionType) break;
                    nodeConnection.connectedOutput = connection.nodeConnection;
                    connection.nodeConnection.connectedOutput = this.nodeConnection;
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

            if (parent == null)
            {
                parent = GetComponentInParent<DialogueBaseNodeUI>();
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
