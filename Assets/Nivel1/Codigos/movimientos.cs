using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class movimientos : MonoBehaviour
{
    [SerializeField]
    CharacterController cc;

    [SerializeField]
    Animator animator;

    [SerializeField]
    float horizontal;

    [SerializeField]
    float vertical;

    [SerializeField]
    PlayerInput pi;

    private Vector3 movimiento;
    private bool bandera1 = false;

    void Start()
    {
        pi = GetComponent<PlayerInput>();
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        bandera1 =  false;
        movimiento = new Vector3(-pi.actions["movimiento"].ReadValue<Vector2>().y,
            0, 
            pi.actions["movimiento"].ReadValue<Vector2>().x);
        
        cc.Move(movimiento * Time.deltaTime * 10f);

        if (pi.actions["movimiento"].ReadValue<Vector2>() != Vector2.zero && !bandera1){
            movimientoAnimacion();
        }
    }

    void movimientoAnimacion()
    {
        Debug.Log("movimiento");
        animator.SetTrigger("cam");
        bandera1 = true;
    }
}
