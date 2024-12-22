using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour
{
    public GameObject correctSpot;
    public GameObject image;
    public bool isCorrectSpot = false;
    public Camera riddle1Camera;
    private Vector3 mOffset;
    private float mZCoord;
    private float snapDistance = 5f;

    private void OnMouseDown()
    {
        Debug.Log("dragging");
        mZCoord = riddle1Camera.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = GetMouseWorldPos() + mOffset;
        newPosition.x = gameObject.transform.position.x;
        transform.position = newPosition;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return riddle1Camera.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseUp()
    {
        Vector3 currentPos = image.transform.position;
        Vector3 correctPos = correctSpot.transform.position;
        float distance = Vector3.Distance(currentPos, correctPos);

        if (distance <= snapDistance)
        {
            isCorrectSpot = true;
            Debug.Log("Dropped in the correct position!");        
        }
    }

}
