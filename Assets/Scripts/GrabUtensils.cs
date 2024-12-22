using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabUtensils : MonoBehaviour
{
    public GameObject mantel;
    public GameObject brille;

    public GameObject riddle1Blocker;
    public GameObject riddle2Canvas;
    public GameObject riddle3Button;
    public GameObject riddle4Canvas;
    public GameObject colorCodeBlocker;

    public Camera skeletonCamera;
    public Text messages;
    private float displayDuration = 3.0f;
    private float displayTimer = 0.0f;
    private bool isDisplayingText = false;
    private bool mantelActive = true;
    private bool brilleActive = true;


    // Start is called before the first frame update
    void Start()
    {
        mantel.SetActive(true);
        brille.SetActive(true);
        riddle1Blocker.SetActive(true);
        riddle2Canvas.SetActive(false);
        riddle3Button.SetActive(false);
        riddle4Canvas.SetActive(false);
        colorCodeBlocker.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && skeletonCamera.enabled)
        {
            RaycastHit hit;
            Ray ray = skeletonCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            { 
                if (hit.collider.gameObject == mantel)
                {
                    messages.gameObject.SetActive(true);
                    mantel.SetActive(false);
                    messages.text = "Mantel aufgenommen.";
                    isDisplayingText = true;
                    mantelActive=false;
                }
                else if (hit.collider.gameObject == brille)
                {
                    messages.gameObject.SetActive(true);
                    brille.SetActive(false);                
                    messages.text = "Brille aufgenommen.";
                    isDisplayingText = true;
                    brilleActive=false;
                }
            }
        }
        if (isDisplayingText)
        {
            displayTimer += Time.deltaTime;
            if (displayTimer >= displayDuration)
            {
                messages.gameObject.SetActive(false);
                displayTimer = 0.0f;
                isDisplayingText = false;
            }
        }
        if(!mantelActive && !brilleActive){
            riddle1Blocker.SetActive(false);
            riddle2Canvas.SetActive(true);
            riddle3Button.SetActive(true);
            riddle4Canvas.SetActive(true);
            colorCodeBlocker.SetActive(false);   
        }
    }
}
