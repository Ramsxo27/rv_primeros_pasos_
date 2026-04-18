using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region Referencias a Objetos en el juego
    public Camera gameCamera;
    public GameObject bulletPrefab;
    public GameObject enemyPrefab;
    [SerializeField] Text puntaje;
    #endregion

    #region Configuración de temporizadores y puntaje
    public float enemySpawningCooldown = 1f;
    public float enemySpawningDistance = 7f;
    public float shootingCooldown = 0.5f;

    private float enemySpawningTimer = 0;
    private float shootingTimer = 0;
    private int npuntaje = 0;
    #endregion
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        if (other.tag == "Enemy")
        {
            // Guardamos el puntaje en PlayerPrefs
            PlayerPrefs.SetInt("Puntaje", npuntaje);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Menu_Game_Over"); //Cargamos la escena del Game Over
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Restamos tiempo a los temporizadores
        shootingTimer -= Time.deltaTime;
        enemySpawningTimer -= Time.deltaTime;
        //Si el temporizador para la aparicion de enemigos llega a 0 
        if (enemySpawningTimer <= 0f)
        {
            enemySpawningTimer = enemySpawningCooldown;

            GameObject enemyObject = Instantiate(enemyPrefab); //Creamos un nuevo enemigo
            float randomAngle = Random.Range(0, Mathf.PI * 2); //Generamos un numero aleatorio
            enemyObject.transform.position = new Vector3( //Posicionamos al Enemigo en una posición aleatoria en el eje x,y
                gameCamera.transform.position.x + Mathf.Cos(randomAngle) * enemySpawningDistance,
                0,
                gameCamera.transform.position.y + Mathf.Sin(randomAngle) * enemySpawningDistance
                );
            //seleccionamos el enemigo creado y configuramos su dirección para que apunte hacia donde esta el player (Camera)
            Enemy enemy = enemyObject.GetComponent<Enemy>();
            enemy.direction = (gameCamera.transform.position - enemy.transform.position).normalized;
            enemy.transform.forward = enemy.direction;
        }
        RaycastHit hit;

        if (Physics.Raycast(gameCamera.transform.position, gameCamera.transform.forward, out hit))
        {
            if (hit.transform.tag == "Enemy" && shootingTimer <= 0f) // Si el raycast golpea a un objeto con el tag "Enemy" y el temporizador de disparo está en cero
            {
                shootingTimer = shootingCooldown;  // Reinicia el temporizador de disparo
                GameObject bulletObject = Instantiate(bulletPrefab);
                bulletObject.transform.position = gameCamera.transform.position; // Instancia una bala en la posición de la cámara

                Bullets bullet = bulletObject.GetComponent<Bullets>();
                bullet.direction = gameCamera.transform.forward; // Configura la dirección de la bala hacia adelante

                npuntaje += 100;
                puntaje.text = "Puntaje: " + npuntaje; // Aumenta el puntaje y actualiza el texto del puntaje en la interfaz de usuario
            }
        }
    }
}
