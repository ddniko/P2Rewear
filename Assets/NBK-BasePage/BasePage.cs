using System;
using UnityEngine;

public abstract class BasePage : MonoBehaviour
{
    public BasePage[] pages;

    private void Awake()
    {
        pages = FindObjectsByType<BasePage>(FindObjectsSortMode.None);
    }

    private void Start()
    {
        if (gameObject.GetComponent<BasePage>().MyPage().ToString() != PAGENAMES.MARKEDSPLADS.ToString())
        {
            gameObject.SetActive(false);
        }
    }


    public enum PAGENAMES
    {
        MINDESKOV,
        MARKEDSPLADS,
        OPRET_TØJ,
        PROFIL,
        CHAT
    }

    public abstract Enum MyPage();

    private void OpenPage(Enum Page)
    {
        foreach (BasePage page in pages)
        {
            if (page.MyPage() == Page)
            {
                page.gameObject.SetActive(true);
            }
        }
    }

    private void ClosePage(Enum Page)
    {
        foreach (BasePage page in pages)
        {
            if (page.MyPage().ToString() == Page.ToString())
            {
                page.gameObject.SetActive(true);
            }
            else
            {
                page.gameObject.SetActive(false);
            }
        }
    }
    
    public void ChangePage()
    {
        ClosePage(gameObject.GetComponent<BasePage>().MyPage());


        //OpenPage(gameObject.GetComponent<BasePage>().MyPage());
    }

}
