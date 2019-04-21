using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public Boundary boundary;
    public float speed;
    public float tilt;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float evadeDistance;
    public GameObject phase;
    public GameObject triShot;
    public Transform powerShotSpawn1;
    public Transform powerShotSpawn2;
    public GameObject powerUpExplosion;
    public bool powerUp; 

    private Rigidbody rb;
    private float nextFire;
    private AudioSource audioSource;
    private bool evade;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        evade = false;
        powerUp = false; 
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audioSource.Play();
        }

       
        if (Input.GetKeyDown("space"))
        {
            evade = true;
        }

        if (Input.GetKeyUp("space"))
        {
            evade = false;
        }


        if (powerUp == true) //tripleshot
        {
            Instantiate(triShot, rb.transform.position, rb.transform.rotation);

            if (Input.GetButton("Fire2") && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                Instantiate(shot, powerShotSpawn1.position, powerShotSpawn1.rotation);
                Instantiate(shot, powerShotSpawn2.position, powerShotSpawn2.rotation);
                audioSource.Play();
            }
        }

        if (Input.GetKey("escape"))
            Application.Quit();

    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        


        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Vector3 dodge = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        
        rb.position = new Vector3
         (
            Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
            0.0f, 
            Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
         );



       if (evade) //dodge up
       {
            rb.position = new Vector3(rb.position.x, evadeDistance, rb.position.z);
            Instantiate(phase, rb.transform.position, rb.transform.rotation);
       }

       

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        }

        if (powerUpExplosion != null)
        {
            Instantiate(powerUpExplosion, transform.position, transform.rotation);
        }

        if (other.tag == "PowerUp")
        {
            StartCoroutine(PowerUpTime(5.0f));
            powerUp = true; 
        }


        Destroy(other.gameObject);

    }

    IEnumerator PowerUpTime(float waitTime)
    {
        yield return new WaitForSeconds(5);
        powerUp = false;
            
    }
}
