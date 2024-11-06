using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageSistem : MonoBehaviour
{
    [SerializeField] private float vida;
    private MovePlayer movePlayer;
    [SerializeField] private float tiempoPerdidaControl;
    private Animator animator;
    [SerializeField] private GameObject deathScreen;

    [SerializeField] private int numOfHearts;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    private void Update()
    {

        if(vida > numOfHearts)
        {
            vida = numOfHearts;
        }

        for(int i = 0; i < hearts.Length; i++)
        {
            if (i < vida)
            {
                hearts[i].sprite = fullHeart;
            } else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            } else
            {
                hearts[i].enabled = false;
            }
        }

    }

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
        deathScreen.SetActive(true);
    }

    public IEnumerator Ataque()
    {
        movePlayer.sePuedeMover = false;
        animator.SetTrigger("2_Attack");
        yield return new WaitForSeconds(0.6f);
        movePlayer.sePuedeMover = true;
    }
}
