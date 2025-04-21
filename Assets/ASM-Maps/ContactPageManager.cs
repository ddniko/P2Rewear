using System.Collections.Generic;
using UnityEngine;

public class ContactPageManager : MonoBehaviour
{
    private static List<MArticle> sellerItems = new List<MArticle>();
    private static List<MArticle> buyerItems = new List<MArticle>();
    private static List<GameObject> visibleContacts = new List<GameObject>();
    
    
    public Transform spawnPoint;
    public Transform spawnPosition;
    [SerializeField] float setOffset = 0f;
    private float offset = 0f;
    [SerializeField] private GameObject content;
    
    [SerializeField] private GameObject buyerContactPrefab;
    [SerializeField] private GameObject sellerContactPrefab;
    
    RectTransform rectTransform;
    public float contentHeight = 0f;

    public void Start()
    {
        rectTransform = content.GetComponent<RectTransform>();
    }

    public static void AddSellerItem(MArticle item)
    {
        bool found = false;
        if (sellerItems.Count > 0)
        {
            for (int i = 0; i < sellerItems.Count; i++)
            {
                if (sellerItems[i].Id == item.Id)
                {
                    found = true;
                }

            }
        }
        if(!found)
        {
            sellerItems.Add(item);
        }
    }

    public static void AddBuyerItem(MArticle item)
    {
        buyerItems.Add(item);
    }
    
    public void SortBySellerContact()
    {
        SortContacts(sellerContactPrefab, sellerItems);
    }
    
    public void SortByBuyerContact()
    {
        SortContacts(buyerContactPrefab, buyerItems);
    }

    private void SortContacts(GameObject prefab, List<MArticle> contacts)
    {
        ClearContacts();
        offset = 0f;
        
        for (int i = 0; i < contacts.Count; i++)
        {
            GameObject contactPage = Instantiate(prefab, spawnPoint.position + new Vector3(0, -offset, 0), prefab.transform.rotation, content.transform);
            ContactButtonScript contactScript = contactPage.GetComponent<ContactButtonScript>();
            contactScript.Article = contacts[i];
            contactScript.SetupClothingItem(contacts[i]);
            visibleContacts.Add(contactPage);
            offset += setOffset;
        }
        
        rectTransform.sizeDelta = rectTransform.sizeDelta + new Vector2(0,contentHeight * contacts.Count);
    }

    private void ClearContacts()
    {
        for (int i = 0; i < visibleContacts.Count; i++)
        {
            Destroy(visibleContacts[i]);
        }
    }
    
    
}
