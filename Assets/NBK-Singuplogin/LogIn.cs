using System;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class LogIn : MonoBehaviour
{

    public List<RawImage> imageSprites;
    //public List<Sprite> sprites;
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
        List<byte[]> imageBytes = new List<byte[]>();
        DBManager.Init();
        foreach (RawImage image in imageSprites)
        {
            image.rectTransform.sizeDelta = new Vector2(228.11f, 238.87f);
            imageBytes.Add(ConvertImageToByteArray(image));
        }
        TestDataGenerator.images = imageBytes;
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
    
    public byte[] ConvertImageToByteArray(RawImage rawImage)
    {

        if (rawImage.texture != null)
        {

            Texture2D texture2D = rawImage.texture as Texture2D;

            if (texture2D != null)
            {
                byte[] imageBytes = texture2D.EncodeToPNG(); 

                return imageBytes;
            }
            else
            {
                Debug.LogError("texturen er ikke 2d");
                return null;
            }
        }
        else
        {
            Debug.LogError("mangler en texture.");
            return null;
        }
    }
}