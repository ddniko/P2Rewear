using System.Collections.Generic;
using UnityEngine;

public class ContactPageManager : MonoBehaviour
{
    private List<GameObject> sellerContacts = new List<GameObject>();
    private List<GameObject> buyerContacts = new List<GameObject>();
    
    [SerializeField] private GameObject buyerContactPrefab;
    [SerializeField] private GameObject sellerContactPrefab;

    public void SortBySellerContact(List<GameObject> contacts)
    {
        for (int i = 0; i < contacts.Count; i++)
        {
            Instantiate(contacts[i], contacts[i].transform.position, Quaternion.identity);
        }
    }
    
    public void SortByBuyerContact(List<GameObject> contacts)
    {
        
    }
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
