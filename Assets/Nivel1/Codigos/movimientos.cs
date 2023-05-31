using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class movimientos : MonoBehaviour
{
    private bool animacionActiva = false;
    public PlayerInput pi;
    [SerializeField]
    CharacterController cc;
    [SerializeField]
    float velocidad = 10;
    [SerializeField]
    float alturaSalto = 5f;
    private bool enSuelo = false;

    // Start is called before the first frame update
    void Start()
    {
        pi = GetComponent<PlayerInput>();
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movIn = new Vector3(-pi.actions["movimiento"].ReadValue<Vector2>().y, -9.81f, pi.actions["movimiento"].ReadValue<Vector2>().x);

        // Controlar el movimiento horizontal
        cc.Move(movIn * Time.deltaTime * velocidad);

        // Verificar si el jugador está en el suelo
        enSuelo = cc.isGrounded;

        // Detectar el salto
        if (pi.actions["salto"].triggered && enSuelo)
        {
            // Aplicar la velocidad de salto al jugador
            float velocidadSalto = Mathf.Sqrt(2 * alturaSalto * Mathf.Abs(Physics.gravity.y));
            movIn.y = velocidadSalto;
        }

        // Aplicar gravedad al movimiento
        movIn.y += Physics.gravity.y * Time.deltaTime;

        // Aplicar movimiento al personaje
        cc.Move(movIn * Time.deltaTime);

        // Verificar si se presionó alguna de las teclas W, A, S o D
        if (pi.actions["movimiento"].ReadValue<Vector2>() != Vector2.zero)
        {
            // Activar la animación si no está activa
            if (!animacionActiva)
            {
                animacionActiva = true;
            }
        }
        else
        {
            animacionActiva = false;
        }

        Debug.Log(pi.actions["camara"].triggered);
        //pi.actions["camara"].triggered
    }
}
