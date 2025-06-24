using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InterfaceButton : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{
    public bool toggleClick = false;
    private bool isOpened = false;

    public UnityEvent OnClick = new UnityEvent();
    public UnityEvent OnClose = new UnityEvent();

    public void OnPointerClick(PointerEventData eventData)
    {
        if (toggleClick)
        {
            isOpened = !isOpened;
            if (isOpened) OnClick.Invoke();
            if (!isOpened) OnClose.Invoke();
            return;
        }
        OnClick.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOpened = false;
        OnClose.Invoke();
    }
}
