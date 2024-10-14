using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorTeleport : MonoBehaviour
{
    public Transform jugador1;
    public float rangoReaparicion = 10f;
    public GameManager gameManager;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == jugador1)
        {
            gameManager.AgregarPunto2();
            gameObject.SetActive(false);
            Reaparecer();
        }
    }

    private void Reaparecer()
    {
        Vector3 nuevaPosicion = new Vector3(
            Random.Range(-rangoReaparicion, rangoReaparicion),
            transform.position.y,
            Random.Range(-rangoReaparicion, rangoReaparicion)
        );

        transform.position = nuevaPosicion;

        gameObject.SetActive(true);
    }
}
