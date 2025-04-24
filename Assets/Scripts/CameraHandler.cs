using UnityEngine;
using UnityEngine.UI;
using NativeCameraNamespace;
using static NativeCamera;
using System.IO;
using UnityEngine.Windows;
public class CameraHandler : MonoBehaviour
{
    PreferredCamera pref = PreferredCamera.Front;
    public RawImage rawImage;
    string newPath;
    //s
    void Start()
    {
        newPath = $"{Application.persistentDataPath}/Pictures/picture.JPEG";
        string directory = Path.GetDirectoryName(newPath);
        if (!System.IO.Directory.Exists(directory))
        {
            System.IO.Directory.CreateDirectory(directory);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenCam()
    {
        
        TakePicture(OnPictureTaken, -1, true);
    }
    public void OnPictureTaken(string path)
    {
        Debug.Log($"Photo saved at: {path}");
        Texture2D texture = LoadTextureFromPath(path);
        System.IO.File.Copy(path, newPath, true);
        
        float aspectRatio = (float)rawImage.texture.width / (float)rawImage.texture.height;

        
        rawImage.texture = texture;

       

        rawImage.rectTransform.sizeDelta = new Vector2(228.11f, 238.87f);
    }

    private Texture2D LoadTextureFromPath(string filePath)
    { //ved ik helt hva fuck med den her endnu
        byte[] fileData = System.IO.File.ReadAllBytes(filePath);  // Read the file into bytes
        Texture2D texture = new Texture2D(2, 2);  // Initialize texture (size will be changed)
        texture.LoadImage(fileData);  // Load the image from bytes into the texture
        return texture;
    }
}

