using UnityEngine;
using System.IO;

public class PhotoCapture : MonoBehaviour
{
    public Camera photoCamera;
    public RenderTexture renderTexture;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // Press 'P' to take photo
        {
            TakePhoto();
        }
    }

    void TakePhoto()
    {
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = renderTexture;

        photoCamera.Render();

        Texture2D photo = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        photo.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        photo.Apply();

        RenderTexture.active = currentRT;

        // Optional: save to disk (Editor or PC only)
        byte[] bytes = photo.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/Photo_" + Time.time + ".png", bytes);

        Debug.Log("Photo taken!");
    }
}
