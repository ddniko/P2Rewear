using Unity.VisualScripting;
using UnityEngine;

//interface ImindeStamme : MonoBehaviour
//{
    
//}



public class Scroll : MonoBehaviour
{
    public int tidligereEjer;
    private int ejerPerScroll;
    public GameObject MSStammePrefab;
    public GameObject MSMindeBtn;
    public GameObject MSScrollContent;

    void AddTrunk()
    {
        ejerPerScroll = 4;
        int ejerCount = tidligereEjer % ejerPerScroll;
        float pagescrolls = tidligereEjer / ejerPerScroll;

        CropTrunk(ejerCount);
        for (int i = 0; i < pagescrolls; i++)
        {
            float TVal = (i * MSStammePrefab.GetComponent<RectTransform>().sizeDelta.y);
            Instantiate(MSStammePrefab, new Vector2 (0, TVal), new Quaternion(),MSScrollContent.transform);
            MSScrollContent.transform.localPosition.y = TVal; MSScrollContent.transform.localScale.y = TVal;


            //GameObject NewObj = Instantiate(MSStammePrefab);
            //NewObj.transform.parent = MSScrollContent.transform;
            //NewObj.transform.localPosition = (i * NewObj.GetComponent<RectTransform>().sizeDelta.y);
        }
        
        

    }
    void CropTrunk(int ejerCount)
    {
        if (ejerCount == 0)
        {
            return;
        }
        else
        {
            //---Dependent on ejerCount---\\
            //CROP N.NN of branch and N.NN of Trunk!
            //ADD the rest of the Bottons!
        }
    }

    //foreach (var Log in ejerLog)
    //{

    //}
    public class Wall : MonoBehaviour
    {
        public GameObject block;
        public int width = 10;
        public int height = 4;

        void Start()
        {
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    Instantiate(block, new Vector3(x, y, 0), Quaternion.identity);
                }
            }
        }
    }






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
