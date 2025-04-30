using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateArticleManager : BasePage
{
    public override Enum MyPage() => PAGENAMES.OPRET_TØJ;

    public Toggle Forsale;
    public TextMeshProUGUI Name;
    public GameObject prize;
    public TextMeshProUGUI prizeText;
    public Slider Cond;
    public TextMeshProUGUI Size;
    public TMP_Dropdown Cat;
    public TextMeshProUGUI Desc;
    public TagOrganizer TO;
    public CameraHandler camhand;
    public DBVisualizer DBV;
    public RawImage img;
    MArticle art;
    public TextMeshProUGUI faillog;
    public bool GenerateData;
    private void Start()
    {
        
        //if (GenerateData)
            //DBManager.GenerateTestData(5);
    }
    public void TogglePriceVisibility()
    {
        if (Forsale.isOn)
        {
            prize.SetActive(true);
            prizeText.text = string.Empty;
        }
        if (!Forsale.isOn)
            prize.SetActive(false);
    }
    private bool CheckArticle()
    {
        if (Name.text.Length <= 0) { Debug.Log("Name Lacking"); return false; }
        if (Size.text.Length == 0) { Debug.Log("Size Lacking"); return false; }
        if (Cat.value == 0) { Debug.Log("Value Lacking"); return false; }
        if (Forsale.isOn && prizeText.text.Length <= 0) { Debug.Log("Prize Lacking"); return false; }
        if (Forsale.isOn && prizeText.text.Length <= 0)
        {
            bool succes = float.TryParse(prizeText.text, out float value);
            if (!succes)
                Debug.Log("Prize Invalid"); return false;
        }
        Debug.Log("Article Viable");
        return true;
    }
    public void CreateArticle()
    {
        if (!CheckArticle()) return;

        art = new MArticle();
        art.Name = Name.text;
        art.Size = Size.text.ToString();
        if (Forsale.isOn)
        {
            float parsedPrize;
            if (float.TryParse(prizeText.text.Replace(',', '.'), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out parsedPrize))
            {
                art.Prize = parsedPrize;
            }
        }
        string tags = "";
        for (int i = 0; i < TO.tagValues.Count; i++)
        {
            if (i == TO.tagValues.Count - 1)
            {
                tags += TO.tagValues[i].ToString();
                continue;
            }
            tags += TO.tagValues[i].ToString() + ", ";
        }
        art.Tags = tags;
        art.ImageData = ConvertImageToByteArray(img);
        art.ParentId = UserInformation.Instance.User.Id;
        art.ChildId = UserInformation.Instance.UserChildren[0].Id;
        //art.Prize = int.TryParse(prizeText.text);
        Debug.Log("Article Created" + prizeText.text +"dsadsa");
        if (float.TryParse(prizeText.text, out float prize))
        {
            art.Prize = prize;
        }
        else
        {
            art.Prize = 30;
        }
        art.Condition = Cond.value;
        DBManager.AddArticle(art);
        //faillog.text = $"{Application.persistentDataPath}/clothing.db";
        
    }

    public void TakePicture()
    {
        camhand.OpenCam();
    }

    public byte[] ConvertImageToByteArray(RawImage rawImage)
    {

        if (rawImage.texture != null)
        {

            Texture2D texture2D = rawImage.texture as Texture2D;

            if (texture2D != null)
            {
                byte[] imageBytes = texture2D.EncodeToPNG(); 

                return imageBytes;
            }
            else
            {
                Debug.LogError("texturen er ikke 2d");
                return null;
            }
        }
        else
        {
            Debug.LogError("mangler en texture.");
            return null;
        }
    }


}