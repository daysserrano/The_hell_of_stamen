using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class movimientos : MonoBehaviour
{

    public PlayerInput pi;
    [SerializeField]
    CharacterController cc;
    [SerializeField]
    float velocidad = 10;
    // Start is called before the first frame update
    void Start()
    {
        pi = GetComponent<PlayerInput>();
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movIn = new Vector3 (-pi.actions["movimiento"].ReadValue<Vector2>().y,-9.81f,pi.actions["movimiento"].ReadValue<Vector2>().x);
        cc.Move(movIn * Time.deltaTime * velocidad);
        // Verificar si se presionó alguna de las teclas W, A, S o D
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            Debug.Log("¡Hola!");
        }




    }
}
