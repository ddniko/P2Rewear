using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TagOrganizer : MonoBehaviour
{
    public TMP_Dropdown TagDropdown;
    List<GameObject> Tags = new List<GameObject>();
    public GameObject Tag;
    public List<string> tagValues = new List<string>();

    public int maxItemsPerRow = 5;
    public float horizontalSpacing = 2f;// de her bestemmer hvor meget rum der skal v�re imellem dem. kunne g�res til public fields s� man kunne �ndre dem i inspectoren
    public float verticalSpacing = 2f;
    public int currentRow = 0;
    public int currentColumn = 0;
    private Vector3 startPosition = new Vector3(0, 0, 0);
    public GameObject[] StartObjects;

    public void RemoveTagByButton(GameObject tag)
    {
        if (Tags.Contains(tag))
        {
            Tags.Remove(tag);
        }
        string tagstring = tagValues.Find(a => a == tag.GetComponentInChildren<TextMeshProUGUI>().text);
        Destroy(tag);
        tagValues.Remove(tagstring);
        OrderArticles();
    }
    public void ClearTags()
    {
        for (int i = 0; i < Tags.Count; i++)
        {
            Destroy(Tags[i]);
        }
        tagValues.Clear();

    }

    public void OnEnable()
    {
        startPosition = StartObjects[0].transform.position;
        horizontalSpacing = Mathf.Abs(StartObjects[1].transform.position.x - StartObjects[0].transform.position.x);
        verticalSpacing = Mathf.Abs(StartObjects[2].transform.position.y - StartObjects[0].transform.position.y);

        currentColumn = 0;
        currentRow = 0;
    }
    public void Start()
    {
        startPosition = StartObjects[0].transform.position;
        horizontalSpacing = Mathf.Abs(StartObjects[1].transform.position.x - StartObjects[0].transform.position.x);
        verticalSpacing = Mathf.Abs(StartObjects[2].transform.position.y - StartObjects[0].transform.position.y);

        currentColumn = 0;
        currentRow = 0;
    }
    public void CreateTag()
    {
        string selectedValue = TagDropdown.options[TagDropdown.value].text;
        if (tagValues.Contains(selectedValue))
            return;

        tagValues.Add(selectedValue);
        var go = Instantiate(Tag, transform);
        go.GetComponentInChildren<TextMeshProUGUI>().text = selectedValue;
        Button btn = go.GetComponentInChildren<Button>();
        btn.onClick.AddListener(() =>
        {
            RemoveTagByButton(go);
        });
        Tags.Add(go);
        OrderArticles();
    }

    public void OrderArticles()
    {
        currentColumn = 0;
        currentRow = 0;
        for (int i = 0; i < Tags.Count; i++)  //index for r�kken
        {
            Tags[i].gameObject.transform.position = startPosition + new Vector3(currentColumn * horizontalSpacing, -currentRow * verticalSpacing, 0);

            currentColumn++;

            // hvis den her row er fyldt, s� g� et hak ned
            if (currentColumn >= maxItemsPerRow)
            {
                currentColumn = 0;
                currentRow++;
            }
        }
    }

}
