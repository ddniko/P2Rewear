using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    public TextMeshProUGUI sellerName;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI distanceText;
    
    public GameObject confirmationPanel;
    
    
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
    public TextMeshProUGUI[] recommendedMessages;
    void Start()
    {
        contentOrigin = content.transform;
    }

    public void SetupChat(ClothingItem clothingItem)
    {
        itemName.text = clothingItem.name;
        priceText.text = clothingItem.prize.ToString();
        //distanceText.text = clothingItem.distance.ToString();   Skal have distancen
    }
    
    public void SendRecommendedMessage(int messageId)
    {
        content.transform.position = contentOrigin.position;
        moveMessages();
        GameObject messageSent = Instantiate(userMessagePrefab, userMessageOrigin.position, userMessagePrefab.transform.rotation, contentOrigin);
        setupMessage = messageSent.GetComponent<SetupMessage>();
        setupMessage.SetupMsg(recommendedMessages[messageId].text);
        sendMessages.Add(messageSent);
        SendReplyMessage(); 
    }

    public void SendChatMessage()
    {
        content.transform.position = contentOrigin.position;
        moveMessages();
        GameObject messageSent = Instantiate(userMessagePrefab, userMessageOrigin.position, userMessagePrefab.transform.rotation, contentOrigin);
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
        yield return new WaitForSeconds(2f);
        confirmationPanel.SetActive(true);
    }
}
