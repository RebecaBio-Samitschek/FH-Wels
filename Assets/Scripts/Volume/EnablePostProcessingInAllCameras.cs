using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnablePostProcessingInAllCameras : MonoBehaviour
{
	
    // Start is called before the first frame update
    void Start()
    {
	    foreach (Camera cam in FindObjectsOfType<Camera>())
		    cam.GetUniversalAdditionalCameraData().renderPostProcessing = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
