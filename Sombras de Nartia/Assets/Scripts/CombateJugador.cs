using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateJugador : MonoBehaviour
{
    [SerializeField] private float vida;
    private MovePlayer movePlayer;
    [SerializeField] private float tiempoPerdidaControl;
    private Animator animator;

    private void Start()
    {
        movePlayer = GetComponent<MovePlayer>();
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        vida -= damage;
    }

    public void TakeDamage(float damage, Vector2 posicion)
    {
        vida -= damage;
        animator.SetTrigger("3_Damaged");
        StartCoroutine(PerderControl());
        StartCoroutine(DesactivarColision());
        movePlayer.Rebote(posicion);
        if (vida <= 0)
        {
            StartCoroutine(Morir());
        }
    }

    private IEnumerator DesactivarColision()
    {
        Physics2D.IgnoreLayerCollision(7, 8, true);
        yield return new WaitForSeconds(tiempoPerdidaControl);
        Physics2D.IgnoreLayerCollision(7, 8, false);
    }

    private IEnumerator PerderControl()
    {
        movePlayer.sePuedeMover = false;
        yield return new WaitForSeconds(tiempoPerdidaControl);
        movePlayer.sePuedeMover = true;
    }

    private IEnumerator Morir()
    {
        movePlayer.sePuedeMover = false;
        animator.SetTrigger("4_Death");
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }
}
