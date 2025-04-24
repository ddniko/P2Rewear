using UnityEngine;

public class MSBttnOverlayManager : MonoBehaviour
{
    public GameObject mindestammeOverlayEditOrCreateOwnMem;
    public GameObject mindestammeOverlayUserMems;

    private SetupMemoryArticle setup; // we set-up / format the overlays with information when the overlay is being activated
    private CreateMemory create;

    public static MSBttnOverlayManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }

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
