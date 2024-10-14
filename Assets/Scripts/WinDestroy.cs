using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinDestroy : MonoBehaviour
{
    public GameObject fragmentoPrefab;  // El prefab que representa un fragmento del objeto
    public int cantidadFragmentos = 10; // Número de fragmentos que aparecerán
    public float fuerzaExplosion = 50f; // Fuerza con la que los fragmentos se dispersarán
    public float tiempoDesaparicionFragmentos = 3f; // Tiempo antes de que los fragmentos desaparezcan
    public float tiempoRalentizacion = 1f; // Tiempo de ralentización
    public float factorRalentizacion = 0.2f; // Factor de ralentización (0.2 = 20% de la velocidad normal)

    private void OnCollisionEnter(Collision collision)
    {
        // Si el objeto colisiona con el suelo
        if (collision.gameObject.CompareTag("Suelo"))
        {
            // Sumar un punto a "Ardilla"
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.AgregarPunto();
                gameManager.RalentizarTiempo(tiempoRalentizacion, factorRalentizacion); // Ralentizar el tiempo
            }
            CameraFollowTwoPlayers camara = FindObjectOfType<CameraFollowTwoPlayers>();
            if (camara != null)
            {
                camara.HacerZoomEnExplosion(transform.position);
            }
            // Reemplazar el objeto actual con fragmentos
            RomperEnFragmentos();

            // Destruir el objeto actual tras romperlo
            Destroy(gameObject);
        }
    }

    private void RomperEnFragmentos()
    {
        for (int i = 0; i < cantidadFragmentos; i++)
        {
            // Instanciar fragmentos alrededor del objeto original
            GameObject fragmento = Instantiate(fragmentoPrefab, transform.position, Random.rotation);

            // Aplicar una fuerza de explosión a cada fragmento para que se dispersen
            Rigidbody rb = fragmento.AddComponent<Rigidbody>();
            Vector3 direccionExplosion = (fragmento.transform.position - transform.position).normalized;
            rb.AddForce(direccionExplosion * fuerzaExplosion, ForceMode.Impulse);

            // Destruir los fragmentos después de 'tiempoDesaparicionFragmentos' segundos
            Destroy(fragmento, tiempoDesaparicionFragmentos);
        }
    }
}