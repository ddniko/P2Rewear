using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class Scroll : MonoBehaviour
{
    public int tidligereEjer;
    private int ejerPerScroll;
    public GameObject MSStammePrefab;
    public GameObject MSMindeBtn;
    public GameObject MSScrollContent;
    List<GameObject> treeTrunks = new List<GameObject>();
    float HeightDifference;
    public float cropB;
    public float cropT;

    private void Start()
    {
        AddTrunk();
    }


    void AddTrunk()
    {
        ejerPerScroll = 4; // amount of people per page (roughly)
        int ejerCount = tidligereEjer % ejerPerScroll; // the number of spialge of previous owners onto the next trunk / scroll 
        float pagescrolls = Mathf.Ceil(tidligereEjer / ejerPerScroll); // how many scrols is there needed to make to scroll to the top, we round up to a whole number because we can crop the rest later. -1 because the first page is free >:3
        
        //float TVal;

        for (int i = 0; i < pagescrolls; i++)
        {
            // Tval is the amount of units that the new trunk will do
            
            //Quaternion rotation = Quaternion.Euler(0, 0, 0);

            //instantiate the prefab so it is a child of content, and offset in the y coordinate
            //Instantiate(MSStammePrefab, position, rotation, MSScrollContent.transform);
            //Instantiate(MSStammePrefab);
            //MSStammePrefab.transform.parent = MSScrollContent.transform;
            //MSStammePrefab.GetComponent<RectTransform>().position = new Vector2(MSStammePrefab.GetComponent<RectTransform>().position.x, TVal);

            
            

            GameObject Stamme = (GameObject)Instantiate(MSStammePrefab, MSScrollContent.transform, false);


            //MSStammePrefab.transform.SetParent(MSScrollContent.transform, false);
            //float TVal = (N * (MSStammePrefab.GetComponent<RectTransform>().sizeDelta.y / 2)) + 450;
           
            

            treeTrunks.Add(Stamme);
            //treeTrunks.LastOrDefault();

            //Makes the GameObject "newParent" the parent of the GameObject "player".
            //player.transform.parent = newParent.transform;

            //MSScrollContent.transform.localPosition.y = TVal; MSScrollContent.transform.localScale.y = TVal;


            //GameObject NewObj = Instantiate(MSStammePrefab);
            //NewObj.transform.parent = MSScrollContent.transform;
            //NewObj.transform.localPosition = (i * NewObj.GetComponent<RectTransform>().sizeDelta.y);
        }

        int N = 2;
        foreach (GameObject Trunk in treeTrunks)
        {
            float TVal = (N * Trunk.GetComponent<RectTransform>().sizeDelta.y);
                //-(1780.5f - (MSStammePrefab.GetComponent<RectTransform>().anchoredPosition.y + (MSStammePrefab.GetComponent<RectTransform>().sizeDelta.y/2)));
            
            //Vector2 position = new Vector3(MSStammePrefab.GetComponent<RectTransform>().position.x, TVal);
            
            N += 1;
            Vector2 position = new Vector3(0, TVal);
            Trunk.GetComponent<RectTransform>().anchoredPosition = position;
            //Debug.Log("TVal is : " + TVal);
            //Debug.Log("Position is : " + position);
            //Debug.Log("And the Trunk is at : " + Trunk.GetComponent<RectTransform>().position);


        }

        

        // position of the last tree trunk! 747 is the hight that we want between the trunk and top of the content edge
        HeightDifference = (treeTrunks.Last().GetComponent<RectTransform>().sizeDelta.y / 2) + treeTrunks.Last().GetComponent<RectTransform>().anchoredPosition.y + 747;
        Debug.Log(HeightDifference);
        
        //we want to crop the last truck being made
        CropTrunk(ejerCount);

        // we want the content container to change its height with the content within i.e. the trunk.
        MSScrollContent.GetComponent<RectTransform>().sizeDelta = new Vector2(MSScrollContent.GetComponent<RectTransform>().anchoredPosition.x, HeightDifference);

        // offset the position
        MSScrollContent.GetComponent<RectTransform>().anchoredPosition = new Vector2(MSScrollContent.GetComponent<RectTransform>().anchoredPosition.x, HeightDifference + MSScrollContent.GetComponent<RectTransform>().anchoredPosition.y);


        //string Str = "There is :";
        //foreach (GameObject s in treeTrunks)
        //{
        //    Str = Str + $"{s}, ";
        //}
        //Debug.Log(Str);

    }
    void CropTrunk(int count)
    {
        if (count == 0)
        {
            return;
        }
        else if (count == 1)
        {
            Crop(cropB = 0.20f, cropT = 0.40f);
            HeightDifference -= MSStammePrefab.GetComponent<RectTransform>().sizeDelta.y * (1-cropT);
        }
        else if (count == 2)
        {
            Crop(cropB = 0.50f, cropT = 0.50f);
            HeightDifference -= MSStammePrefab.GetComponent<RectTransform>().sizeDelta.y * (1-cropT);
        }
        else if (count == 3)
        {
            Crop(cropB = 0.60f, cropT = 0.80f);
            HeightDifference -= MSStammePrefab.GetComponent<RectTransform>().sizeDelta.y * (1-cropT);
            //---Dependent on ejerCount---\\
            //CROP N.NN of branch and N.NN of Trunk!
            //ADD the rest of the Bottons!
        }
    }
    void Crop(float a, float b)
    {
        treeTrunks.Last().GetComponent<Image>().fillAmount = a;
        treeTrunks.Last().transform.GetChild(0).GetComponent<Image>().fillAmount = b;
        // treeTrunks.Last().GetComponentsInChildren<Image>().fillAmount = b;
    }
    //foreach (var Log in ejerLog)
    //{

    //}
    //public class Wall : MonoBehaviour
    //{
    //    public GameObject block;
    //    public int width = 10;
    //    public int height = 4;

    //    void Start()
    //    {
    //        for (int y = 0; y < height; ++y)
    //        {
    //            for (int x = 0; x < width; ++x)
    //            {
    //                Instantiate(block, new Vector3(x, y, 0), Quaternion.identity);
    //            }
    //        }
    //    }
    //}






    // Der er ikke brug for parralaxing eller noget real time generering af textures. Jeg kan simpelt nok bare construct hele contents dellen af scroll obj
    // ved brug af assets lig med mængden af sælgere / tidligere ejere, til sidst skal der bare top og en bund på :)
    // Dette sker kun hvis mindetræet allerede er "fuld groet" (nået N antal af tidlere ejere / sælgere), inden dette punkt kunne der være simpel asset skift mellem træerne med

    // Dette kommer nok til at være assetsne hvorledes er sektioner a stamme med en knap, med data formateret af kode samme tagning af data fra en gemt fil. en skal skifte med et tryk af en knap.
    //================================//
    // |            |           KNAP  //
    // |            \       _/---     //
    // |             \----/   /       //
    // |                   /-         //
    // |             ___/--           //
    // |            /                 //
    // |            |                 //
    //================================//

    // FIL ID
    // Dato
    //    |
    //    |
    //    |
    //    \/
    // 





    // Minderne skal soreteres efter dato lavet / solgt, dette bliver gjort via tekstfilen der er formateret således :
    // 10.10.2025 (dato)
    // Dette er en Titel (Titel)
    // Dette er en beskrivelse (beskrivelse)
    // Projekt/Mappe/Mappe/BilledMappe (Sti)
    // Dette bliver nok seppereret af tegn af en art, eller bare tælle gennem pr linje (her ville det så være hvært tredje linje hvor der ville være "cut-off").


    // når der trykkes på et minde skal :
    //      1. der tages data fra et sheet ift : Titel, Beskrivelse og Billede.


    // der er nok assicieret et ID til hvært minde, det sættes på


    //interface - Branches
    //interface - Memories
}
