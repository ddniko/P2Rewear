using TMPro;
using UnityEngine;

public class SetupMessage : MonoBehaviour
{
    public GameObject messageText;
    public GameObject back;
    
    
    
    public void SetupMsg(string message)
    {
        TextMeshPro text = messageText.GetComponent<TextMeshPro>();
        text.text = message;
    }
    
}
