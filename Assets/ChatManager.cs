using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    private List<GameObject> sendMessages = new List<GameObject>();
    public GameObject content;

    private Transform contentOrigin;
    
    
    public TextMeshProUGUI text;
    
    
    public GameObject userMessagePrefab;
    public Transform userMessageOrigin;
    public GameObject buyerMessagePrefab;
    public Transform buyerMessageOrigin;

    public float offset = 0f;
    
    
    [SerializeField] SetupMessage setupMessage;
    private int currentMessage = 0;


    public TextMeshProUGUI confirmText;
   
  
    public string[] replyMessages;
    void Start()
    {
        contentOrigin = content.transform;
    }
    

    public void SendChatMessage()
    {
        content.transform.position = contentOrigin.position;
        moveMessages();
        GameObject messageSent = Instantiate(userMessagePrefab, userMessageOrigin.position, userMessagePrefab.transform.rotation, contentOrigin);
        Debug.Log("The send text is: " + text.text);
        setupMessage = messageSent.GetComponent<SetupMessage>();
        setupMessage.SetupMsg(text.text);
        sendMessages.Add(messageSent);
        SendReplyMessage(); 
        
    }

    private void moveMessages()
    {
        foreach (GameObject msg in sendMessages)
        {
            Debug.Log("MessageMoved");
            msg.transform.position = msg.transform.position + new Vector3(0f, offset, 0f);
        }
    }

    public void SendReplyMessage()
    {
        moveMessages();
        GameObject messageSent = Instantiate(buyerMessagePrefab, buyerMessageOrigin.position, buyerMessagePrefab.transform.rotation, contentOrigin);
        setupMessage = messageSent.GetComponent<SetupMessage>();
        if (currentMessage < replyMessages.Length)
        {
            setupMessage.SetupMsg(replyMessages[currentMessage]);
            currentMessage++;
        }
        else
        {
            currentMessage = 0;
            setupMessage.SetupMsg(replyMessages[currentMessage]);
            currentMessage++;
        }
        sendMessages.Add(messageSent);
        
    }

    public void ConfirmDeal()
    {
        confirmText.text = "Du har bekræftet handlen (1/2)";
        StartCoroutine(DelayedConfirmation());
    }

    IEnumerator DelayedConfirmation()
    {
        yield return new WaitForSeconds(2f);
        confirmText.text = "Begge parter har acceptereret handlen (2/2)";
    }
}
