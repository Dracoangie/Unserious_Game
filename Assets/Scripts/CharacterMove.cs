using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMove : MonoBehaviour
{
    public string horizontalInput = "Horizontal";
    public string verticalInput = "Vertical";
    public float speed = 6;
    public float rotSpeed = 20;
    private Rigidbody rig;
    private Vector2 input;
    private Vector3 movementVector;
    private bool controlActivado = true;


    private void Start() {
        rig = GetComponent<Rigidbody>();
        rig.freezeRotation = true;
    }

    private void Update() {
        if (controlActivado)  // Solo aceptar input si control está activado
        {
            input = new Vector2(Input.GetAxis(horizontalInput), Input.GetAxis(verticalInput));
        }
        else
        {
            input = Vector2.zero; // No moverse si el control está desactivado
        }

    }

    private void FixedUpdate() {
        if (controlActivado)  // Solo mover si control está activado
        {
            movementVector = new Vector3(input.x, 0, input.y) * speed;
            rig.velocity = new Vector3(movementVector.x, rig.velocity.y, movementVector.z);

            if (input != Vector2.zero)
            {
                Vector3 targetDirection = new Vector3(input.x, 0, input.y);
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                rig.rotation = Quaternion.Slerp(rig.rotation, targetRotation, rotSpeed * Time.fixedDeltaTime);
            }
        }
        else
        {
            // Asegurarse de que no hay movimiento
            rig.velocity = new Vector3(0, rig.velocity.y, 0);
        }
    }
    public void ToggleControl(bool activar)
    {
        controlActivado = activar;
        if (!activar)
        {
            rig.velocity = Vector3.zero;
        }
    }


}
