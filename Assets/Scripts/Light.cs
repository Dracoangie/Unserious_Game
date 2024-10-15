using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light spotlight;  // Referencia a la luz que parpadea
    public Renderer coneRenderer;  // Referencia al Renderer del cono para activar/desactivar el material
    public float minIntensity = 0.2f;
    public float maxIntensity = 1.0f;
    public float flickerSpeed = 0.1f;
    public float flickerThreshold = 0.5f;  // Umbral de parpadeo

    void Start()
    {
        // Asegúrate de tener una referencia a la luz y al renderer del cono
        if (spotlight == null) 
        {
            spotlight = GetComponent<Light>();
        }

        if (coneRenderer == null) 
        {
            coneRenderer = GetComponent<Renderer>();
        }
    }

    void Update()
    {
        // Genera un valor aleatorio de intensidad para simular el parpadeo
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.PerlinNoise(Time.time * flickerSpeed, 0.0f));
        spotlight.intensity = intensity;

        // Controla el estado del cono (activar/desactivar material) en función de la intensidad
        if (intensity > flickerThreshold)
        {
            // Si la intensidad supera el umbral, activa el material (haz visible el cono)
            coneRenderer.enabled = true;
        }
        else
        {
            // Si la intensidad es menor al umbral, desactiva el material (haz invisible el cono)
            coneRenderer.enabled = false;
        }
    }
}
