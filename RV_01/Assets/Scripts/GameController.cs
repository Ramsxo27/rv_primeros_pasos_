using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public Camera gameCamera;
    public GameObject bulletPrefab;
    public GameObject enemyPrefab;

    [SerializeField] GameObject menu;
    [SerializeField] TMP_Text puntaje;

    public float enemySpawningCooldown = 1f;
    public float enemySpawningDistance = 7f;
    public float shootingCooldown = 0.5f;

    private float enemySpawningTimer = 0f;
    private float shootingTimer = 0f;
    private int npuntaje = 0;

    void Start()
    {
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            menu_principal();
        }
    }

    void Update()
    {
        shootingTimer -= Time.deltaTime;
        enemySpawningTimer -= Time.deltaTime;

        // Spawn de enemigos
        if (enemySpawningTimer <= 0f && menu.activeSelf == false)
        {
            enemySpawningTimer = enemySpawningCooldown;

            GameObject enemyObject = Instantiate(enemyPrefab);

            float randomAngle = Random.Range(0, Mathf.PI * 2);

            Vector3 spawnPosition = new Vector3(
                gameCamera.transform.position.x + Mathf.Cos(randomAngle) * enemySpawningDistance,
                gameCamera.transform.position.y,
                gameCamera.transform.position.z + Mathf.Sin(randomAngle) * enemySpawningDistance
            );

            enemyObject.transform.position = spawnPosition;

            Enemy enemy = enemyObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.direction = (gameCamera.transform.position - enemyObject.transform.position).normalized;
            }

            enemyObject.transform.LookAt(gameCamera.transform.position);
        }

        // Disparo
        RaycastHit hit;
        if (Physics.Raycast(gameCamera.transform.position, gameCamera.transform.forward, out hit))
        {
            if (hit.transform.CompareTag("Enemy") && shootingTimer <= 0f)
            {
                shootingTimer = shootingCooldown;

                GameObject bulletObject = Instantiate(bulletPrefab);
                bulletObject.transform.position = gameCamera.transform.position;

                Bullets bullet = bulletObject.GetComponent<Bullets>();
                if (bullet != null)
                {
                    bullet.direction = gameCamera.transform.forward;
                }

                npuntaje += 100;
                puntaje.text = "Puntaje: " + npuntaje;
            }
        }
    }

    public void menu_principal()
    {
        SceneManager.LoadScene("Menu_principal");
    }
}