using System;
using UnityEngine;

public class OpretTøjSide : MonoBehaviour
{
    public GameObject buttons;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        buttons.SetActive(false);
    }

    private void OnDisable()
    {
        buttons.SetActive(true);
    }
    
}
