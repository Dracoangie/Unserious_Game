using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpujarObjetos : MonoBehaviour
{
    public float fuerzaBase = 100f;
    public float fuerzaVertical = 50f;
    private List<GameObject> objetosEmpujables = new List<GameObject>();
    private audioGameManager audiogameManager;

    private void Awake()
    {
        audiogameManager = FindObjectOfType<audioGameManager>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0) && objetosEmpujables.Count > 0)
        {
            audiogameManager.SelectAudio(1, 1f);
            foreach (GameObject objetoEmpujable in objetosEmpujables)
            {
                ResistenciaObjeto resistencia = objetoEmpujable.GetComponent<ResistenciaObjeto>();
                if (resistencia != null && resistencia.PuedeEmpujar())
                {
                    EmpujarObjeto(objetoEmpujable);
                    resistencia.ReiniciarCooldown();
                }
            }
        }}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lanzable") && !objetosEmpujables.Contains(other.gameObject))
        {
            objetosEmpujables.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Lanzable") && objetosEmpujables.Contains(other.gameObject))
        {
            objetosEmpujables.Remove(other.gameObject);
        }
    }

    void EmpujarObjeto(GameObject objeto)
    {
        Rigidbody rb = objeto.GetComponent<Rigidbody>();

        if (rb != null)
        {
            ResistenciaObjeto resistencia = objeto.GetComponent<ResistenciaObjeto>();

            if (resistencia != null)
            {
                float fuerzaAjustada = fuerzaBase * (1 - resistencia.valorResistencia);

                Vector3 direccionEmpuje = transform.forward + Vector3.up * (fuerzaVertical / fuerzaBase);

                rb.AddForce(direccionEmpuje.normalized * fuerzaAjustada, ForceMode.Impulse);
            }
        }
    }
}