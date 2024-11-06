using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateCaC : MonoBehaviour
{
    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float danoGolpe;
    private Animator animator;
    private DamageSistem damageSistem;
    [SerializeField] private float tiempoEntreAtaque;
    [SerializeField] private float tiempoSiguienteAtaque;

    private void Start()
    {
        animator = GetComponent<Animator>();
        damageSistem = GetComponent<DamageSistem>();
    }

    private void Update()
    {
        if(tiempoSiguienteAtaque > 0)
        {
            tiempoSiguienteAtaque -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire1") && tiempoSiguienteAtaque <= 0)
        {
            Golpe();
            tiempoSiguienteAtaque = tiempoEntreAtaque;
        }
    }

    private void Golpe()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);
        StartCoroutine(damageSistem.Ataque());

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemy"))
            {
                colisionador.transform.GetComponent<Enemy>().TakeDamage(danoGolpe);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }
}
