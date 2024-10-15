using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int puntosArdilla = 0;
    public int puntosEmpresario = 0;

    public int puntosWinArdilla = 5;
    public int puntosWinEmpresario = 3;

    public Image hudImageAr;
    public Image hudImageEm;
    public CharacterMove jugador1;
    public CharacterMove jugador2;

    public Transform puntosArdillaContainer;
    public Transform puntosEmpresarioContainer;
    public GameObject puntoArdillaPrefab;
    public GameObject puntoEmpresarioPrefab;
    private audioGameManager audiogameManager;

    private void Awake()
    {
        audiogameManager = FindObjectOfType<audioGameManager>();
    }

    private void Update()
    {
        if (puntosArdilla >= puntosWinArdilla || puntosEmpresario >= puntosWinEmpresario)
        {
            if (hudImageAr != null && puntosArdilla >= puntosWinArdilla)
            {
                hudImageAr.gameObject.SetActive(true);
            }
            else if (hudImageEm != null && puntosEmpresario >= puntosWinEmpresario)
                hudImageEm.gameObject.SetActive(true);

            if (jugador1 != null && jugador2 != null)
            {
                jugador1.ToggleControl(false);
                jugador2.ToggleControl(false);
            }
        }
    }

    public void AgregarPunto()
    {
        puntosArdilla++;
        audiogameManager.SelectAudio(0, 0.5f);
        ActualizarPuntosArdilla();
    }

    public void AgregarPunto2()
    {
        puntosEmpresario++;
        ActualizarPuntosEmrpesario();
    }

    private void ActualizarPuntosArdilla()
    {
        foreach (Transform child in puntosArdillaContainer)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < Mathf.Min(puntosArdilla, puntosWinArdilla); i++)
        {
            Instantiate(puntoArdillaPrefab, puntosArdillaContainer);
        }
    }

    private void ActualizarPuntosEmrpesario()
    {
        foreach (Transform child in puntosEmpresarioContainer)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < Mathf.Min(puntosEmpresario, puntosWinEmpresario); i++)
        {
            Instantiate(puntoEmpresarioPrefab, puntosEmpresarioContainer);
        }
    }

    public void RalentizarTiempo(float tiempoRalentizacion, float factorRalentizacion)
    {
        StartCoroutine(RalentizarCoroutine(tiempoRalentizacion, factorRalentizacion));
    }

    private IEnumerator RalentizarCoroutine(float tiempoRalentizacion, float factorRalentizacion)
    {
        float tiempo = 0f;

        while (tiempo < tiempoRalentizacion)
        {
            Time.timeScale = Mathf.Lerp(1f, factorRalentizacion, tiempo / tiempoRalentizacion);
            tiempo += Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = factorRalentizacion;

        yield return new WaitForSecondsRealtime(tiempoRalentizacion);

        tiempo = 0f;

        while (tiempo < tiempoRalentizacion)
        {
            Time.timeScale = Mathf.Lerp(factorRalentizacion, 1f, tiempo / tiempoRalentizacion);
            tiempo += Time.unscaledDeltaTime;
            yield return null;
        }
        Time.timeScale = 1f;
    }
}
