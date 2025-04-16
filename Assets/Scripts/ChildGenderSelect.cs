using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class ChildGenderSelect : MonoBehaviour
{
    public Toggle Boy;
    public Toggle Girl;
    public Toggle Other;

    public GENDER gender;

    private bool isUpdating = false;

    void Start()
    {
        Boy.onValueChanged.AddListener((isOn) => {
            if (isOn) SetGender(GENDER.Male);
        });

        Girl.onValueChanged.AddListener((isOn) => {
            if (isOn) SetGender(GENDER.Female);
        });

        Other.onValueChanged.AddListener((isOn) => {
            if (isOn) SetGender(GENDER.Other);
        });
    }

    private void SetGender(GENDER selected)
    {
        if (isUpdating) return;
        isUpdating = true;

        gender = selected;

        
        Boy.isOn = selected == GENDER.Male;
        Girl.isOn = selected == GENDER.Female;
        Other.isOn = selected == GENDER.Other;

        Debug.Log("Selected Gender: " + gender);

        isUpdating = false;
    }
}
