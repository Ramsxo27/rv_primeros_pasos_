using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// Script de Linny referencias
public class UIElementXR : MonoBehaviour
{
    public UnityEvent OnXRPointerEnter;
    public UnityEvent OnXRPointerExit;

    private Camera xRCamera;

    // Start is called before the first frame update
    void Start()
    {
        xRCamera = CameraPointerManager.instance.gameObject.GetComponent<Camera>();
    }

    // Click XR
    public void OnPointerClickXR()
    {
        PointerEventData pointerEvent = PlacePointer();
        ExecuteEvents.Execute(gameObject, pointerEvent, ExecuteEvents.pointerClickHandler);
    }

    // Pointer Enter
    public void OnPointerEnterXR()
    {
        GazeManager.Instance.SetUpGaze(1.5f);
        OnXRPointerEnter?.Invoke();

        PointerEventData pointerEvent = PlacePointer();
        ExecuteEvents.Execute(gameObject, pointerEvent, ExecuteEvents.pointerDownHandler);
    }

    // Pointer Exit
    public void OnPointerExitXR()
    {
        GazeManager.Instance.SetUpGaze(2.5f);
        OnXRPointerExit?.Invoke();

        PointerEventData pointerEvent = PlacePointer();
        ExecuteEvents.Execute(gameObject, pointerEvent, ExecuteEvents.pointerUpHandler);
    }

    // Crear evento de puntero
    public PointerEventData PlacePointer()
    {
        Vector3 screenPos = xRCamera.WorldToScreenPoint(CameraPointerManager.instance.hitPoint);

        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = new Vector2(screenPos.x, screenPos.y);

        return pointer;
    }
}