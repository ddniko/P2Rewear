using UnityEngine;

public class LocationTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Input.location.Start(10f, 10f);
        Debug.Log(Input.location.status + "Hej");
        if (Input.location.status == LocationServiceStatus.Initializing)
        {
            Debug.Log(Input.location.lastData.latitude + Input.location.lastData.longitude);
        }
        Input.location.Stop();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
