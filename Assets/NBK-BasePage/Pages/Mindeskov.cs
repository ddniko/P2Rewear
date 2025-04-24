using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;

public class Mindeskov : BasePage
{
    public override Enum MyPage() => PAGENAMES.MINDESKOV;

    public GameObject childPrefab;


    //private void OnEnable()
    //{
    //    List<MChild> children = DBManager.GetChildrenByParentId(LogIn.LoggedIn.Id);

    //    foreach (MChild child in children)
    //    {
    //        GameObject kid = Instantiate(childPrefab);//child af hvor den skal være(definer position)
    //        kid.GetComponent<ChildButton>().thisChild = child;


    //    }
    //}
}
