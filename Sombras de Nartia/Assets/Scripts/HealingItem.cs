using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : MonoBehaviour
{
    [SerializeField] private float healingAmount = 1f; // Cantidad de vida que cura

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Verifica que el jugador toque el objeto
        {
            DamageSistem playerDamageSystem = collision.GetComponent<DamageSistem>();
            if (playerDamageSystem != null)
            {
                playerDamageSystem.Heal(healingAmount); // Cura al jugador
            }
            Destroy(gameObject); // Elimina el objeto de curación
        }
    }
}
