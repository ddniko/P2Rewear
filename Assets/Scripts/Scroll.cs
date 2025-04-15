using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;


// this guy is the cuprit if code shows error in saying that Image is not an UNITY component
//using UnityEngine.UIElements;
using static Unity.Collections.Unicode;



public class Scroll : MonoBehaviour
{
    public int tidligereEjer;
    private int ejerPerScroll = 4; // there is min 4 required to have the big tree we see here.
    public GameObject MSStammePrefab;
    public GameObject MSMindeBtn;
    public GameObject MSScrollContent;
    public GameObject MSBaseStammePrefab;
    private List<GameObject> treeTrunks = new List<GameObject>();
    private List<GameObject> Btns = new List<GameObject>();
    private float HeightDifference;
    private float cropB;
    private float cropT;
    // some very temp values only used when there is no DB
    public int temp_Clothing_DB_1_or_2;
    private string temp_Clothing_DB_001_or_002;
    // Declare the dictionaries that will be passed by reference, these are semi temp for DB
    Dictionary<string, List<string>> clothingDB;
    Dictionary<string, Memory> memDB;
    public Dictionary<string, Memory> memDBPublic;

    BtnScript btnSc;
    TextMeshProUGUI txtObj;

    List<string> countList;

    Vector2[] btnPos = new Vector2[4] {
        new Vector2(250, 800), new Vector2(-250, 1010),
        new Vector2(250, 1300), new Vector2(-250, 1500)
    };

    private void OnEnable()
    {

        // Declare the dictionaries that will be passed by reference, these are semi temp for DB
        //Dictionary<string, List<string>> clothingDB;
        //Dictionary<string, Memory> memDB;

        // Read the data from the database using out parameters
        ReadDB(out clothingDB, out memDB);
        memDBPublic = memDB;
        if (temp_Clothing_DB_1_or_2 == 1)
        {
            temp_Clothing_DB_001_or_002 = "001";
        }
        else if (temp_Clothing_DB_1_or_2 == 2)
        {
            temp_Clothing_DB_001_or_002 = "002";
        }

        countList = clothingDB[temp_Clothing_DB_001_or_002];
        int count = countList.Count;
        tidligereEjer = count;
        //___________________________________________________\\

        AddTrunk();

    }

    void OutputMemories(Dictionary<string, Memory> memDB, int Index, List <string> memList)
    {
        
    }

    //void AddEveryInstance()
    //{
    //}

    void AddTrunk()
    {
        int ejerCount = tidligereEjer % ejerPerScroll; // the number of spialge of previous owners onto the next trunk / scroll 
        float pagescrolls = Mathf.Ceil(tidligereEjer / ejerPerScroll); // how many scrols is there needed to make to scroll to the top, we round up to a whole number because we can crop the rest later. -1 because the first page is free >:3

        // forloop instantiater alle obj's 
        for (int i = 1; i < pagescrolls; i++)
        {
            GameObject Stamme = (GameObject)Instantiate(MSStammePrefab, MSScrollContent.transform, false);
            
            treeTrunks.Add(Stamme);
        }
        for (int i = 0;i < tidligereEjer; i++)
        {
            GameObject MSBtn = (GameObject)Instantiate(MSMindeBtn, MSScrollContent.transform, false);

            Btns.Add(MSBtn);
        }


        // Move all trunks to their respective places
        int trunkIndex = 2;
        foreach (GameObject Trunk in treeTrunks)
        {
            float SVal = MSBaseStammePrefab.GetComponent<RectTransform>().anchoredPosition.y - Trunk.GetComponent<RectTransform>().sizeDelta.y;
            float TVal = (trunkIndex * Trunk.GetComponent<RectTransform>().sizeDelta.y + SVal);

            trunkIndex ++;
            Vector2 position = new Vector3(0, TVal);
            Trunk.GetComponent<RectTransform>().anchoredPosition = position;
            //Debug.Log("TVal is : " + TVal);
            //Debug.Log("Position is : " + position);
            //Debug.Log("And the Trunk is at : " + Trunk.GetComponent<RectTransform>().position);


        }



        int BtnIndex = 0;
        //Move all bottons(btn) to their respective places
        foreach (GameObject btn in Btns)
        {
            int btnCycle = BtnIndex % btnPos.Length;
            Vector3 targetPos = btnPos[btnCycle];
            btnPos[btnCycle] += new Vector2(0, 1200);

            btn.GetComponent<RectTransform>().anchoredPosition = targetPos;
            string MemID = countList[BtnIndex];


            txtObj = btn.GetComponentInChildren<TextMeshProUGUI>();
            txtObj.text = $"{memDB[MemID].DateAdded}";
            //btnSc = btn.AddComponent<BtnScript>();

            btn.GetComponent<BtnScript>().Indexbtn = BtnIndex; btn.GetComponent<BtnScript>().MemIDbtn = MemID;

            //btnSc.Indexbtn = BtnIndex; btnSc.MemIDbtn = MemID;

            BtnIndex++;

            //Output the memories to the console

            
        }

        // position of the last tree trunk! 747 is the hight that we want between the trunk and top of the content edge
        HeightDifference = (treeTrunks.LastOrDefault().GetComponent<RectTransform>().sizeDelta.y / 2) + treeTrunks.LastOrDefault().GetComponent<RectTransform>().anchoredPosition.y + 747;
        Debug.Log(HeightDifference);
        
        //we want to crop the last truck being made
        CropTrunk(ejerCount);

        // we want the content container to change its height with the content within i.e. the trunk.
        MSScrollContent.GetComponent<RectTransform>().sizeDelta = new Vector2(MSScrollContent.GetComponent<RectTransform>().anchoredPosition.x, HeightDifference);

        // offset the position
        MSScrollContent.GetComponent<RectTransform>().anchoredPosition = new Vector2(MSScrollContent.GetComponent<RectTransform>().anchoredPosition.x, HeightDifference);
        //        MSScrollContent.GetComponent<RectTransform>().anchoredPosition = new Vector2(MSScrollContent.GetComponent<RectTransform>().anchoredPosition.x, HeightDifference + MSScrollContent.GetComponent<RectTransform>().anchoredPosition.y);


    }


    void CropTrunk(int count)
    {
        //---Dependent on ejerCount---\\
        //CROP N.NN of branch and N.NN of Trunk!
        //ADD the rest of the Bottons!
        if (count == 0)
        {
            return;
        }
        else if (count == 1)
        {
            Crop(cropB = 0.20f, cropT = 0.40f);
            HeightDifference -= MSStammePrefab.GetComponent<RectTransform>().sizeDelta.y * (1 - cropT);
        }
        else if (count == 2)
        {
            Crop(cropB = 0.50f, cropT = 0.50f);
            HeightDifference -= MSStammePrefab.GetComponent<RectTransform>().sizeDelta.y * (1-cropT);
        }
        else if (count == 3)
        {
            Crop(cropB = 0.80f, cropT = 0.80f);
            HeightDifference -= MSStammePrefab.GetComponent<RectTransform>().sizeDelta.y * (1-cropT);
            
        }
    }
    void Crop(float b, float a)
    {
        treeTrunks.LastOrDefault().GetComponent<Image>().fillAmount = a;
        treeTrunks.LastOrDefault().transform.GetChild(0).GetComponent<Image>().fillAmount = b;
    }





    //________________________________________________________________________________________________________\\



    //This DB method was made by AI, it is simply just a placeholder for DB.
    // Method to read the database and use out parameters to modify dictionaries
    public static void ReadDB(out Dictionary<string, List<string>> clothingDB, out Dictionary<string, Memory> memDB)
    {
        // Initialize the dictionaries inside the method, from the database. There should be a method that finds the DB, there should be a method that "cuts" up a string into the list.
        clothingDB = new Dictionary<string, List<string>>
        {
            { "001", new List<string> { "101", "102", "103" } },
            { "002", new List<string> { "201", "202", "203", "204", "205", "206", "207", "208" } }
        };

        memDB = new Dictionary<string, Memory>
        {
            // Memories for Group 001 - Cozy Blue Jacket
            { "101", new Memory { Id = "101", DateAdded = "2024-03-01", Title = "First Time in the Jacket", Description = "We put the cozy blue jacket on you for the first time. You looked so snug and curious.", ImageFileName = "jacket_first.jpg" } },
            { "102", new Memory { Id = "102", DateAdded = "2024-03-03", Title = "Jacket at the Park", Description = "You wore the blue jacket on our walk to the park. You found a stick and refused to let go of it.", ImageFileName = "jacket_park.jpg" } },
            { "103", new Memory { Id = "103", DateAdded = "2024-03-05", Title = "Falling Asleep in the Jacket", Description = "After a long day, you passed out in the jacket in your stroller. We didn’t want to take it off.", ImageFileName = "jacket_stroller.jpg" } },

            // Memories for Group 002 - Tiny Red Boots
            { "201", new Memory { Id = "201", DateAdded = "2024-03-07", Title = "Boots in the Rain", Description = "You stomped through puddles in the tiny red boots. Splashing was your new favorite thing.", ImageFileName = "boots_rain.jpg" } },
            { "202", new Memory { Id = "202", DateAdded = "2024-03-08", Title = "Wearing Boots Indoors", Description = "You refused to take the red boots off, even indoors. You wore them with pajamas.", ImageFileName = "boots_pajamas.jpg" } },
            { "203", new Memory { Id = "203", DateAdded = "2024-03-09", Title = "Boots on the Beach", Description = "Not quite beachwear, but you wore the boots anyway. You filled them with sand.", ImageFileName = "boots_beach.jpg" } },
            { "204", new Memory { Id = "204", DateAdded = "2024-03-10", Title = "Birthday Boots", Description = "Your birthday outfit wasn’t complete without the red boots. They were the star of every photo.", ImageFileName = "boots_birthday.jpg" } },
            { "205", new Memory { Id = "205", DateAdded = "2024-03-11", Title = "Boots in the Snow", Description = "We layered socks and squeezed your feet in. You marched proudly through the snow.", ImageFileName = "boots_snow.jpg" } },
            { "206", new Memory { Id = "206", DateAdded = "2024-03-12", Title = "Zoo Day Boots", Description = "You wore the boots to the zoo. They got muddy near the goat pen but still held up.", ImageFileName = "boots_zoo.jpg" } },
            { "207", new Memory { Id = "207", DateAdded = "2024-03-13", Title = "Napping with the Boots", Description = "You fell asleep on the couch wearing the boots and a superhero cape.", ImageFileName = "boots_nap.jpg" } },
            { "208", new Memory { Id = "208", DateAdded = "2024-03-14", Title = "Final Outing in the Boots", Description = "The last time you wore the red boots. We kept them even though they no longer fit.", ImageFileName = "boots_final.jpg" } }

        };
    }



}

// en struct for memories, den har også verreided to string method
public class Memory
{
    public string Id { get; set; }
    
    public string Title { get; set; } /**/ public string DateAdded { get; set; } /**/ public string Description { get; set; } /**/ public string ImageFileName { get; set; }
    
    public override string ToString()
    {
        return $"{Id}: {Title} ({DateAdded})\n{Description}\nImage: {ImageFileName}\n";
    }
}


