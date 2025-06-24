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

        public event Action StartDrag;
        public event Action EndDrag;

        private void Awake()
        {
            graphCanvas = GetComponentInParent<Canvas>();
            thisRect = GetComponent<RectTransform>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            StartDrag?.Invoke();
        }

        public void OnDrag(PointerEventData eventData)
        {
            mouseWorld = eventData.pointerCurrentRaycast.worldPosition;
            window.anchoredPosition = mouseWorld;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
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
