using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    
    public float particleSpeed;
    public bool speedUp;
    public GameController gc;

    private ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        particleSpeed = 4;
        speedUp = false;
    }

    void Update()
    {
        var main = ps.main;
        main.simulationSpeed = particleSpeed;

        if (gc.win == true)
        {
            speedUp = true;
        }

        if (speedUp)
            {
                particleSpeed = 14;
            }
    }

}
