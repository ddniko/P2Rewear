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
    public int Indexbtn;
    public string MemIDbtn;

    [Header("Obj References")]
    public GameObject mindeStamme;


    private Scroll scrollSc;
    

    private void OnEnable() 
    {
        mindeStamme = GameObject.FindGameObjectWithTag("MindeStamme");
        
        BtnUpdate(Indexbtn, MemIDbtn);
    }

    public void BtnUpdate(int index, string memID)
    {
        Console.WriteLine($"Index of {index} and ID {memID}");
        
        //treeTrunks.LastOrDefault().transform.GetChild(0).GetComponent<Image>().fillAmount = b;
        
    }

    public void OnClick()
    {

        scrollSc = mindeStamme.GetComponentInChildren<Scroll>();
        Memory mem = scrollSc.memDBPublic[$"{MemIDbtn}"];
        //Console.Write(mem.ToString());
        Debug.Log($"{mem.ToString()}");
    }

    
}
