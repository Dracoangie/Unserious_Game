using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResistenciaObjeto : MonoBehaviour
{
    [Range(0f, 1f)]

    public float valorResistencia = 0.5f;
    public float cooldownEmpuje = 0.2f;
    private float tiempoUltimoEmpuje = -Mathf.Infinity;

    public bool PuedeEmpujar()
    {
        return Time.time >= tiempoUltimoEmpuje + cooldownEmpuje;
    }

    public void ReiniciarCooldown()
    {
        tiempoUltimoEmpuje = Time.time;
    }
}

