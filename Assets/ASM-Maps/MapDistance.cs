using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapDistance : MonoBehaviour
{
    public TextMeshProUGUI mapDistanceText;
    public GameObject MarketPlace;
    [SerializeField] Markedsplads markedsplads;
    
    public Slider slider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        markedsplads = MarketPlace.GetComponent<Markedsplads>();
        float newScale = slider.value * 0.0342f;
        int mapDistance = (int)slider.value;
        gameObject.transform.localScale = new Vector3(newScale, newScale, newScale);
        mapDistanceText.text = mapDistance + " meter";
    }
    

    public void ChangeMapDistance()
    {
        float newScale = slider.value * 0.0342f;
        int mapDistance = (int)slider.value;
        gameObject.transform.localScale = new Vector3(newScale, newScale, newScale);
        mapDistanceText.text = mapDistance + " meter";
        markedsplads.SetMaxDistance(mapDistanceText);
    }
}
