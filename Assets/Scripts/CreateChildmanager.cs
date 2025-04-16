using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreateChildmanager : MonoBehaviour
{

    public TMP_InputField Name;
    public TMP_Dropdown Age1;
    public TMP_Dropdown Age2;
    public TMP_Dropdown Age3;
    public ChildGenderSelect cgs;
    public ChildSizeSelect CSS;
    public TMP_InputField Centi;
    public TMP_Dropdown SizeDrop;
    private string size;
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
        string cAge;
        cAge = Age1.value.ToString() + "," + Age2.value.ToString() + "," + Age3.value.ToString();
        MChild mChild = new MChild
        {
            Name = Name.text,
            Gender = cgs.gender,
            Size = size,
            Age = SelectAge(),

        };
    }
    void Start()
    {
        PopulateDropDowns();
    }

    // Update is called once per frame
    void Update()
    {
        
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
            Debug.LogWarning("Der mangler valg på Ages");
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
