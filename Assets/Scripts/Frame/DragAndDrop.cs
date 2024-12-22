using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public Camera riddle1Camera;
    private Vector3 mOffset;
    private bool isDragging = false;

    private void OnMouseDown()
    {
        mOffset = transform.position - GetMouseWorldPos();
        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;

    }

    void OnMouseDrag()
    {
        Vector3 newPosition = GetMouseWorldPos() + mOffset;

        newPosition.z = gameObject.transform.position.z;
        transform.position = newPosition;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = gameObject.transform.position.z;
        return riddle1Camera.ScreenToWorldPoint(mousePoint);
    }
}