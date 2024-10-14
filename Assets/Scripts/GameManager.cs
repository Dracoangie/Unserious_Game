using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int puntosArdilla = 0;
    public int puntosEmpresario = 0;
    public Image hudImageAr;
    public Image hudImageEm;
    public CharacterMove jugador1;
    public CharacterMove jugador2;

    private void Update()
    {
        if(puntosArdilla >= 5 || puntosEmpresario >= 3)
        {
            if (hudImageAr != null && puntosArdilla >= 5)
            {
                hudImageAr.gameObject.SetActive(true);
            }else if(hudImageEm != null && puntosEmpresario >= 3)
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
    }
    public void AgregarPunto2()
    {
        puntosEmpresario++;
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
