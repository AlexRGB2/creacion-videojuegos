using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private Rigidbody2D rb2D;

    [Header("Movimiento")]
    private float moveHorizontal = 0f;
    [SerializeField] private float velocidadMov;
    [Range(0, 0.3f)][SerializeField] private float suavisado;
    private Vector3 velocidad = Vector3.zero;
    private bool mirandoDerecha = false;

    [Header("Salto")]
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Vector3 dimensionesCaja;
    [SerializeField] private bool enSuelo;
    private bool salto = false;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal") * velocidadMov;

        if(Input.GetButtonDown("Jump"))
        {
            salto = true;
        }
    }

    private void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);
        // Mover
        Mover(moveHorizontal * Time.fixedDeltaTime, salto);

        salto = false;
    }

    private void Mover(float mover, bool saltar)
    {
        Vector3 velocidadObjetivo = new Vector2(mover, rb2D.velocity.y);
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, velocidadObjetivo, ref velocidad, suavisado);

        if(mover >0 && !mirandoDerecha)
        {
            // Girar
            Girar();
        } else if (mover < 0 && mirandoDerecha)
        {
            // Girar
            Girar();
        }
        
        if(saltar && enSuelo)
        {
            rb2D.AddForce(new Vector2(0f, fuerzaSalto));
        }
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
