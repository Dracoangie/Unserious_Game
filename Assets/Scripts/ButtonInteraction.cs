using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button3D : MonoBehaviour
{
    public Material normalMaterial;
    public Material highlightedMaterial;
    private Renderer objectRenderer;
    public SceneTransition sceneTransition;

    private AudioManager audioManager;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.material = normalMaterial;
        audioManager = FindObjectOfType<AudioManager>();
    }

    void OnMouseEnter()
    {
        objectRenderer.material = highlightedMaterial;
    }

    void OnMouseExit()
    {
        objectRenderer.material = normalMaterial;
    }

    void OnMouseDown()
    {
        Debug.Log("Boton presionado");
        audioManager.LowerMusic();
        sceneTransition.FadeToScene("PlayScene");
       // SceneManager.LoadScene("SampleScene");
    }
}
