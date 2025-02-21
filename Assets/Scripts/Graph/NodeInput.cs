using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Graph
{
    //TODO: make code more DRY, nodeOutput has lot of same code
    public class NodeInput : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public DialogueBaseNode parent;
        public NodeOutput connectedOutput;
        public LineRenderer lineRenderer;

        [SerializeField]
        private RectTransform rectTransform;


        public void OnBeginDrag(PointerEventData eventData)
        {
            if (connectedOutput != null)
            {
                connectedOutput.connectedInput = null;
                connectedOutput = null;
            }

            lineRenderer.startColor = Color.green;
            lineRenderer.endColor = Color.red;
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
                if (obj.TryGetComponent<NodeOutput>(out NodeOutput output))
                {
                    connectedOutput = output;
                    connectedOutput.connectedInput = this;
                    return;
                }
            }
            lineRenderer.enabled = false;
        }

        private void Start()
        {
            if (parent == null)
            {
                parent = GetComponentInParent<DialogueBaseNode>();
            }
        }

        private void Update()
        {
            if (!parent.isBeingDragged) return;

            lineRenderer.SetPosition(0, (Vector2)Camera.main.ScreenToWorldPoint(rectTransform.position));

            if (connectedOutput == null) return;

            connectedOutput.lineRenderer.SetPosition(1, (Vector2)Camera.main.ScreenToWorldPoint(rectTransform.position));
        }
    }
}
