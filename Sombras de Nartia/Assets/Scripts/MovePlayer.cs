using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public bool sePuedeMover = true;
    [SerializeField] private Vector2 velocidadRebote;

    [Header("Movimiento")]
    private float moveHorizontal = 0f;
    [SerializeField] private float velocidadMov;
    [Range(0, 0.3f)][SerializeField] private float suavisado;
    private Vector3 velocidad = Vector3.zero;
    private bool mirandoDerecha = true;

    [Header("Salto")]
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Vector3 dimensionesCaja;
    [SerializeField] private bool enSuelo;
    private bool salto = false;
    Animator animator;
    public GameObject shadow;

    private DamageSistem damageSistem;

    private EntradasMov inputActions;

    private void Awake()
    {
        inputActions = new EntradasMov();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageSistem = GetComponent<DamageSistem>();
        inputActions.Movimiento.Salto.performed += contexto => Salto(contexto);
    }

    private void Update()
    {
        moveHorizontal = inputActions.Movimiento.Horizontal.ReadValue<float>() * velocidadMov;

        CheckOutOfBounds();
    }

    private void Salto(InputAction.CallbackContext context)
    {
        salto = true;
    }

    private void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);
        if (sePuedeMover)
        {
            Mover(moveHorizontal * Time.fixedDeltaTime, salto);
        }
        
        salto = false;
    }

    private void Mover(float mover, bool saltar)
    {
        Vector3 velocidadObjetivo = new Vector2(mover, rb2D.velocity.y);
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, velocidadObjetivo, ref velocidad, suavisado);

        if (mover > 0 && !mirandoDerecha)
        {
            // Girar
            Girar();
        } else if (mover < 0 && mirandoDerecha)
        {
            // Girar
            Girar();
        }

        if (mover != 0)
        {
            animator.SetBool("1_Move", true);
        } else
        {
            animator.SetBool("1_Move", false);
        }

        if (enSuelo && saltar)
        {
            enSuelo = false;
            rb2D.AddForce(new Vector2(0f, fuerzaSalto));
        }

        shadow.SetActive(enSuelo);
    }
    private void CheckOutOfBounds()
    {
        // Obtener las coordenadas de la cámara
        Camera mainCamera = Camera.main;
        Vector3 screenBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));

        // Comprobar si el jugador está por debajo del límite inferior de la cámara
        if (transform.position.y < screenBottomLeft.y)
        {
            StartCoroutine(damageSistem.Morir());
        }
    }

    public void Rebote(Vector2 puntoGolpe)
    {
        rb2D.velocity = new Vector2(-velocidadRebote.x * puntoGolpe.x, velocidadRebote.y);
    }

    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
    }
}
