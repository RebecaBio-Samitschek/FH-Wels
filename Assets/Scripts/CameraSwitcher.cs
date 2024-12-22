using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
   
    public Camera mainCamera1;
    public GameObject backbuttonRoom1Left;
    public GameObject backbuttonRoom1Right;
    
    public Camera mainCamera2;
    public GameObject backbuttonRoom2Left;
    public GameObject backbuttonRoom2Right;

    public Camera mainCamera3;
    public GameObject backbuttonRoom3Left;
    public GameObject backbuttonRoom3Right;

    public Camera mainCamera4;
    public GameObject backbuttonRoom4Left;
    public GameObject backbuttonRoom4Right;

    public Camera counter1Camera;
    public GameObject backbuttonCounter;
    public GameObject counterDoor;

    public Camera ratCamera;
    public GameObject backbuttonRat;
    public GameObject rat;
    public GameObject ratMessages;
    
    public Camera riddle1Camera;
    public GameObject backbuttonRiddle1;
    public GameObject riddle1Paper;

    public Camera riddle2Camera;
    public GameObject backbuttonRiddle2;
    public GameObject writingBoard;

    public Camera riddle3CameraPaper;
    public GameObject backbuttonRiddle3Paper;
    public GameObject riddle3Paper;

    public Camera riddle7Camera;
    public GameObject backbuttonRiddle7;
    public GameObject scale;

    public Camera riddle10Camera;
    public GameObject backbuttonRiddle10;
    public GameObject GlassDoor;
    public GameObject TriggerObject;

    public Camera secretShelveCamera;
    public GameObject backbuttonSecretShelve;
    public GameObject secretWallPart;
    public GameObject labPoster;

    public Camera skeletonCamera;
    public GameObject backButtonSkeleton;
    public GameObject mantel;
    public GameObject CanvasSekeleton;

    //variablen bezeichnung überdenken, ist das wirklich Rätsel 3?
    public Camera clipboardRiddle3Camera;
    public GameObject backbuttonClipboard;
    public GameObject riddle3Clipboard;

    public Camera riddle4Camera;
    public GameObject backbuttonRiddle4;
    public GameObject riddle4Tablet;
    public GameObject Passcode;
    public PasscodeRiddle4 passcodeRiddle4;

    public Camera laptopCamera;
    public GameObject backbuttonlaptop;
    public GameObject laptop;
    public GameObject laptopImage;
    public GameObject colorCode;
    public ColorCode riddle;
    public int inputIndex;

    public Camera cableCabinetCamera;
    public GameObject backbuttonCableCabinet;
    public GameObject cableCabinet;

    public Camera currentCamera; 
    public ThirdRiddleSolved thirdRiddleSolved;

    // Start is called before the first frame update
    void Start()
    {
        currentCamera = mainCamera1;
        currentCamera.enabled = true;
        backbuttonRoom1Left.SetActive(true);
        backbuttonRoom1Right.SetActive(true);

        counter1Camera.enabled = false;
        ratCamera.enabled = false;
        mainCamera2.enabled = false;
        mainCamera3.enabled = false;
        mainCamera4.enabled = false;
        riddle1Camera.enabled = false;
        clipboardRiddle3Camera.enabled = false;
        riddle2Camera.enabled = false;
        riddle3CameraPaper.enabled = false;
        riddle4Camera.enabled = false;
        riddle7Camera.enabled = false;
        riddle10Camera.enabled = false;
        secretShelveCamera.enabled = false;
        laptopCamera.enabled = false;
        cableCabinetCamera.enabled = false;
        skeletonCamera.enabled = false;

        backbuttonCounter.SetActive(false);
        backbuttonRiddle1.SetActive(false);
        backbuttonRiddle2.SetActive(false);
        backbuttonRiddle3Paper.SetActive(false);
        backbuttonRat.SetActive(false);
        ratMessages.SetActive(false);
        backbuttonRiddle4.SetActive(false);
        backbuttonRiddle7.SetActive(false);
        backbuttonClipboard.SetActive(false);
        Passcode.SetActive(false);
        backbuttonSecretShelve.SetActive(false);
        backbuttonlaptop.SetActive(false);
        backbuttonCableCabinet.SetActive(false);  
        backButtonSkeleton.SetActive(false);
        CanvasSekeleton.SetActive(false);
        backbuttonRiddle10.SetActive(false);
}

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log(currentCamera.name);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            { 
                //camera switch after first riddle is solved - doesnt work yet
                if (thirdRiddleSolved.alreadyPlayed)
                {
                   if (hit.collider.gameObject == backbuttonCounter)
                    {
                    HandleBackButton(backbuttonCounter, mainCamera1);
                    }
                }
                
                currentCamera.enabled = false;
                if (hit.collider.gameObject == backbuttonRoom1Left)
                {
                    HandleBackButton(backbuttonCounter, mainCamera4);
                }
                else if (hit.collider.gameObject == backbuttonRoom1Right)
                {
                    HandleBackButton(backbuttonCounter, mainCamera2);
                }
                else if (hit.collider.gameObject == backbuttonRoom2Left)
                {
                    HandleBackButton(backbuttonCounter, mainCamera1);
                }
                else if (hit.collider.gameObject == backbuttonRoom2Right)
                {
                    HandleBackButton(backbuttonCounter, mainCamera3);
                }
                else if (hit.collider.gameObject == backbuttonRoom3Left)
                {
                    HandleBackButton(backbuttonCounter, mainCamera2);
                }
                else if (hit.collider.gameObject == backbuttonRoom3Right)
                {
                    HandleBackButton(backbuttonCounter, mainCamera4);
                }
                else if (hit.collider.gameObject == backbuttonRoom4Left)
                {
                    HandleBackButton(backbuttonCounter, mainCamera3);
                }
                else if (hit.collider.gameObject == backbuttonRoom4Right)
                {
                    HandleBackButton(backbuttonCounter, mainCamera1);
                }
                else if (hit.collider.gameObject == counterDoor)
                {
                    backbuttonCounter.SetActive(true);
                    currentCamera = counter1Camera;
                }
                else if (hit.collider.gameObject == backbuttonCounter)
                {
                    HandleBackButton(backbuttonCounter, mainCamera1);
                }
                else if (hit.collider.gameObject == rat)
                {
                    backbuttonRat.SetActive(true);
                    ratMessages.SetActive(true);
                    currentCamera = ratCamera;
                }
                else if (hit.collider.gameObject == backbuttonRat)
                {
                    HandleBackButton(backbuttonRat, mainCamera1);
                    ratMessages.SetActive(false);
                }
                else if (hit.collider.gameObject == riddle1Paper)
                {
                    currentCamera = riddle1Camera;
                    backbuttonRiddle1.SetActive(true);
                }
                else if (hit.collider.gameObject == backbuttonRiddle1)
                {
                    HandleBackButton(backbuttonRiddle1, mainCamera4);
                }
                else if (hit.collider.gameObject == riddle3Clipboard)
                {
                    currentCamera = clipboardRiddle3Camera;
                    backbuttonClipboard.SetActive(true);
                    backbuttonCounter.SetActive(true);
                }
                else if (hit.collider.gameObject == backbuttonClipboard)
                {
                    HandleBackButton(backbuttonClipboard, counter1Camera);
                }
                else if (hit.collider.gameObject == riddle4Tablet)
                {
                    currentCamera = riddle4Camera;
                    backbuttonRiddle4.SetActive(true);
                    if (!passcodeRiddle4.solved)
                    {
                        Passcode.SetActive(true);
                    }
                }
                else if (hit.collider.gameObject == backbuttonRiddle4)
                {
                    HandleBackButton(backbuttonRiddle4, mainCamera1);
                    Passcode.SetActive(false);
                }
                else if (hit.collider.gameObject == writingBoard)
                {
                    currentCamera = riddle2Camera;
                    backbuttonRiddle2.SetActive(true);
                }
                else if (hit.collider.gameObject == backbuttonRiddle2)
                {
                    HandleBackButton(backbuttonRiddle2, mainCamera2);
                }
                else if (hit.collider.gameObject == riddle3Paper)
                {
                    currentCamera = riddle3CameraPaper;
                    backbuttonRiddle3Paper.SetActive(true);
                }
                else if (hit.collider.gameObject == scale)
                {
                    currentCamera = riddle7Camera;
                    backbuttonRiddle7.SetActive(true);
                }
                else if (hit.collider.gameObject == backbuttonRiddle7)
                {
                    HandleBackButton(backbuttonRiddle7, mainCamera4);
                }
                else if (hit.collider.gameObject == TriggerObject)
                {
                    currentCamera = riddle10Camera;
                    backbuttonRiddle10.SetActive(true);
                }
                else if (hit.collider.gameObject == GlassDoor)
                {
                    currentCamera = riddle10Camera;
                    backbuttonRiddle10.SetActive(true);
                }
                else if (hit.collider.gameObject == backbuttonRiddle10)
                {
                    HandleBackButton(backbuttonRiddle10, mainCamera2);
                }
                else if (hit.collider.gameObject == backbuttonRiddle3Paper)
                {
                    HandleBackButton(backbuttonRiddle3Paper, mainCamera3);
                }
                else if (hit.collider.gameObject == secretWallPart || hit.collider.gameObject == labPoster)
                {
                    currentCamera = secretShelveCamera;
                    backbuttonSecretShelve.SetActive(true);
                }
                else if (hit.collider.gameObject == backbuttonSecretShelve)
                {
                    HandleBackButton(backbuttonSecretShelve, mainCamera1);
                }
                else if (hit.collider.gameObject == laptop)
                {
                    currentCamera = laptopCamera;
                    backbuttonlaptop.SetActive(true);
                    laptopImage.SetActive(false);
                    if (!riddle.solved)
                    {
                        colorCode.SetActive(true);
                    }
                }
                else if (hit.collider.gameObject == backbuttonlaptop)
                {
                    HandleBackButton(backbuttonlaptop, mainCamera4);
                    if (riddle.solved)
                    {
                        laptopImage.SetActive (false);
                        colorCode.SetActive(false);
                    }
                    else
                    {
                        laptopImage.SetActive (true);
                        colorCode.SetActive (false);
                        inputIndex = 0;
                    }
                }
                else if (hit.collider.gameObject == cableCabinet)
                {
                    currentCamera = cableCabinetCamera;
                    backbuttonCableCabinet.SetActive(true);
                }
                else if (hit.collider.gameObject == backbuttonCableCabinet)
                {
                    HandleBackButton(backbuttonCableCabinet, mainCamera3);
                }
                else if (hit.collider.gameObject == mantel)
                {
                    currentCamera = skeletonCamera;
                    backButtonSkeleton.SetActive(true);
                    CanvasSekeleton.SetActive(true);
                }
                else if (hit.collider.gameObject == backButtonSkeleton)
                {
                    HandleBackButton(backButtonSkeleton, mainCamera3);
                    CanvasSekeleton.SetActive(false);
                }

                currentCamera.enabled = true;
            }
        }
    }

    public void HandleBackButton(GameObject backButton, Camera previousCamera)
    {
        currentCamera = previousCamera;
        backButton.SetActive(false);
        currentCamera.enabled = true;
    }

}
