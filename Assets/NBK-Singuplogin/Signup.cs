using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Signup : MonoBehaviour
{

    public TMP_InputField Password;
    public TMP_InputField Email;
    public TMP_InputField Brugernavn;
    public TMP_InputField Gentag;
    public GameObject Login;

    private void Awake()
    {
        DBManager.Init();
    }
    public void MakeAccount()
    {
        if (Password.text == Gentag.text && Password.text != "" && Brugernavn.text != "" && Email.text != "")
        {
            DBManager.AddParent(Brugernavn.text, 0, 0, Password.text, Email.text, 0);
            Login.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("try again");
        }
    }
}
