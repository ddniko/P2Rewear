using System.Collections.Generic;
using UnityEngine;

public class ContactPageManager : MonoBehaviour
{
    private List<ClothingItem> sellerItems = new List<ClothingItem>();
    private List<ClothingItem> buyerItems = new List<ClothingItem>();
    private List<GameObject> visibleContacts = new List<GameObject>();
    
    
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

    public void AddSellerItem(ClothingItem item)
    {
        sellerItems.Add(item);
    }

    public void AddBuyerItem(ClothingItem item)
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

    private void SortContacts(GameObject prefab, List<ClothingItem> contacts)
    {
        ClearContacts();
        offset = 0f;
        
        for (int i = 0; i < contacts.Count; i++)
        {
            GameObject contactPage = Instantiate(prefab, spawnPoint.position + new Vector3(0, -offset, 0), prefab.transform.rotation, content.transform);
            ContactButtonScript contactScript = contactPage.GetComponent<ContactButtonScript>();
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
