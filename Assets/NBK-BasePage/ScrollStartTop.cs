using UnityEngine;
using UnityEngine.UI;

public class ScrollStartTop : MonoBehaviour
{
    public ScrollRect scrollRect;

    void OnEnable()
    {
        // Make sure to reset after a frame to ensure layout has refreshed
        StartCoroutine(ResetScrollNextFrame());
    }

    private System.Collections.IEnumerator ResetScrollNextFrame()
    {
        yield return null; // Wait one frame
        scrollRect.verticalNormalizedPosition = 1f; // Top of the scroll
    }
}
