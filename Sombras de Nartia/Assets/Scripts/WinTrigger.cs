using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    [SerializeField] private GameObject pantallaGanador;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Objeto tocado: {other.name}"); // Para depuración

        // Verifica si el jugador toca el objeto
        if (other.CompareTag("Player"))
        {
            // Carga la pantalla de ganar
            Time.timeScale = 0f;
            pantallaGanador.SetActive(true);
        }
    }
}
