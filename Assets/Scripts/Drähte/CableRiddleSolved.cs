using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableRiddleSolved : MonoBehaviour
{
    public GameObject plug1;
    public GameObject plug2;
    public GameObject plug3;
    public GameObject plug4;
    public GameObject plug5;
    public GameObject yellowPlug;
    public GameObject bluePlug;
    public GameObject redPlug;
    public GameObject whitePlug;
    public GameObject greenPlug;

    public GameObject yellowCableLeft;
    public GameObject yellowCableRight;
    public GameObject blueCableLeft;
    public GameObject blueCableRight;
    public GameObject redCableLeft;
    public GameObject redCableRight;
    public GameObject whiteCableLeft;
    public GameObject whiteCableRight;
    public GameObject greenCableLeft;
    public GameObject greenCableRight;
    public GameObject glassCabinetDoor;
    public GameObject riddleDoor;

    private float snapDistance = 10f;


    CableDrag cableDrag;
    private int[] plugNumber;

    public void Update()
    {
        Vector3 currentPos1 = yellowCableLeft.transform.position; 
        Vector3 currentPos2 = yellowCableRight.transform.position;
        Vector3 correctPos1 = yellowPlug.transform.position;
        Vector3 correctPos2 = plug3.transform.position;

        Vector3 currentPos3 = greenCableLeft.transform.position;
        Vector3 currentPos4 = greenCableRight.transform.position;
        Vector3 correctPos3 = greenPlug.transform.position;
        Vector3 correctPos4 = plug2.transform.position;

        Vector3 currentPos5 = redCableLeft.transform.position;
        Vector3 currentPos6 = redCableRight.transform.position;
        Vector3 correctPos5 = redPlug.transform.position;
        Vector3 correctPos6 = plug5.transform.position;

        Vector3 currentPos7 = blueCableLeft.transform.position;
        Vector3 currentPos8 = blueCableRight.transform.position;
        Vector3 correctPos7 = bluePlug.transform.position;
        Vector3 correctPos8 = plug1.transform.position;

        Vector3 currentPos9 = whiteCableLeft.transform.position;
        Vector3 currentPos10 = whiteCableRight.transform.position;
        Vector3 correctPos9 = whitePlug.transform.position;
        Vector3 correctPos10 = plug4.transform.position;

        float distance1 = Vector3.Distance(currentPos1, correctPos1);
        float distance2 = Vector3.Distance(currentPos2, correctPos2);
        float distance3 = Vector3.Distance(currentPos3, correctPos3);
        float distance4 = Vector3.Distance(currentPos4, correctPos4);
        float distance5 = Vector3.Distance(currentPos5, correctPos5);
        float distance6 = Vector3.Distance(currentPos6, correctPos6);
        float distance7 = Vector3.Distance(currentPos7, correctPos7);
        float distance8 = Vector3.Distance(currentPos8, correctPos8);
        float distance9 = Vector3.Distance(currentPos9, correctPos9);
        float distance10 = Vector3.Distance(currentPos10, correctPos10);

        float distance11 = Vector3.Distance(currentPos1, correctPos2);
        float distance22 = Vector3.Distance(currentPos2, correctPos1);
        float distance33 = Vector3.Distance(currentPos3, correctPos4);
        float distance44 = Vector3.Distance(currentPos4, correctPos3);
        float distance55 = Vector3.Distance(currentPos5, correctPos6);
        float distance66 = Vector3.Distance(currentPos6, correctPos5);
        float distance77 = Vector3.Distance(currentPos7, correctPos8);
        float distance88 = Vector3.Distance(currentPos8, correctPos7);
        float distance99 = Vector3.Distance(currentPos9, correctPos10);
        float distance100 = Vector3.Distance(currentPos10, correctPos9);

        if ((distance1 <= snapDistance || distance11 <= snapDistance) &&
        (distance2 <= snapDistance || distance22 <= snapDistance) &&
       (distance3 <= snapDistance || distance33 <= snapDistance) &&
        (distance4 <= snapDistance || distance44 <= snapDistance) &&
        (distance5 <= snapDistance || distance55 <= snapDistance) &&
        (distance6 <= snapDistance || distance66 <= snapDistance) &&
        (distance7 <= snapDistance || distance77 <= snapDistance) &&
        (distance8 <= snapDistance || distance88 <= snapDistance) &&
        (distance9 <= snapDistance || distance99 <= snapDistance) &&
        (distance10 <= snapDistance || distance100 <= snapDistance)
        )
        {
            Debug.Log("Riddle solved");
            glassCabinetDoor.SetActive(false);
            riddleDoor.SetActive(true);
            FindObjectOfType<DoorOpenEvent>().PlayDoorEvent();
        }
    }
}

