using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlataforma : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private float distancia;
    [SerializeField] private bool moviendoDerecha;
    Animator animator;
    private Rigidbody2D rb2D;
    [SerializeField] private LayerMask queEsSuelo;
    public bool sePuedeMover = true;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (sePuedeMover)
        {
            Mover();
        } else
        {
            animator.SetBool("1_Move", false);
        }
    }

    private void Mover()
    {
        RaycastHit2D informacionSuelo = Physics2D.Raycast(controladorSuelo.position, Vector2.down, distancia, queEsSuelo);

        rb2D.velocity = new Vector2(velocidad, rb2D.velocity.y);

        // Activa la animación de movimiento solo si el enemigo realmente está moviéndose
        animator.SetBool("1_Move", Mathf.Abs(rb2D.velocity.x) > 0);

        if (!informacionSuelo)
        {
            // Girar si no hay suelo
            Girar();
        }
    }

    private void Girar()
    {
        moviendoDerecha = !moviendoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        velocidad *= -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorSuelo.transform.position, controladorSuelo.transform.position + Vector3.down * distancia);
    }
}
