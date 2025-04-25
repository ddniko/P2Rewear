using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;



public class ScrollTrigger : MonoBehaviour
{

    public ScrollRect scrollRect;
    [Range(0f, 1f)]
    public float triggerThreshold = 0.2f; // Trigger når 20% er tilbage
    public UnityEvent onScrollThresholdReached;

    private bool hasTriggered = false;

    void Update()
    {
        if (!hasTriggered && scrollRect.verticalNormalizedPosition <= triggerThreshold)
        {
            hasTriggered = true;
            onScrollThresholdReached.Invoke();
        }
    }

    // Kald denne hvis du vil tillade retrigger, fx efter at have tilføjet flere objekter
    public void ResetTrigger()
    {
        hasTriggered = false;
    }
}


