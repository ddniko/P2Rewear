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
    MArticle art;
    private void Start()
    {
        DBManager.Init();
        DBManager.AddChild("tom", 0, 4, "stor");
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
        if (Size.value == 0) { Debug.Log("Size Lacking"); return false; }
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
        art.SizeCategory = Size.value.ToString();
        if (Forsale.isOn)
        {
            float parsedPrize;
            if (float.TryParse(prizeText.text.Replace(',', '.'), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out parsedPrize))
            {
                art.Prize = parsedPrize;
            }
        }
            

        DBManager.AddArticle(art);
        DBV.DisplayArticles(0);
    }

    public void TakePicture()
    {

    }


}