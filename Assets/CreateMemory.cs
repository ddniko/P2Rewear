using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateMemory : MonoBehaviour
{
    public TextMeshProUGUI titleInput;
    public TextMeshProUGUI descriptionInput;
    public GameObject cameraHandlerObject;
    private CameraHandler cameraHandler;
    public RawImage memoryImage;

    public void Start()
    {
        cameraHandler = cameraHandlerObject.GetComponent<CameraHandler>();
    }

    public void createMemory(MArticle article)
    {
        
         new MMemory().CreateMemory(article.Id, titleInput.text, descriptionInput.text, ConvertImageToByteArray(memoryImage));
        
         
    }

    public void OpenCamera()
    {
        cameraHandler.OpenCam();
    }
    
    public byte[] ConvertImageToByteArray(RawImage rawImage)
    {

        if (rawImage.texture != null)
        {

            Texture2D texture2D = rawImage.texture as Texture2D;

            if (texture2D != null)
            {
                byte[] imageBytes = texture2D.EncodeToPNG(); 

                return imageBytes;
            }
            else
            {
                Debug.LogError("texturen er ikke 2d");
                return null;
            }
        }
        else
        {
            Debug.LogError("mangler en texture.");
            return null;
        }
    }
    
    
}
