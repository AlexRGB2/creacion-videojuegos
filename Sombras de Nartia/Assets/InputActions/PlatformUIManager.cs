using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformUIManager : MonoBehaviour
{
    public GameObject mobileUI; // Interfaz para dispositivos móviles

    void Start()
    {
        // Verificar la plataforma y activar/desactivar las interfaces
        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
        {
            mobileUI.SetActive(true);
        }
        else
        {
            mobileUI.SetActive(false);
        }
    }
}
