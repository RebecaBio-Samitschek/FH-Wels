using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class labTips : MonoBehaviour
{
//    public GameObject rat;
//    public Camera ratCamera;
//    public Text messages;
//    private float displayDuration = 3.0f;
//    private float displayTimer = 0.0f;
//    private bool isDisplayingText = false;
//    private int count;

//    public bool riddle1solved;
//    public bool riddle2solved;
//    public bool riddle3solved;
//    public bool riddle4solved;
//    public bool riddle5solved;
//    public bool riddle6solved;
//    public bool riddle7solved;
//    public bool riddle8solved;

//    public FirstRiddleSolved firstRef;
//    public SecondRiddle secondRef;
//    public ThirdRiddleSolved thirdRef;


//    // Start is called before the first frame update
//    void Start()
//    {
//        count = 0;
//        riddle1solved = false;
//        riddle2solved = false;
//        riddle3solved = false;
//        riddle4solved = false;
//        riddle5solved = false;
//        riddle6solved = false;
//        riddle7solved = false;
//        riddle8solved = false;

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        riddle1solved = firstRef.solved;
//        riddle2solved = secondRef.solved;
//        riddle3solved = thirdRef.solved;
        
//        if (Input.GetMouseButtonDown(0) && ratCamera.enabled)
//        {
//            RaycastHit hit;
//            Ray ray = ratCamera.ScreenPointToRay(Input.mousePosition);

//            if (Physics.Raycast(ray, out hit))
//            { 
//                if (hit.collider.gameObject == rat)
//                {
//                    if(count==0){
//                        messages.gameObject.SetActive(true);
//                        messages.text = "Hallo! Ich bin Max.";
//                        count++;
//                    } else if(count==1){
//                        messages.gameObject.SetActive(true);
//                        messages.text = "Ich kann dir hilfreiche Tipps geben!";
//                        count++;    
//                    } else if (!riddle1solved && !riddle2solved && !riddle3solved && !riddle4solved && !riddle5solved && !riddle6solved && !riddle7solved && !riddle8solved){
//                        messages.gameObject.SetActive(true);
//                        messages.text = "Tipp für Rätsel 1: ...";
//                        Debug.Log(riddle1solved); 
//                    } else if (riddle1solved && !riddle2solved && !riddle3solved && !riddle4solved && !riddle5solved && !riddle6solved && !riddle7solved && !riddle8solved){
//                        messages.gameObject.SetActive(true);
//                        messages.text = "Tipp für Rätsel 2: ...";
//                        Debug.Log(riddle2solved); 
//                    } else if (riddle1solved && riddle2solved && !riddle3solved && !riddle4solved && !riddle5solved && !riddle6solved && !riddle7solved && !riddle8solved){
//                        messages.gameObject.SetActive(true);
//                        messages.text = "Tipp für Rätsel 3: ...";
//                        Debug.Log(riddle3solved);
//                    } else if (riddle1solved && riddle2solved && riddle3solved && !riddle4solved && !riddle5solved && !riddle6solved && !riddle7solved && !riddle8solved){
//                        messages.gameObject.SetActive(true);
//                        messages.text = "Tipp für Rätsel 4: ...";
//                        Debug.Log(riddle4solved);
//                    } else if (riddle1solved && riddle2solved && riddle3solved && riddle4solved && !riddle5solved && !riddle6solved && !riddle7solved && !riddle8solved){
//                        messages.gameObject.SetActive(true);
//                        messages.text = "Tipp für Rätsel 5: ...";
//                        Debug.Log(riddle5solved);
//                    } else if (riddle1solved && riddle2solved && riddle3solved && riddle4solved && riddle5solved && !riddle6solved && !riddle7solved && !riddle8solved){
//                        messages.gameObject.SetActive(true);
//                        messages.text = "Tipp für Rätsel 6: ...";
//                        Debug.Log(riddle6solved);
//                    } else if (riddle1solved && riddle2solved && riddle3solved && riddle4solved && riddle5solved && riddle6solved && !riddle7solved && !riddle8solved){
//                        messages.gameObject.SetActive(true);
//                        messages.text = "Tipp für Rätsel 7: ...";
//                        Debug.Log(riddle7solved);
//                    } else if (riddle1solved && riddle2solved && riddle3solved && riddle4solved && riddle5solved && riddle6solved && riddle7solved && !riddle8solved){
//                        messages.gameObject.SetActive(true);
//                        messages.text = "Tipp für Rätsel 8: ...";
//                        Debug.Log(riddle8solved);
//                    }

                    
//                    isDisplayingText = true;
//                }
//            }
//        }
//        if (isDisplayingText)
//        {
//            displayTimer += Time.deltaTime;
//            if (displayTimer >= displayDuration)
//            {
//                messages.gameObject.SetActive(false);
//                displayTimer = 0.0f;
//                isDisplayingText = false;
//            }
//        }
//    }
}
