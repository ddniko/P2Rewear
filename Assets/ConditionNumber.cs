using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConditionNumber : MonoBehaviour
{
    public Slider slider;

    public void ChangeCondition()
    {
        TextMeshProUGUI text = gameObject.GetComponent<TextMeshProUGUI>();
        text.text = slider.value.ToString() + "/5";
    }
}
