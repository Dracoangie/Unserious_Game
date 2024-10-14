using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoAdelanteAtras : MonoBehaviour
{
    public float VelocidadNecesariaDelObjeto = 15f;
    public float factorDeEscalaDetencion = 0.1f;
    public CharacterMove jugador; 
    private bool estaDetenido = false;

    void Start()
    {
        if (jugador == null)
        {
            jugador = FindObjectOfType<CharacterMove>();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;
        if (rb != null && rb.velocity.magnitude >= VelocidadNecesariaDelObjeto)
        {
            float tiempoDetenido = rb.velocity.magnitude * factorDeEscalaDetencion;
            StartCoroutine(DetenidoCoroutine(tiempoDetenido));
        }
    }

    private System.Collections.IEnumerator DetenidoCoroutine(float tiempoDetenido)
    {
        estaDetenido = true;

        if (jugador != null)
        {
            jugador.ToggleControl(false);
            Debug.Log("Control desactivado");
        }

        yield return new WaitForSeconds(tiempoDetenido);
        if (jugador != null)
        {
            jugador.ToggleControl(true);
            Debug.Log("Control activado");
        }

        estaDetenido = false;
    }
}