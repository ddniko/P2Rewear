using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateArticleManager : MonoBehaviour
{
    public Toggle Forsale;
    public TextMeshPro Name;
    public GameObject prize;
    public Slider Cond;
    public Dropdown Size;
    public Dropdown Cat;
    public TextMeshPro Desc;

    MArticle art;
    private void Start()
    {
        
    }
    public void TogglePriceVisibility()
    {
        if (Forsale.isOn)
        {
            prize.SetActive(true);
            prize.GetComponentInChildren<TextMeshPro>().text = string.Empty;
        }
        if (!Forsale.isOn)
            prize.SetActive(false);
    }
    private bool CheckArticle()
    {
        if (Name.text.Length <= 0) { Debug.Log("Name Lacking"); return false; }       
        if (Size.value == 0) { Debug.Log("Size Lacking"); return false; }        
        if (Cat.value == 0) { Debug.Log("Value Lacking"); return false; }        
        if (Forsale.isOn && prize.GetComponentInChildren<TextMeshPro>().text.Length <= 0) { Debug.Log("Prize Lacking"); return false; }
        if (Forsale.isOn && prize.GetComponentInChildren<TextMeshPro>().text.Length <= 0)
        {
            bool succes = float.TryParse(prize.GetComponentInChildren<TextMeshPro>().text, out float value);
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
                art.Prize = float.Parse(prize.GetComponentInChildren<TextMeshPro>().text);
    }

    public void TakePicture()
    {

    }

}