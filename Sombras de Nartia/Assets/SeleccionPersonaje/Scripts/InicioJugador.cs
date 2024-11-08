using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InicioJugador : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        // Obt�n el �ndice del jugador desde PlayerPrefs
        int indexJugador = PlayerPrefs.GetInt("JugadorIndex");

        // Instancia el personaje en la posici�n del objeto InicioJugador
        GameObject jugadorInstanciado = Instantiate(
            GameManager.Instance.players[indexJugador].player,
            transform.position,
            Quaternion.identity
        );

        // Asigna el personaje instanciado como el objetivo de la Cinemachine Virtual Camera
        virtualCamera.Follow = jugadorInstanciado.transform.GetChild(0).gameObject.transform;
    }

}
