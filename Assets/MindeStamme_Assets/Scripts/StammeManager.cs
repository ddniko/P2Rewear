using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using static Unity.Collections.Unicode;

public class StammeManager : MonoBehaviour
{
    [Header("Is it an internal test environment?")]
    public bool IsTest = false;

    [Header("Gameobjects")]
    public GameObject MSTidligereEjere;
    public GameObject MSStammePrefab;
    public GameObject MSMindeBtn;
    public GameObject MSScrollContent;
    public GameObject MSBaseStammePrefab;
    public GameObject MSEditAddBtn;
    public GameObject MSScrollObj;

    [Header("Other Variables")]
    public int tidligereEjer;
    public bool erEjer = false; // after testing this should be made false.
    public List<MMemory> memDBPublic;
    public List<MMemory> userCreatedMem;
    public bool haveCreated = false;

    private int ejerPerScroll = 4; // there is min 4 required to have the big tree we see here.
    private List<GameObject> treeTrunks = new List<GameObject>();
    private List<GameObject> Btns = new List<GameObject>();
    private float HeightDifference = 0;
    private float cropB;
    private float cropT;
    MMemory tempMemory;

    public int clothingArticleID = 0;
    private int memMatchingID = 0;

    // Declare the dictionaries that will be passed by reference, these are semi temp for DB
    Dictionary<int, List<int>> clothingDB;
    List<MMemory> memDBList;

    BtnScript btnSc;
    //TextMeshProUGUI txtObj;

    List<MMemory> memSortedByDate;

    Vector2[] btnPos = new Vector2[4] {
        new Vector2(250, 800), new Vector2(-250, 1010),
        new Vector2(250, 1300), new Vector2(-250, 1500)
    };


    public static StammeManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }

    public void StammeStartup(int ArticleID)
    {
        MSScrollObj.SetActive(true);
        this.clothingArticleID = ArticleID;
        //Scroll.instance.clothingArticleID = ArticleID;
        
        Scroll.instance.StartStamme(ArticleID);
        
    }

    public void AddTrunk()
    {
        foreach (GameObject trunk in treeTrunks)
        {
            Destroy(trunk);
        }
        treeTrunks.Clear();
        Btns.Clear();
        tidligereEjer = Scroll.instance.tidligereEjer;
        int ejerCount = tidligereEjer % ejerPerScroll; // the number of spialge of previous owners onto the next trunk / scroll 
        float pagescrolls = Mathf.Ceil(tidligereEjer / ejerPerScroll); // how many scrols is there needed to make to scroll to the top, we round up to a whole number because we can crop the rest later. -1 because the first page is free >:3

        // forloop instantiater alle obj's 
        for (int i = 0; i <= pagescrolls; i++)
        {
            GameObject Stamme = (GameObject)Instantiate(MSStammePrefab, MSScrollContent.transform, false);

            treeTrunks.Add(Stamme);
        }
        for (int i = 0; i < tidligereEjer; i++)
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

            trunkIndex++;
            Vector2 position = new Vector3(0, TVal);
            Trunk.GetComponent<RectTransform>().anchoredPosition = position;

            //Debug.Log("SVal = " + SVal + " TVal = " + TVal + " TrunkIndex = " + trunkIndex + " position = " + position);


        }



        int BtnIndex = 0;
        //Move all bottons(btn) to their respective places
        Debug.Log(Btns.Count + " How many items in Btns " + treeTrunks.Count + " How many items in treetrunks");
        foreach (GameObject btn in Btns) // make the txtObj in specific instances and not a global value
        {
            memSortedByDate = Scroll.instance.memSortedByDate;
            TextMeshProUGUI txtObj;
            int btnCycle = BtnIndex % btnPos.Length;
            //btnPos[btnCycle] += new Vector2(0, 1200 * Mathf.Floor(BtnIndex / btnPos.Length));
            Vector3 targetPos = btnPos[btnCycle] + new Vector2(0, 1200 * Mathf.Floor(BtnIndex / btnPos.Length)); ;

            btn.GetComponent<RectTransform>().anchoredPosition = targetPos;

            txtObj = btn.GetComponentInChildren<TextMeshProUGUI>();
            //txtObj.text = $"{memDBTest[MemID].DateAdded}";    //We take the date of the memory and set the button to it (just fetch the date from the correct memory from the DB)
            txtObj.text = $"{memSortedByDate[BtnIndex].DateAdded}";


            btn.GetComponent<BtnScript>().Indexbtn = BtnIndex;
            btn.GetComponent<BtnScript>().MemIDbtn = memSortedByDate[BtnIndex].Id;

            BtnIndex++;

            //Debug.Log("btnPos.Length = " + btnPos.Length + " btnPos[btnCycle] = " + btnPos[btnCycle] + " btnCycle = " + btnCycle + " targetPos = " + targetPos + " btnindex / btnpos length = " + Mathf.Floor(BtnIndex / btnPos.Length));

        }

        //if (erEjer == true) // hvis det er ejeren af tøjet atm, så bliver knappen edit/add btn vist, og hvis de har redigere på det før bliver knappen ændret fra opret til rediger minde
        //{
        //    TextMeshProUGUI txtObj;
        //    MSEditAddBtn.gameObject.SetActive(true);

        //    if (haveCreated == true) //checks if any ID's matches the ones of the user's private list
        //    {
        //        txtObj = MSEditAddBtn.GetComponentInChildren<TextMeshProUGUI>();
        //        txtObj.text = "Rediger\nMinde";
        //    }
        //}
        // Sets the amount of previous owners to a visable text obj on the page.
        TextMeshProUGUI txtObjEjerCount = MSTidligereEjere.GetComponent<TextMeshProUGUI>();
        txtObjEjerCount.text = $"{tidligereEjer}";

        // position of the last tree trunk! 747 is the hight that we want between the trunk and top of the content edge
        if (treeTrunks.Count <= 0)
        {
            HeightDifference = MSScrollContent.GetComponent<RectTransform>().sizeDelta.y;
        }
        else
        {
            HeightDifference = (treeTrunks.LastOrDefault().GetComponent<RectTransform>().sizeDelta.y / 2) + treeTrunks.LastOrDefault().GetComponent<RectTransform>().anchoredPosition.y + 747;

        }
        Debug.Log(HeightDifference);

        //we want to crop the last truck being made
        CropTrunk(ejerCount);

        // we want the content container to change its height with the content within i.e. the trunk.
        MSScrollContent.GetComponent<RectTransform>().sizeDelta = new Vector2(MSScrollContent.GetComponent<RectTransform>().anchoredPosition.x, HeightDifference);

        // offset the position
        MSScrollContent.GetComponent<RectTransform>().anchoredPosition = new Vector2(MSScrollContent.GetComponent<RectTransform>().anchoredPosition.x, HeightDifference);
        //        MSScrollContent.GetComponent<RectTransform>().anchoredPosition = new Vector2(MSScrollContent.GetComponent<RectTransform>().anchoredPosition.x, HeightDifference + MSScrollContent.GetComponent<RectTransform>().anchoredPosition.y);


    }


    public void CropTrunk(int count)
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
            HeightDifference -= MSStammePrefab.GetComponent<RectTransform>().sizeDelta.y * (1 - cropT);
        }
        else if (count == 3)
        {
            Crop(cropB = 0.80f, cropT = 0.80f);
            HeightDifference -= MSStammePrefab.GetComponent<RectTransform>().sizeDelta.y * (1 - cropT);

        }
    }
    public void Crop(float b, float a)
    {
        treeTrunks.LastOrDefault().GetComponent<Image>().fillAmount = a;
        treeTrunks.LastOrDefault().transform.GetChild(0).GetComponent<Image>().fillAmount = b;
    }



    public void ReadDBTest(int currArticleID, out List<MMemory> memListOutput) // Make the memListOutput used instead of memDBTest //this is begining to be reduntant / forældet - since this is already handled in the DBManager and MMemory
    {

        List<MMemory> memDBTest;


        memDBTest = new List<MMemory>
        {
            // Memories for Group 001 - Cozy Blue Jacket
             new MMemory { ArticleID = currArticleID, DateAdded = "2024-03-03", Title = "First Time in the Jacket", Description = "We put the cozy blue jacket on you for the first time. You looked so snug and curious." } ,
             new MMemory { ArticleID = currArticleID, DateAdded = "2024-03-01", Title = "Jacket at the Park", Description = "You wore the blue jacket on our walk to the park. You found a stick and refused to let go of it." } ,
             new MMemory { ArticleID = currArticleID, DateAdded = "2024-03-05", Title = "Falling Asleep in the Jacket", Description = "After a long day, you passed out in the jacket in your stroller. We didn’t want to take it off." } ,
            // Memories for Group 002 - Tiny Red Boots
             new MMemory { ArticleID = currArticleID, DateAdded = "2024-03-08", Title = "Boots in the Rain", Description = "You stomped through puddles in the tiny red boots. Splashing was your new favorite thing." } ,
             new MMemory { ArticleID = currArticleID, DateAdded = "2024-03-07", Title = "Wearing Boots Indoors", Description = "You refused to take the red boots off, even indoors. You wore them with pajamas." } ,
             new MMemory { ArticleID = currArticleID, DateAdded = "2024-03-09", Title = "Boots on the Beach", Description = "Not quite beachwear, but you wore the boots anyway. You filled them with sand." } ,
             new MMemory { ArticleID = currArticleID, DateAdded = "2024-03-10", Title = "Birthday Boots", Description = "Your birthday outfit wasn’t complete without the red boots. They were the star of every photo." } ,
             new MMemory { ArticleID = currArticleID, DateAdded = "2024-03-11", Title = "Boots in the Snow", Description = "We layered socks and squeezed your feet in. You marched proudly through the snow." } ,
             new MMemory { ArticleID = currArticleID , DateAdded = "2024-03-12", Title = "Zoo Day Boots", Description = "You wore the boots to the zoo. They got muddy near the goat pen but still held up." } ,
             new MMemory { ArticleID = currArticleID, DateAdded = "2024-03-13", Title = "Napping with the Boots", Description = "You fell asleep on the couch wearing the boots and a superhero cape." } ,
             new MMemory { ArticleID = currArticleID, DateAdded = "2024-03-14", Title = "Final Outing in the Boots", Description = "The last time you wore the red boots. We kept them even though they no longer fit." }

        };

        foreach (MMemory mem in memDBTest)
        {

            DBManager.AddMemory(mem);

        }

        memListOutput = DBManager.GetMemoriesByArticle(currArticleID);
        //tidligereEjer = DBManager.GetMemoriesByArticle(clothingArticleID).Count;

    }

}
