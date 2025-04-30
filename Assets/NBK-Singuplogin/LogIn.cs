using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class LogIn : MonoBehaviour
{

    public TMP_InputField Brugernavn;
    public TMP_InputField Password;
    public GameObject Bottombar;
    public GameObject loginPage;

    private List<MParent> mParents;

    public static MParent LoggedIn;
    public static int UserId;
    public bool CreateTestData;
    private void Awake()
    {
        DBManager.Init();
        if (CreateTestData &&  DBManager.GetAllArticles().Count <= 5)
            TestDataGenerator.GenerateRandomTestData(100);

    }

    public void Login()
    {
        mParents = DBManager.GetAllParents();
        MParent loggedInParent = mParents.Find(p => p.Name == Brugernavn.text && p.Password == Password.text);
        if (loggedInParent != null)
        {
            LoggedIn = loggedInParent;
            
            loginPage.SetActive(false);
            //gameObject.SetActive(false);
            UserInformation.Instance.User = loggedInParent;
            UserInformation.Instance.UserChildren = DBManager.GetChildrenByParentId(loggedInParent.Id);
            Bottombar.SetActive(true);
        }
    }
}