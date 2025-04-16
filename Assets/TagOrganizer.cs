using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TagOrganizer : MonoBehaviour
{
    TMP_Dropdown TagDropdown;
    List<GameObject> Tags = new List<GameObject>();

    public int maxItemsPerRow = 5;
    public float horizontalSpacing = 2f;// de her bestemmer hvor meget rum der skal være imellem dem. kunne gøres til public fields så man kunne ændre dem i inspectoren
    public float verticalSpacing = 2f;
    public int currentRow = 0;
    public int currentColumn = 0;
    private Vector3 startPosition = new Vector3(0, 0, 0);
    public GameObject[] StartObjects;

    public void OnEnable()
    {
        startPosition = StartObjects[0].transform.position;
        horizontalSpacing = Mathf.Abs(StartObjects[0].transform.position.x) - Mathf.Abs(StartObjects[1].transform.position.x);
        verticalSpacing = Mathf.Abs(StartObjects[0].transform.position.y) - Mathf.Abs(StartObjects[2].transform.position.y);
        currentColumn = 0;
        currentRow = 0;
    }
    public void CreateTag()
    {

    }

    public void OrderArticles()
    {
        for (int i = 0; i < Tags.Count; i++)  //index for rækken
        {
            Tags[i].gameObject.transform.position = startPosition + new Vector3(currentColumn * horizontalSpacing, -currentRow * verticalSpacing, 0);

            currentColumn++;

            // hvis den her row er fyldt, så gå et hak ned
            if (currentColumn >= maxItemsPerRow)
            {
                currentColumn = 0;
                currentRow++;
            }
        }
    }

}
