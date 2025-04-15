using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapDistance : MonoBehaviour
{
    public TextMeshProUGUI mapDistanceText;
    
    public Slider slider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float newScale = slider.value * 0.0342f;
        int mapDistance = (int)slider.value;
        gameObject.transform.localScale = new Vector3(newScale, newScale, newScale);
        mapDistanceText.text = mapDistance + " meter";
    }
    

    public void changeMapDistance()
    {
        float newScale = slider.value * 0.0342f;
        int mapDistance = (int)slider.value;
        gameObject.transform.localScale = new Vector3(newScale, newScale, newScale);
        mapDistanceText.text = mapDistance + " meter";
    }
}
