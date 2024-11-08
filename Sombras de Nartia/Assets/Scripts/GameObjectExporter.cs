using UnityEngine;
using System.IO;

public class GameObjectExporter : MonoBehaviour
{
    public Camera captureCamera; // Cámara configurada para capturar el objeto
    public RenderTexture renderTexture; // RenderTexture asignada a la cámara
    public string fileName = "ExportedGameObject.png";

    [ContextMenu("Export GameObject as PNG")]
    public void ExportAsPNG()
    {
        // Establecer la RenderTexture activa
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = renderTexture;

        // Crear una textura 2D del tamaño de la RenderTexture
        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();

        // Guardar la textura como PNG
        byte[] pngData = texture.EncodeToPNG();
        string filePath = Path.Combine(Application.dataPath, fileName);
        File.WriteAllBytes(filePath, pngData);

        // Restaurar el RenderTexture activo anterior
        RenderTexture.active = currentRT;

        // Limpieza
        Destroy(texture);

        Debug.Log($"Imagen exportada a: {filePath}");
    }
}
