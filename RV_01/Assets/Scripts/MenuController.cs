using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{

    private Camera xRCamera;
    [SerializeField] Text puntaje;
    private void Start()
    {
        xRCamera = CameraPointerManager.instance.gameObject.GetComponent<Camera>();

        int puntajes = PlayerPrefs.GetInt("Puntaje", 1);
        if (puntajes > 0 && puntaje is not null)
        {
            puntaje.text = "Puntaje: " + puntajes.ToString();
        }

    }
    public void menu_principal()
    {
        SceneManager.LoadScene("Menu_Principal");
    }
    public void reiniciar()
    {
        SceneManager.LoadScene("HelloCardboard");
    }
    public void game_over()
    {
        SceneManager.LoadScene("Menu_Game_Over");
    }
    public void EndGame()
    {
        Application.Quit();
    }
    public void Creditos()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
    }
    public void Menu()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
    }
    public void OnPointerClickXR()
    {
        PointerEventData pointerEvent = PlacePointer();
        ExecuteEvents.Execute(this.gameObject, pointerEvent, ExecuteEvents.pointerClickHandler);
    }
    public PointerEventData PlacePointer()
    {
        Vector3 screenPos = xRCamera.WorldToScreenPoint(CameraPointerManager.instance.hitPoint);
        var pointer = new PointerEventData(EventSystem.current);
        pointer.position = new Vector2(screenPos.x, screenPos.y);
        return pointer;
    }
}