using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
public enum SIZETYPE { CM, SIZE}
public class ChildSizeSelect : MonoBehaviour
{
    public Toggle Centimeter;



    public SIZETYPE st;

    private bool isUpdating = false;

    void Start()
    {
        Centimeter.onValueChanged.AddListener((isOn) => {
            if (isOn) SetSizeType(SIZETYPE.CM);
        });


    }

    private void SetSizeType(SIZETYPE selected)
    {
        if (isUpdating) return;
        isUpdating = true;

        st = selected;

        
        Centimeter.isOn = selected == SIZETYPE.CM;



        Debug.Log("Selected SizeType: " + st);

        isUpdating = false;
    }
}
