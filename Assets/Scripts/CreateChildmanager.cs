using System;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateChildmanager : MonoBehaviour
{

    public TMP_InputField Name;
    public RawImage img;
    public TMP_Dropdown Age1;
    public TMP_Dropdown Age2;
    public TMP_Dropdown Age3;
    public ChildGenderSelect cgs;
    public ChildSizeSelect CSS;
    public TagOrganizer TO;
    public TMP_InputField Centi;
    public TMP_Dropdown SizeDrop;
    private string size;
    public CameraHandler camhand;
    public void CreateChild()
    {

        if (CSS.st == SIZETYPE.CM)
        {
            size = Centi.text;
        }
        else if (CSS.st == SIZETYPE.SIZE)
        {
            size = SizeDrop.value.ToString();
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

        MChild mChild = new MChild
        {
            Name = Name.text,
            
            Gender = cgs.gender,
            Size = size,
            Age = SelectAge(),
            Tags = tags,
        };
    }
    //private bool CheckChild()
    //{
    //    if (Name.text.Length <= 0) { faillog.text = "Name Lacking"; return false; }
    //    if (Size.value == 0) { faillog.text = "Size Lacking"; return false; }
    //    if (Cat.value == 0) { faillog.text = "Value Lacking"; return false; }
    //    if (Forsale.isOn && prizeText.text.Length <= 0) { faillog.text = "Prize Lacking"; return false; }
    //    if (Forsale.isOn && prizeText.text.Length <= 0)
    //    {
    //        bool succes = float.TryParse(prizeText.text, out float value);
    //        if (!succes)
    //            faillog.text = "Prize Invalid"; return false;
    //    }
    //    faillog.text = "Article Viable";
    //    return true;
    //}
    void Start()
    {
        PopulateDropDowns();
    }

    // Update is called once per frame
    void Update()
    {

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
    public void PopulateDropDowns()
    {
        //  Days 
        List<TMP_Dropdown.OptionData> days = new List<TMP_Dropdown.OptionData>();
        days.Add(new TMP_Dropdown.OptionData("Day"));
        for (int i = 1; i <= 31; i++)
        {
            days.Add(new TMP_Dropdown.OptionData(i.ToString()));
        }
        Age1.ClearOptions();
        Age1.AddOptions(days);
        Age1.value = 0;
        Age1.RefreshShownValue();

        //  Months 
        string[] months = System.Globalization.DateTimeFormatInfo.InvariantInfo.MonthNames;
        List<TMP_Dropdown.OptionData> monthOptions = new List<TMP_Dropdown.OptionData>();
        monthOptions.Add(new TMP_Dropdown.OptionData("Month"));
        for (int i = 0; i < 12; i++)
        {
            monthOptions.Add(new TMP_Dropdown.OptionData(months[i]));
        }
        Age2.ClearOptions();
        Age2.AddOptions(monthOptions);
        Age2.value = 0;
        Age2.RefreshShownValue();

        //  Years 
        int currentYear = DateTime.Now.Year;
        List<TMP_Dropdown.OptionData> years = new List<TMP_Dropdown.OptionData>();
        years.Add(new TMP_Dropdown.OptionData("Year"));
        for (int i = 0; i <= 60; i++)
        {
            int year = currentYear - i;
            years.Add(new TMP_Dropdown.OptionData(year.ToString()));
        }
        Age3.ClearOptions();
        Age3.AddOptions(years);
        Age3.value = 0;
        Age3.RefreshShownValue();
    }
    public string SelectAge()
    {
        if (Age1.value == 0 || Age2.value == 0 || Age3.value == 0)
        {
            Debug.LogWarning("Der mangler valg p� Ages");
            return string.Empty;
        }
        else
        {
            string cAge = Age1.options[Age1.value].text + "," +
                          Age2.options[Age2.value].text + "," +
                          Age3.options[Age3.value].text;
            Debug.Log("ages resultat: " + cAge);
            return cAge;
        }
    }
}
