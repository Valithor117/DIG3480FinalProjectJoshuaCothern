using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContactPowerup : MonoBehaviour
{
    public GameObject explosion;
    public bool powerUp;

    private GameController gameController;
    private GameObject playerController; 
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if (other.tag == "Player")
        {
            powerUp = true;           
        }


        Destroy(gameObject);

    }
}
