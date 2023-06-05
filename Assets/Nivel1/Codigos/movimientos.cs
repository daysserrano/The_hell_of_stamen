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

    [SerializeField]
    float gravedad = 9.8f;

    [SerializeField]
    float fuerzaSalto = 10f;

    [SerializeField]
    GameObject objetoPrefab;

    [SerializeField]
    float distancia = 1f;

    private float velocidadRotacion = 1f;

    private Vector3 movimiento;
    private bool bandera1 = false;

    private bool ePresionada = false;

    private GameObject objetoGenerado;

    private bool objetoGenerado1 = false;

    void Start()
    {
        pi = GetComponent<PlayerInput>();
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {

        float movimientoMouseX = Input.GetAxis("Mouse X");
        // Rotar el objeto en el eje Y según el movimiento del mouse
        transform.Rotate(Vector3.up, movimientoMouseX * velocidadRotacion);

        bandera1 =  false;
        movimiento = Quaternion.Euler(0f, transform.eulerAngles.y, 0f) * new Vector3(-pi.actions["movimiento"].ReadValue<Vector2>().y, 0, pi.actions["movimiento"].ReadValue<Vector2>().x);

        movimiento = movimiento * 4f;
        Agravedad();
        Salto();
        cc.Move(movimiento * Time.deltaTime);

        if (pi.actions["movimiento"].ReadValue<Vector2>() != Vector2.zero && !bandera1){
            movimientoAnimacion();
        }else animator.SetBool("caminar", false);

        Habilidades();
        
    }

    void Habilidades()
    {
        if(Input.GetMouseButtonDown(0)){
            animator.SetBool("tak1", true);
        }else animator.SetBool("tak1", false);

        if(Input.GetMouseButtonDown(1)){
            animator.SetBool("tak2", true);
        }else animator.SetBool("tak2", false);

         if (Input.GetKeyDown(KeyCode.E) && !ePresionada)
        {
            animator.SetBool("escudo", true);
            ePresionada = true;
            Debug.Log("Acabas de presionar E");
        }

        if (Input.GetKey(KeyCode.E) && ePresionada)
        {
            animator.SetBool("gira", true);
            Debug.Log("Estás manteniendo presionada la tecla E");
            if(!objetoGenerado1) GenerarObjeto();
        }else animator.SetBool("gira", false);

        if (Input.GetKeyUp(KeyCode.E) && ePresionada)
        {
            animator.SetBool("escudo", false);
            ePresionada = false;
            if(objetoGenerado1) DestruirObjetoGenerado();
            Debug.Log("Dejaste de presionar la tecla E");
        }
    }

    void GenerarObjeto()
    {
        Vector3 posicionGeneracion = transform.position + transform.forward * distancia + transform.right * -0.6f;
        objetoGenerado = Instantiate(objetoPrefab, posicionGeneracion, Quaternion.Euler(0f, 90f, 0f));
        objetoGenerado.transform.parent = transform;
        objetoGenerado1 = true;

    }
    void DestruirObjetoGenerado()
    {
        Destroy(objetoGenerado);
        objetoGenerado1 = false;
    }

    void Agravedad()
    {
        if(cc.isGrounded)
        {
            movimiento.y = -gravedad * Time.deltaTime; 
        }else
        {
            movimiento.y -= gravedad * Time.deltaTime * 4;
        }
    }

    void Salto()
    {
        if(cc.isGrounded && pi.actions["salto"].triggered)
        {
            movimiento.y = fuerzaSalto;
            animator.SetBool("salto", true);
        }else animator.SetBool("salto", false);
    }

    void movimientoAnimacion()
    {
        animator.SetBool("caminar", true);
        bandera1 = true;
    }
}
