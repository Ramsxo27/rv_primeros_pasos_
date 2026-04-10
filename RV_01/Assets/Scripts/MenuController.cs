using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    private Camera XRCamera;

    // Start is called before the first frame update
    void Start()
    {
        XRCamera = CameraPointerManager.instance.gameObject.GetComponent<Camera>();
    }

    public void menu_principal()
    {
        SceneManager.LoadScene("Menu_principal");
    }

    public void reiniciar()
    {
        SceneManager.LoadScene("Juego_principal_vr");
    }

    public void game_over()
    {
        SceneManager.LoadScene("Menu_Game_Over");
    }

    public void OnPointerClickXR()
    {
        PointerEventData pointerEvent = PlacePointer();
        ExecuteEvents.Execute(this.gameObject, pointerEvent, ExecuteEvents.pointerClickHandler);
    }

    public PointerEventData PlacePointer()
    {
        Vector3 screenPos = XRCamera.WorldToScreenPoint(CameraPointerManager.instance.hitPoint);

        var pointer = new PointerEventData(EventSystem.current);
        pointer.position = new Vector2(screenPos.x, screenPos.y);

        return pointer;
    }
}