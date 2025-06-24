using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InterfaceButton : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{
    private bool isOpened;

    public UnityEvent OnClick = new UnityEvent();
    public UnityEvent OnClose = new UnityEvent();

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isOpened)
        {
            isOpened = false;
            OnClose.Invoke();
        }
        else
        {
            isOpened = true;
            OnClick.Invoke();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOpened = false;
        OnClose.Invoke();
    }
}
