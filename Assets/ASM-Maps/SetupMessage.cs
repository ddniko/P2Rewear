using TMPro;
using UnityEngine;

public class SetupMessage : MonoBehaviour
{
    public GameObject messageText;
    public GameObject back;
    
    
    
    public void SetupMsg(string message)
    {
        TextMeshProUGUI text = messageText.GetComponent<TextMeshProUGUI>();
        text.text = message;
        
    }
    
}
