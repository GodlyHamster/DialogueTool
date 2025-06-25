using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Graph
{
    public class DraggableWindow : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerClickHandler, IBeginDragHandler
    {
        [SerializeField]
        private RectTransform window;
        [SerializeField]
        private Canvas graphCanvas;

        private RectTransform thisRect;
        private Vector3 mouseWorld;

        private NodeUIInteraction node;

        public event Action StartDrag;
        public event Action EndDrag;

        private void Awake()
        {
            graphCanvas = GetComponentInParent<Canvas>();
            thisRect = GetComponent<RectTransform>();
            node = GetComponentInParent<NodeUIInteraction>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            StartDrag?.Invoke();
        }

        public void OnDrag(PointerEventData eventData)
        {
            mouseWorld = Camera.main.ScreenToWorldPoint(eventData.position);
            window.anchoredPosition = mouseWorld;
            node.SetPosition(mouseWorld);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            node.SetPosition(window.anchoredPosition);
            EndDrag?.Invoke();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                NodeUIInteraction thisNode = gameObject.GetComponentInParent<NodeUIInteraction>();
                NodeGraph.Instance.RemoveNode(thisNode);
            }
        }
    }
}
