using UnityEngine;

public class MSBttnOverlayManager : MonoBehaviour
{
    [SerializeField] private GameObject mindestammeOverlayEditOrCreateOwnMem;
    [SerializeField] private GameObject mindestammeOverlayUserMems;

    private SetupMemoryArticle setup; // we set-up / format the overlays with information when the overlay is being activated

    public void OpenUserMemOverlay(MMemory memory) // this is the on click enter method for the memory overlay
    {
        mindestammeOverlayUserMems.SetActive(true);
        setup = mindestammeOverlayUserMems.GetComponent<SetupMemoryArticle>();
        setup.SetupMemory(memory);
    }

    public void CloseUserMemOverlay()
    {
        mindestammeOverlayUserMems.SetActive(false);
    }


    public void OpenOwnEditOrCreateMemOverlay(MMemory memory)
    {
        mindestammeOverlayEditOrCreateOwnMem.SetActive(true);
        setup = mindestammeOverlayEditOrCreateOwnMem.GetComponent<SetupMemoryArticle>();
        setup.SetupMemory(memory);
    }

    public void CloseOwnEditOrCreateMemOverlay()
    {
        mindestammeOverlayEditOrCreateOwnMem.SetActive(false);
    }
}
