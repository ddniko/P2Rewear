using UnityEngine;
using UnityEngine.UI;
using NativeCameraNamespace;
using static NativeCamera;
using System.IO;
public class CameraHandler : MonoBehaviour
{
    PreferredCamera pref = PreferredCamera.Front;
    public RawImage rawImage;
    string newPath;
    void Start()
    {
        newPath = $"{Application.persistentDataPath}/Pictures";

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenCam()
    {
        string s;
        TakePicture(OnPictureTaken, -1, true);
    }
    public void OnPictureTaken(string path)
    {
        Debug.Log($"Photo saved at: {path}");
        Texture2D texture = LoadTextureFromPath(path);
        File.Copy(path, newPath, true);

        rawImage.texture = texture;
        rawImage.rectTransform.sizeDelta = new Vector2(texture.width, texture.height);
    }

    private Texture2D LoadTextureFromPath(string filePath)
    { //ved ik helt hva fuck med den her endnu
        byte[] fileData = System.IO.File.ReadAllBytes(filePath);  // Read the file into bytes
        Texture2D texture = new Texture2D(2, 2);  // Initialize texture (size will be changed)
        texture.LoadImage(fileData);  // Load the image from bytes into the texture
        return texture;
    }
}

