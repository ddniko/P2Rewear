using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class Signup : MonoBehaviour
{

    public TMP_InputField Password;
    public TMP_InputField Email;
    public TMP_InputField Brugernavn;
    public TMP_InputField Gentag;
    public GameObject Bottombar;

    public void MakeAccount()
    {
        if (Password.text == Gentag.text && Password.text != "" && Brugernavn.text != "" && Email.text != "")
        {
            //DBManager.AddParent(Brugernavn.text, 0, 0, Password.text, Email.text);
            Bottombar.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("try again");
        }
    }
}
