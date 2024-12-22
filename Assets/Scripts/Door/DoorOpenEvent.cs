using System;
using UnityEngine;


public class DoorOpenEvent : MonoBehaviour
{

    // Set on editor.
    [SerializeField] private GameObject _camsParent;
    [SerializeField] private Camera _finishCam;
    
    // Animation Related Components
    private Animator _doorAnimator;
    
    // Animation parameter
    private static readonly int OpenDoor = Animator.StringToHash("OpenDoor");

    void Start()
    {
        _doorAnimator = GetComponent<Animator>();
        if (_finishCam is null)
            Debug.LogWarning("ERROR, THERE IS NO CAMERA SET TO THE OPEN DOR ANIMATION");
    }
    
    /// <summary>
    /// Sets the right camera and plays the opening animation.
    /// </summary>
    public void PlayDoorEvent()
    {
        if (_finishCam is not null)
            SetFinishCamera();
        PlayDoorAnimation();
    }
    
    /// <summary>
    /// Sets the right parameter on the animator component.
    /// </summary>
    private void PlayDoorAnimation()
    {
        _doorAnimator.SetTrigger(OpenDoor);
    }
    
    /// <summary>
    /// Disables all cameras, then enables the desired one.
    /// </summary>
    private void SetFinishCamera()
    {
        foreach (Camera cam in _camsParent.GetComponentsInChildren<Camera>())
            cam.enabled = false;
        _finishCam.enabled = true;
    }
    
}
