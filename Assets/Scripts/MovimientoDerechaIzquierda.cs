using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoAdelanteAtras : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    public float rangoMovimiento = 5f;
    public float VelocidadNecesariaDelObjeto = 15f;
    public float factorDeEscalaDetencion = 0.1f;
    private bool estaDetenido = false;
    private Vector3 posicionInicial;
    private float tiempoDesdeInicio;

    void Start()
    {
        posicionInicial = transform.position;
        tiempoDesdeInicio = 0f;
    }

    void Update()
    {
        if (!estaDetenido)
        {
            tiempoDesdeInicio += Time.deltaTime;
            float movimiento = Mathf.Sin(tiempoDesdeInicio * velocidadMovimiento) * rangoMovimiento;
            transform.position = new Vector3(transform.position.x, transform.position.y, posicionInicial.z + movimiento);
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
        yield return new WaitForSeconds(tiempoDetenido);
        estaDetenido = false;
    }
}