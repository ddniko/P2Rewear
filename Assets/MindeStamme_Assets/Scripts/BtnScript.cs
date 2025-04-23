using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class BtnScript : MonoBehaviour
{
    [Header ("Public variables")]
    [SerializeField]
    public int Indexbtn; //not using
    public int MemIDbtn;
    //public List<MMemory> userMem;
    public bool haveCreatedbtn = false;

    [Header("Obj References")]
    public GameObject mindeStamme;

    private Scroll scrollSc;
    private ReloadMS reloadMSSc;
    
    //public GameObject MSBttnOverlayManagerObject;
    private MSBttnOverlayManager manager;
    
    //[Header("PAGES")]
    //[SerializeField] private GameObject mindestamme_Main_PAGE;
    //[SerializeField] private GameObject mindeskov_Main_PAGE;


    public void Start()
    {
        manager = FindObjectOfType<MSBttnOverlayManager>();
    }

    private void OnEnable() 
    {
        mindeStamme = GameObject.FindGameObjectWithTag("MindeStamme");
        
        //BtnUpdate(Indexbtn, MemIDbtn);
    }

    //public void BtnUpdate(int index, int memID)
    //{
    //    Console.WriteLine($"Index of {index} and ID {memID}");
        
    //    //treeTrunks.LastOrDefault().transform.GetChild(0).GetComponent<Image>().fillAmount = b;
        
    //}


    // i have 3 fkn lists for some fkn reason.
    //      1. memDBTest - main list made to hold all of the id's of memories

    public void OnClick() //this is an on click method that is set-up in the editor of unity.
    {

        scrollSc = mindeStamme.GetComponentInChildren<Scroll>();
        MMemory mem = scrollSc.memDBPublic[MemIDbtn];
        //Console.Write(mem.ToString());

        Debug.Log($"This is information of memory ({mem.Id}) connected to this article ({mem.ArticleID}) : \n{mem.Title} ({mem.DateAdded})\n{mem.Description}\nImage: {mem.ImageData}\n");
        manager.OpenUserMemOverlay(mem);

        //Debug.Log($"{mem.ToString()}");



        //public override string ToString()
        //{
        //    return $" This is information of memory ({Id}) connected to this article ({ArticleID}) : \n{Title} ({DateAdded})\n{Description}\nImage: {ImageData}\n";
        //}

        // sends info to overlay that have been enabled in this method.

    }

    public void OnEditAddMemClick() // this
    {
        scrollSc = mindeStamme.GetComponentInChildren<Scroll>();
        manager.CreateMemOverlay(DBManager.GetArticleById(scrollSc.clothingArticleID));
        // Activate edit / add mem overlay
    }

    public void OnEditAddMemSaveClick() // this is on the btn called (GEM) that saves a new mem.
    {


        if (haveCreatedbtn == true) // edit memory
        {
            MMemory tempUserMem = new MMemory();

            // add logic here dependent on what the user writes in textfields and get the date created
            // and assign it to the apropriate clothing id (do this by checking what clothing id you are on or go check on another memory)


            DBManager.UpdateMemory(tempUserMem); // adds the new memory to DB
            reloadMSSc = mindeStamme.GetComponent<ReloadMS>(); reloadMSSc.ReloadMSPage();
        }
        else // make new memory
        {
            MMemory tempUserMem = new MMemory();

            // add logic here dependent on what the user writes in textfields and get the date created
            // and assign it to the apropriate clothing id (do this by checking what clothing id you are on or go check on another memory)


            DBManager.AddMemory(tempUserMem); // adds the new memory to DB
            reloadMSSc = mindeStamme.GetComponent<ReloadMS>(); reloadMSSc.ReloadMSPage();


            //haveCreatedbtn = scrollSc.haveCreated;
            //mindeStamme.transform.parent?.parent?.gameObject.GetComponent<ReloadMS>();

            // Make the memtree reload with the new datapoint.
            // Disable this overlay, and the previous 

            // Reloads the MS page
        }

        

    }
    private void OnDisable()
    {
        Destroy(gameObject);
    }
}
