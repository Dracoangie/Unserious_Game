using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int puntosArdilla = 0;
    public void AgregarPunto()
    {
        puntosArdilla++;
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
