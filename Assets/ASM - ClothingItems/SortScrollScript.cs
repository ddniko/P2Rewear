using UnityEngine;

public class SortScrollScript : MonoBehaviour
{

    public GameObject[] itemsVisible = new GameObject[5];
    public GameObject[] itemsSpots = new GameObject[5];
    public GameObject content;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sortItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sortItems()
    {
        for (int i = 0; i < itemsVisible.Length; i++)
        {
            Instantiate(itemsVisible[i], itemsSpots[i].transform.position, Quaternion.identity, content.transform);
        }
        
        
    }
}
