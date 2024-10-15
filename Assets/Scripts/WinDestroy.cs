using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinDestroy : MonoBehaviour
{
    public GameObject fragmentoPrefab;
    public int cantidadFragmentos = 10;
    public float fuerzaExplosion = 50f;
    public float tiempoDesaparicionFragmentos = 3f;
    public float tiempoRalentizacion = 1f;
    public float factorRalentizacion = 0.2f;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Suelo"))
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.AgregarPunto();
                gameManager.RalentizarTiempo(tiempoRalentizacion, factorRalentizacion);
            }
            CameraFollowTwoPlayers camara = FindObjectOfType<CameraFollowTwoPlayers>();
            if (camara != null)
            {
                camara.HacerZoomEnExplosion(transform.position);
            }
            RomperEnFragmentos();

            Destroy(gameObject);
        }
    }

    private void RomperEnFragmentos()
    {
        for (int i = 0; i < cantidadFragmentos; i++)
        {
            GameObject fragmento = Instantiate(fragmentoPrefab, transform.position, Random.rotation);

            Rigidbody rb = fragmento.AddComponent<Rigidbody>();
            Vector3 direccionExplosion = (fragmento.transform.position - transform.position).normalized;
            rb.AddForce(direccionExplosion * fuerzaExplosion, ForceMode.Impulse);

            Destroy(fragmento, tiempoDesaparicionFragmentos);
        }
    }
}