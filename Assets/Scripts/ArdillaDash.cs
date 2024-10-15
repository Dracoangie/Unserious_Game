using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArdillaDash : MonoBehaviour
{

    public float dashDistancia = 5f;
    public float dashDuracion = 0.2f;
    public KeyCode teclaDash = KeyCode.Alpha0;
    public float dashCooldown = 2.5f;

    private bool estaDashing = false;
    private bool puedeHacerDash = true;
    private Vector3 direccionDash;
    private Rigidbody rb;

    private audioGameManager audiogameManager;

    private void Awake()
    {
        audiogameManager = FindObjectOfType<audioGameManager>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(teclaDash) && !estaDashing && puedeHacerDash)
        {
            audiogameManager.SelectAudio(2, 0.5f);
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        estaDashing = true;
        puedeHacerDash = false;

        direccionDash = transform.forward;

        float tiempoTranscurrido = 0f;
        Vector3 posicionInicial = transform.position;
        Vector3 posicionFinal = transform.position + direccionDash * dashDistancia;

        while (tiempoTranscurrido < dashDuracion)
        {
            rb.MovePosition(Vector3.Lerp(posicionInicial, posicionFinal, tiempoTranscurrido / dashDuracion));
            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }

        estaDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        puedeHacerDash = true;
    }
}
