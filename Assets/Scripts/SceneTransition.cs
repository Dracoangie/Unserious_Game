using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public Image fadeImage; // Referencia a la imagen de la cortinilla
    public float fadeDuration = 1.0f; // Duración del efecto de cortinilla

    private void Start()
    {
        // Iniciar la imagen como completamente transparente
        fadeImage.color = new Color(0, 0, 0, 0);
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    private IEnumerator FadeOut(string sceneName)
    {
        // Hacer la imagen visible
        fadeImage.gameObject.SetActive(true);

        // Llenar el color de la imagen para hacer el fade
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = t / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, alpha); // Ajusta el color de la imagen
            yield return null;
        }

        // Cargar la nueva escena
        SceneManager.LoadScene(sceneName);
    }
}