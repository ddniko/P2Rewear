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
    // ved brug af assets lig med m�ngden af s�lgere / tidligere ejere, til sidst skal der bare top og en bund p� :)
    // Dette sker kun hvis mindetr�et allerede er "fuld groet" (n�et N antal af tidlere ejere / s�lgere), inden dette punkt kunne der v�re simpel asset skift mellem tr�erne med

    // Dette kommer nok til at v�re assetsne hvorledes er sektioner a stamme med en knap, med data formateret af kode samme tagning af data fra en gemt fil. en skal skifte med et tryk af en knap.
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





    // Minderne skal soreteres efter dato lavet / solgt, dette bliver gjort via tekstfilen der er formateret s�ledes :
    // 10.10.2025 (dato)
    // Dette er en Titel (Titel)
    // Dette er en beskrivelse (beskrivelse)
    // Projekt/Mappe/Mappe/BilledMappe (Sti)
    // Dette bliver nok seppereret af tegn af en art, eller bare t�lle gennem pr linje (her ville det s� v�re hv�rt tredje linje hvor der ville v�re "cut-off").


    // n�r der trykkes p� et minde skal :
    //      1. der tages data fra et sheet ift : Titel, Beskrivelse og Billede.


    // der er nok assicieret et ID til hv�rt minde, det s�ttes p�


    //interface - Branches
    //interface - Memories
}
