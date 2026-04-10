using UnityEngine;
using System.Collections;
public class Bullets : MonoBehaviour {

    public float speed = 1f;
    public Vector3 direction;

    private float lifetime = 2f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {



        transform.position += direction * speed * Time.deltaTime;
        lifetime -= Time.deltaTime;
        if (lifetime <= 0) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collider) {

        if (collider.gameObject.tag == "Enemy") {

            Destroy(collider.gameObject);
            Destroy(gameObject);
        }
    }
}


