using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTwoPlayers : MonoBehaviour
{
    public Transform jugador1;
    public Transform jugador2;

    public Vector3 offset = new Vector3(0, 10, -10);
    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomLimiter = 50f;
    public float tiempoFocalizado = 0.3f;
    public float duracionZoom = 0.5f;

    public float smoothSpeed = 0.5f;

    private Camera cam;
    private bool haciendoZoom = false;
    private Vector3 posicionObjetivo;
    private float zoomObjetivo;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (jugador1 == null || jugador2 == null)
            return;

        if (haciendoZoom)
        {
            MoverCamara(posicionObjetivo);
        }
        else
        {
            Vector3 centroEntreJugadores = ObtenerCentroEntreJugadores();
            MoverCamara(centroEntreJugadores);
            AjustarZoom();
        }
    }

    private Vector3 ObtenerCentroEntreJugadores()
    {
        Vector3 puntoMedio = (jugador1.position + jugador2.position) / 2;
        return puntoMedio;
    }

    private void MoverCamara(Vector3 posicionObjetivo)
    {
        Vector3 nuevaPosicion = posicionObjetivo + offset;
        transform.position = Vector3.Lerp(transform.position, nuevaPosicion, smoothSpeed * Time.deltaTime);
    }

    private void AjustarZoom()
    {
        float distanciaEntreJugadores = Vector3.Distance(jugador1.position, jugador2.position);
        float nuevoZoom = Mathf.Lerp(maxZoom, minZoom, distanciaEntreJugadores / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, nuevoZoom, Time.deltaTime);
    }

    public void HacerZoomEnExplosion(Vector3 posicion)
    {
        haciendoZoom = true;
        posicionObjetivo = posicion;

        StartCoroutine(ZoomTemporal());
    }

    private IEnumerator ZoomTemporal()
    {
        float zoomInicial = cam.fieldOfView;
        zoomObjetivo = 20f;

        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < duracionZoom)
        {
            cam.fieldOfView = Mathf.Lerp(zoomInicial, zoomObjetivo, tiempoTranscurrido / duracionZoom);
            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(tiempoFocalizado);

        haciendoZoom = false;
        tiempoTranscurrido = 0f;
        while (tiempoTranscurrido < duracionZoom)
        {
            cam.fieldOfView = Mathf.Lerp(zoomObjetivo, zoomInicial, tiempoTranscurrido / duracionZoom);
            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }
    }
}
