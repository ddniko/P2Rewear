using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
using static Unity.VisualScripting.Metadata;

public class Mindeskov : BasePage
{
    public override Enum MyPage() => PAGENAMES.MINDESKOV;
    SortScrollScript s;
    public void OnEnable()
    {
        s = GetComponent<SortScrollScript>();
        SetupChildren();
    }
    public void SetupChildren()
    {
        s.CreateChildren();
        foreach (var child in s.CurrentChildren)
        {
            MChild mChild = child.GetComponent<Child>().GetChild;
            child.GetComponent<ChildButton>().SetupChild(mChild);
        }
    }
}
