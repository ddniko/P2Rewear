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
// Using the package (UnityEngine.UIElements;) overides the use of (UnityEngine.UI) package,
// making code throw the error saying that Image is not an UNITY component. Therefor dont use that.



public class Scroll : MonoBehaviour
{
    
    [Header ("Is it an internal test environment?")]
    public bool IsTest = false;

    [Header("Gameobjects")]
    public GameObject MSTidligereEjere;
    public GameObject MSStammePrefab;
    public GameObject MSMindeBtn;
    public GameObject MSScrollContent;
    public GameObject MSBaseStammePrefab;
    public GameObject MSEditAddBtn;

    [Header ("Other Variables")]
    public int tidligereEjer;
    public bool erEjer = false; // after testing this should be made false.
    public List<MMemory> memDBPublic;
    public List<MMemory> userCreatedMem;
    public bool haveCreated = false;

    private int ejerPerScroll = 4; // there is min 4 required to have the big tree we see here.
    private List<GameObject> treeTrunks = new List<GameObject>();
    private List<GameObject> Btns = new List<GameObject>();
    private float HeightDifference;
    private float cropB;
    private float cropT;
    MMemory tempMemory;
    public int clothingArticleID = 1;

    private int memMatchingID = 0;
    
    // Declare the dictionaries that will be passed by reference, these are semi temp for DB
    Dictionary<int, List<int>> clothingDB;
    List< MMemory> memDBList;

    BtnScript btnSc;
    //TextMeshProUGUI txtObj;

    public List<MMemory> memSortedByDate;

    Vector2[] btnPos = new Vector2[4] {
        new Vector2(250, 800), new Vector2(-250, 1010),
        new Vector2(250, 1300), new Vector2(-250, 1500)
    };


    public static Scroll instance;

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

    private void OnDisable()
    {
        if (IsTest == true)
        {
            memDBList = DBManager.GetAllMemories();
            foreach (MMemory mem in memDBList)
            {
                DBManager.DeleteMemory(mem.Id);
            }
            
        }
    }
    public void StartStamme(int clothingArticleID)
    {
        
        //DBManager.Init();
        if (DBManager.GetParentByArticleId(clothingArticleID).Id == LogIn.LoggedIn.Id )
        {
            erEjer = true;
        }else { erEjer = false; }

        // Read the data from the database using out parameters
        
        if (IsTest == true)
        {
            StammeManager.instance.ReadDBTest(clothingArticleID, out memDBList); // Make the memDB, memSortedByDate and props the public one to be the same.
        }
        else if (IsTest == false)
        {
            memDBList = DBManager.GetMemoriesByArticle(clothingArticleID);
        }


        memDBPublic = memDBList;
        
        // this whole section is about getting the apropriate amount of previous owners by the amount of memories present and...
        

        //the new count of previous owners
        //tidligereEjer = DBManager.GetMemoriesByArticle(clothingArticleID).Count; ___________
        
        // we sort the Outputted list by decending date (Old -> New)
        memSortedByDate = memDBList.OrderBy(tempMemory => DateTime.Parse(tempMemory.DateAdded)).ToList();
        if (userCreatedMem != null) // gør ikke noget?
        {
            if (memDBList.Any(memA => userCreatedMem.Any(memB => { if (memA == memB) { memMatchingID = memA.Id; return true; } return false; })))
            {
                haveCreated = true;

            }
        }

        if (erEjer == true) // hvis det er ejeren af tøjet atm, så bliver knappen edit/add btn vist, og hvis de har redigere på det før bliver knappen ændret fra opret til rediger minde
        {
            TextMeshProUGUI txtObj;
            MSEditAddBtn.gameObject.SetActive(true);

            if (haveCreated == true) //checks if any ID's matches the ones of the user's private list
            {
                txtObj = MSEditAddBtn.GetComponentInChildren<TextMeshProUGUI>();
                txtObj.text = "Rediger\nMinde";
            }
        }
        else
        {
            MSEditAddBtn.gameObject.SetActive(false);
        }

        tidligereEjer = DBManager.GetMemoriesByArticle(clothingArticleID).Count;
        // THIS ONLY HAPPENS IF THEY OWN THE CLOTHING
        // check if the current shown mems have been created by the user.
        // (we do that by getting the list of mem id, and compare it with the memid of the item that have been newly created (logged after the reload) maybe compare through the dates)
        //      if there have been created any by the user, the ADDMEMORY btn switches to a EDITMEMORY or REDIGERMINDE

        StammeManager.instance.AddTrunk();

    }

    public void OpenCreateOverlay()
    {
        MSBttnOverlayManager.instance.mindestammeOverlayEditOrCreateOwnMem.SetActive(true);
    }


}




