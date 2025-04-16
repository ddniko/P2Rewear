using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class LogIn : MonoBehaviour
{

    public TMP_InputField Brugernavn;
    public TMP_InputField Password;
    public GameObject Bottombar;

    private List<MParent> mParents;

    public static MParent LoggedIn;

    public void Login()
    {
        mParents = DBManager.GetAllParents();
        MParent loggedInParent = mParents.Find(p => p.Name == Brugernavn.text && p.Password == Password.text);
        if (loggedInParent != null)
        {
            Bottombar.SetActive(true);
            gameObject.SetActive(false);
            LoggedIn = loggedInParent;
        }
    }
}