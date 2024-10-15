using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoParticula : MonoBehaviour
{
    public ParticleSystem particula;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (rb.velocity.magnitude > 0.1f)
        {
            if (!particula.isPlaying)
            {
                particula.Play();
            }
        }
        else
        {
            if (particula.isPlaying)
            {
                particula.Stop();
            }
        }
    }
}
