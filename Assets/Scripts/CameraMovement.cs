using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    private Camera mainCam;

    [SerializeField]
    private float movementSpeed = 20f;
    [SerializeField]
    private float scrollModifier = 5f;
    private float currentZoom;

    private bool holdingDragButton = false;
    private bool shiftHeld = false;

    private void Start()
    {
        mainCam = Camera.main;
        currentZoom = mainCam.orthographicSize;
    }

    public void OnZoom(InputValue value)
    {
        float zoomDirection = value.Get<Vector2>().y;
        if (shiftHeld) zoomDirection *= 4;
        currentZoom -= zoomDirection * scrollModifier;
        currentZoom = Mathf.Clamp(currentZoom, 5, Mathf.Infinity);
        mainCam.orthographicSize = currentZoom;
    }
    public void OnMouseMove(InputValue value)
    {
        Vector3 delta = value.Get<Vector2>();
        if (holdingDragButton)
        {
            mainCam.transform.position -= delta * (movementSpeed * currentZoom) * Time.deltaTime;
        }
    }

    public void OnMiddleMouse(InputValue value)
    {
        holdingDragButton = value.isPressed;
    }
    public void OnShiftHeld(InputValue value)
    {
        shiftHeld = value.isPressed;
    }

    private void Update()
    {
        //mainCam.transform.position += movement * (movementSpeed * currentZoom) * Time.deltaTime;
    }
}
