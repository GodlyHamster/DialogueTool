using Assets.Scripts;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationPopup : MonoBehaviour
{
    [SerializeField]
    private Button confirmButton;
    [SerializeField]
    private Button cancelButton;

    [SerializeField]
    private GameObject popup;

    private PopupResult popupResult = PopupResult.Awaiting;

    private void OnEnable()
    {
        confirmButton.onClick.AddListener(Confirm);
        cancelButton.onClick.AddListener(Cancel);
    }
    private void OnDisable()
    {
        confirmButton.onClick.RemoveListener(Confirm);
        cancelButton.onClick.RemoveListener(Cancel);
    }

    private void Start()
    {
        popup.gameObject.SetActive(false);
    }

    private void ShowPopUp(bool show)
    {
        popup.gameObject.SetActive(show);
    }

    private void Confirm()
    {
        popupResult = PopupResult.Confirmed;
    }
    private void Cancel()
    {
        popupResult = PopupResult.Canceled;
    }

    public async Task<bool> GetPopupResult()
    {
        ShowPopUp(true);
        //wait for user to press a button
        while (popupResult == PopupResult.Awaiting) await Task.Delay(50);

        bool result = false;
        switch (popupResult)
        {
            case PopupResult.Confirmed:
                result = true;
                break;
            case PopupResult.Canceled:
                result = false;
                break;
        }
        ShowPopUp(false);
        popupResult = PopupResult.Awaiting;
        return result;
    }
}