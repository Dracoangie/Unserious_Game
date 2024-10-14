using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoAdelanteAtras : MonoBehaviour
{
    public float VelocidadNecesariaDelObjeto = 15f;
    public float factorDeEscalaDetencion = 0.1f;
    public CharacterMove jugador; 

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
        if (rb != null && rb.velocity.magnitude >= VelocidadNecesariaDelObjeto && collision.gameObject.CompareTag("Lanzable"))
        {
            float tiempoDetenido = rb.velocity.magnitude * factorDeEscalaDetencion;
            StartCoroutine(DetenidoCoroutine(tiempoDetenido));
        }
    }

    private System.Collections.IEnumerator DetenidoCoroutine(float tiempoDetenido)
    {
        if (jugador != null)
        {
            jugador.ToggleControl(false);
        }

        yield return new WaitForSeconds(tiempoDetenido);
        if (jugador != null)
        {
            jugador.ToggleControl(true);
        }
    }
}