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

    private void OnEnable() 
    {
        mindeStamme = GameObject.FindGameObjectWithTag("MindeStamme");
    }


    // i have 3 fkn lists for some fkn reason.
    //      1. memDBTest - main list made to hold all of the id's of memories

    public void OnClick() //this is an on click method that is set-up in the editor of unity.
    {
        MMemory mem = Scroll.instance.memDBPublic[Indexbtn];
        MSBttnOverlayManager.instance.OpenUserMemOverlay(mem);
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
