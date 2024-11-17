using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlataforma : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Transform controladorPared;
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
        }
        else
        {
            animator.SetBool("1_Move", false);
        }
    }

    private void Mover()
    {
        // Detectar suelo
        RaycastHit2D informacionSuelo = Physics2D.Raycast(controladorSuelo.position, Vector2.down, distancia, queEsSuelo);
        // Detectar paredes
        RaycastHit2D informacionPared = Physics2D.Raycast(controladorPared.position, moviendoDerecha ? Vector2.right : Vector2.left, distancia, queEsSuelo);

        rb2D.velocity = new Vector2(velocidad, rb2D.velocity.y);

        // Activa la animación de movimiento solo si el enemigo realmente está moviéndose
        animator.SetBool("1_Move", Mathf.Abs(rb2D.velocity.x) > 0);

        // Cambiar de dirección si no hay suelo o si hay una pared
        if (!informacionSuelo || informacionPared)
        {
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
        // Gizmo para el Raycast del suelo
        Gizmos.DrawLine(controladorSuelo.transform.position, controladorSuelo.transform.position + Vector3.down * distancia);
        // Gizmo para el Raycast de la pared
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(controladorPared.transform.position, controladorPared.transform.position + (moviendoDerecha ? Vector3.right : Vector3.left) * distancia);
    }
}
