using System;
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
    public TMP_Dropdown Size;
    public TMP_Dropdown Cat;
    public TextMeshProUGUI Desc;
    public CameraHandler camhand;
    public DBVisualizer DBV;
    public RawImage img;
    MArticle art;
    public TextMeshProUGUI faillog;
    public bool GenerateData;
    private void Start()
    {
        DBManager.Init();
        if (GenerateData)
            DBManager.GenerateTestData(5);
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
        if (Name.text.Length <= 0) { faillog.text ="Name Lacking"; return false; }
        if (Size.value == 0) { faillog.text = "Size Lacking"; return false; }
        if (Cat.value == 0) { faillog.text = "Value Lacking"; return false; }
        if (Forsale.isOn && prizeText.text.Length <= 0) { faillog.text = "Prize Lacking"; return false; }
        if (Forsale.isOn && prizeText.text.Length <= 0)
        {
            bool succes = float.TryParse(prizeText.text, out float value);
            if (!succes)
                faillog.text = "Prize Invalid"; return false;
        }
        faillog.text = "Article Viable";
        return true;
    }
    public void CreateArticle()
    {
        if (!CheckArticle()) return;

        art = new MArticle();
        art.Name = Name.text;
        art.SizeCategory = Size.value.ToString();
        if (Forsale.isOn)
        {
            float parsedPrize;
            if (float.TryParse(prizeText.text.Replace(',', '.'), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out parsedPrize))
            {
                art.Prize = parsedPrize;
            }
        }
        art.ImageData = ConvertImageToByteArray(img);

        DBManager.AddArticle(art);
        faillog.text = $"{Application.persistentDataPath}/clothing.db";
        
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