using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
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

    Vector2[] btnPos = new Vector2[4] {
        new Vector2(250, 800), new Vector2(-250, 1010),
        new Vector2(250, 1300), new Vector2(-250, 1500)
    };

    //List<Vector3> btnPos = new List<Vector3>
    //{
    //new Vector3(0, 0, 0), new Vector3(1, 0, 0),
    //new Vector3(2, 0, 0), new Vector3(3, 0, 0)
    //};

    private void Start()
    {
        AddTrunk();
    }

    //void AddEveryInstance()
    //{
    //}

    void AddTrunk()
    {
        int ejerCount = tidligereEjer % ejerPerScroll; // the number of spialge of previous owners onto the next trunk / scroll 
        float pagescrolls = Mathf.Ceil(tidligereEjer / ejerPerScroll); // how many scrols is there needed to make to scroll to the top, we round up to a whole number because we can crop the rest later. -1 because the first page is free >:3

        // forloop instantiater alle obj's 
        for (int i = 0; i < pagescrolls; i++)
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
            //float TVal = (trunkIndex * ((MSBaseStammePrefab.GetComponent<RectTransform>().anchoredPosition.y +(MSStammePrefab.GetComponent<RectTransform>().sizeDelta.y))/2));

            //if (trunkIndex == 2)
            //{
            //    TVal = MSBaseStammePrefab.GetComponent<RectTransform>().anchoredPosition.y + MSStammePrefab.GetComponent<RectTransform>().sizeDelta.y;
            //}


            //-(1780.5f - (MSStammePrefab.GetComponent<RectTransform>().anchoredPosition.y + (MSStammePrefab.GetComponent<RectTransform>().sizeDelta.y/2)));

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
            //btnPos[btnCycle] += new Vector2(0,(trunkIndex * MSStammePrefab.GetComponent<RectTransform>().sizeDelta.y));
            //btnPos.SetValue(btnPos.,btnCycle);
            btnPos[btnCycle] += new Vector2(0, 1200);

            btn.GetComponent<RectTransform>().anchoredPosition = targetPos;
            //btn.transform.position = targetPos;


            BtnIndex++;



            //NN += 1;

            //    if (NN == 0)
            //    {
            //        //Do something
            //    }
            //    else if (NN == 1)
            //    {
            //        //Do something
            //    }
            //    else if (NN == 2)
            //    {
            //        //Do something
            //    }
            //    else if (NN == 3)
            //    {
            //        //Do something
            //    }
            //    else if (NN > 3)
            //    {
            //        NN = 0;
            //    }

            //float pagescrolls = Mathf.Ceil(tidligereEjer / ejerPerScroll);
            //float TVal = (N * Trunk.GetComponent<RectTransform>().sizeDelta.y);


            //Vector2 position = new Vector3(0, TVal);
            //Trunk.GetComponent<RectTransform>().anchoredPosition = position;
        }

        // position of the last tree trunk! 747 is the hight that we want between the trunk and top of the content edge
        HeightDifference = (treeTrunks.LastOrDefault().GetComponent<RectTransform>().sizeDelta.y / 2) + treeTrunks.LastOrDefault().GetComponent<RectTransform>().anchoredPosition.y + 747;
        Debug.Log(HeightDifference);
        
        //we want to crop the last truck being made
        CropTrunk(ejerCount);

        // we want the content container to change its height with the content within i.e. the trunk.
        MSScrollContent.GetComponent<RectTransform>().sizeDelta = new Vector2(MSScrollContent.GetComponent<RectTransform>().anchoredPosition.x, HeightDifference);

        // offset the position
        MSScrollContent.GetComponent<RectTransform>().anchoredPosition = new Vector2(MSScrollContent.GetComponent<RectTransform>().anchoredPosition.x, HeightDifference + MSScrollContent.GetComponent<RectTransform>().anchoredPosition.y);


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
    
}
