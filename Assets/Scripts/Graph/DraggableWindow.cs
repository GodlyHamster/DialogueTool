using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Graph
{
    public class DraggableWindow : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        private DialogueBaseNodeUI parent;
        [SerializeField]
        private RectTransform window;
        [SerializeField]
        private Canvas graphCanvas;


        private void OnEnable()
        {
            graphCanvas = GetComponentInParent<Canvas>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            window.anchoredPosition += eventData.delta / graphCanvas.scaleFactor;
            parent.isBeingDragged = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            parent.isBeingDragged = false;
            parent.SetPosition(window.anchoredPosition);
        }
    }
}
