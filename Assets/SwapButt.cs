using UnityEngine;

public class SwapButt : MonoBehaviour
{
    public GameObject clothesInput;
    public GameObject clothesDisplay;
    public bool displayinginput = true;

    public void ChangeDisplay()
    {
        if (displayinginput)
        {
            clothesInput.SetActive(false);
            clothesDisplay.SetActive(true);
        }
        else
        {
            clothesDisplay.SetActive(false);
            clothesInput.SetActive(true);
        }
        displayinginput = !displayinginput;
    }
}
