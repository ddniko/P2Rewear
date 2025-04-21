using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
public enum SIZETYPE { CM, SIZE}
public class ChildSizeSelect : MonoBehaviour
{
    public Toggle Centimeter;
    public Toggle Size;


    public SIZETYPE st;

    private bool isUpdating = false;

    void Start()
    {
        Centimeter.onValueChanged.AddListener((isOn) => {
            if (isOn) SetSizeType(SIZETYPE.CM);
        });

        Size.onValueChanged.AddListener((isOn) => {
            if (isOn) SetSizeType(SIZETYPE.SIZE);
        });
    }

    private void SetSizeType(SIZETYPE selected)
    {
        if (isUpdating) return;
        isUpdating = true;

        st = selected;

        
        Centimeter.isOn = selected == SIZETYPE.CM;
        Size.isOn = selected == SIZETYPE.SIZE;


        Debug.Log("Selected SizeType: " + st);

        isUpdating = false;
    }
}
