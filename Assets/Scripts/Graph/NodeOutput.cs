using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Graph
{
    //TODO: make code more DRY, nodeInput has lot of same code
    public class NodeOutput : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public DialogueNode parent;
        public NodeInput connectedInput;
        public LineRenderer lineRenderer;

        [SerializeField]
        private RectTransform rectTransform;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (connectedInput != null)
            {
                connectedInput.connectedOutput = null;
                connectedInput = null;
            }
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.green;
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
                if (obj.TryGetComponent<NodeInput>(out NodeInput input))
                {
                    connectedInput = input;
                    connectedInput.connectedOutput = this;
                    return;
                }
            }
            lineRenderer.enabled = false;
        }

        private void Start()
        {
            if (parent == null)
            {
                parent = GetComponentInParent<DialogueNode>();
            }
        }

        private void Update()
        {
            if (!parent.isBeingDragged) return;

            lineRenderer.SetPosition(0, (Vector2)Camera.main.ScreenToWorldPoint(rectTransform.position));
        }
    }
}
