using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FirstRiddleSolved : MonoBehaviour
{
    public bool solved;

    private float snapDistance = 10f;

    public GameObject correctSpot1;
    public GameObject image1;

    public GameObject correctSpot2;
    public GameObject image2;

    public GameObject correctSpot3;
    public GameObject image3;

    public GameObject correctSpot4;
    public GameObject image4;

    public GameObject correctSpot5;
    public GameObject image5;

    public GameObject newImages;
    public GameObject correct;

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPos1 = image1.transform.position;
        Vector3 correctPos1 = correctSpot1.transform.position;

        Vector3 currentPos2 = image2.transform.position;
        Vector3 correctPos2 = correctSpot2.transform.position;

        Vector3 currentPos3 = image3.transform.position;
        Vector3 correctPos3 = correctSpot3.transform.position;

        Vector3 currentPos4 = image4.transform.position;
        Vector3 correctPos4 = correctSpot4.transform.position;

        Vector3 currentPos5 = image5.transform.position;
        Vector3 correctPos5 = correctSpot5.transform.position;


        float distance1 = Vector3.Distance(currentPos1, correctPos1);
        float distance2 = Vector3.Distance(currentPos2, correctPos2);
        float distance3 = Vector3.Distance(currentPos3, correctPos3);
        float distance4 = Vector3.Distance(currentPos4, correctPos4);
        float distance5 = Vector3.Distance(currentPos5, correctPos5);

        if (distance1 <= snapDistance &&
        distance2 <= snapDistance &&
        distance3 <= snapDistance &&
        distance4 <= snapDistance &&
        distance5 <= snapDistance
        )
        {
            newImages.SetActive(true);
            image1.SetActive(false);
            image2.SetActive(false);
            image3.SetActive(false);
            image4.SetActive(false);
            image5.SetActive(false);
            solved = true;

        }
    }
}