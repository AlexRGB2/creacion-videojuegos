using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{ 
    [SerializeField] private float vida;
    private Animator animator;
    private MovimientoPlataforma movimientoPlataforma;
    [SerializeField] private float tiempoPerdidaControl;
    private Rigidbody2D rb2D;
    [SerializeField] private int damage;

    private bool haRecibidoGolpe = false;
    [SerializeField] private int puntos = 100; // Ajusta la cantidad de puntos por enemigo

    private void Start()
    {
        animator = GetComponent<Animator>();
        movimientoPlataforma = GetComponent<MovimientoPlataforma>();
        rb2D = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(7, 8, false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<DamageSistem>().TakeDamage(damage, other.GetContact(0).normal);
        }
    }

    public void TakeDamage(float damage)
    {
        if (haRecibidoGolpe) return; // Evita recibir daño varias veces en un mismo ataque

        haRecibidoGolpe = true;
        vida -= damage;
        StartCoroutine(PerderControl());

        if (vida <= 0)
        {
            StartCoroutine(Morir());
        }
        else
        {
            StartCoroutine(ReiniciarInvulnerabilidad());
        }
    }

    private IEnumerator ReiniciarInvulnerabilidad()
    {
        yield return new WaitForSeconds(tiempoPerdidaControl);
        haRecibidoGolpe = false;
    }

    private IEnumerator Morir()
    {
        Physics2D.IgnoreLayerCollision(7, 8, true);
        animator.SetTrigger("4_Death");
        // Añadir puntos antes de destruir el objeto
        ScoreManager.instance.AddScore(puntos);

        yield return new WaitForSeconds(0.6f);
        Physics2D.IgnoreLayerCollision(7, 8, false);
        Destroy(gameObject);
    }

    private IEnumerator PerderControl()
    {
        movimientoPlataforma.sePuedeMover = false;
        animator.SetTrigger("3_Damaged");
        yield return new WaitForSeconds(tiempoPerdidaControl);
        movimientoPlataforma.sePuedeMover = true;
    }
}
