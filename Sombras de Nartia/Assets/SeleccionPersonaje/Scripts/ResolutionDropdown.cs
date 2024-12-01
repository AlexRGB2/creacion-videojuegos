using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionDropdown : MonoBehaviour
{
    public Dropdown resolutionDropdown; // Referencia al Dropdown

    private Resolution[] resolutions; // Lista de resoluciones disponibles

    void Start()
    {
        // Obtener las resoluciones disponibles
        resolutions = Screen.resolutions;

        // Limpiar las opciones del Dropdown
        resolutionDropdown.ClearOptions();

        // Crear una lista para las resoluciones en formato de texto
        var options = new System.Collections.Generic.List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // Comprobar si esta es la resoluci�n actual
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // A�adir las opciones al Dropdown
        resolutionDropdown.AddOptions(options);

        // Seleccionar la resoluci�n actual
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Suscribirse al cambio de selecci�n
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }

    public void SetResolution(int index)
    {
        // Cambiar la resoluci�n seg�n la opci�n seleccionada
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
