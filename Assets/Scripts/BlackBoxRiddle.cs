using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using Unity.VisualScripting;
using UnityEngine.UI;

public class BlackBoxRiddle : MonoBehaviour
{
    public GameObject correctSpot;
    public GameObject flask;
    public bool isCorrectSpot = false;
    public Camera riddle10Camera;
    private Vector3 mOffset;
    private float mZCoord;
    private float snapDistance = 10f;
    public float distance;

    private void OnMouseDown()
    {
        Debug.Log("dragging");
        mZCoord = riddle10Camera.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = GetMouseWorldPos() + mOffset;
        newPosition.z = gameObject.transform.position.z;
        transform.position = newPosition;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return riddle10Camera.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseUp()
    {
        Vector3 currentPos = flask.transform.position;
        Vector3 correctPos = correctSpot.transform.position;
        distance = Vector3.Distance(currentPos, correctPos);
        if (distance <= snapDistance)
        {
            isCorrectSpot = true;
            Debug.Log("Dropped in the correct position!");
            Debug.Log(distance);
            flask.SetActive(false);
        }
    }

    /*
    public void OnSubmitClicked()
    {
        if (distance <= snapDistance)
        {
            isCorrectSpot = true;
            Debug.Log("Dropped in the correct position!");
            Debug.Log(distance);
            flask.SetActive(false);
        } else {
            Debug.Log("false");
        }
    }
    */
}
