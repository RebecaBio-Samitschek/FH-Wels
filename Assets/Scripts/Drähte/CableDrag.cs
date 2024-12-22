using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CableDrag : MonoBehaviour
{
    public GameObject cableMiddle;
    public GameObject otherEnd;
    private Vector3 mOffset;
    private float mZCoord;
    public Camera cableCamera;
    public bool isRightCableEnd;
    private bool isDragging = false;

    public bool isCorrectSpot = false;
    private Quaternion initialRotation;
    private Quaternion rotatedState;

    private void Start()
    {
        UpdateCableMiddle();
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        UpdateCableMiddle();
    }
    

    void OnMouseDown()
    {
        isDragging = true;
        mZCoord = cableCamera.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();

        if (transform.rotation.y != 0)
        {
            transform.Rotate(0, 0, 0);
        }

        /*int rotationDirection = isRightCableEnd ? 90 : -90;
        transform.Rotate(0, rotationDirection, 0, Space.Self);
        rotatedState = transform.rotation;
        */
    }


    void OnMouseDrag()
    {
        Vector3 newPosition = GetMouseWorldPos() + mOffset;

        newPosition.x = gameObject.transform.position.x;
        transform.position = newPosition;

    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return cableCamera.ScreenToWorldPoint(mousePoint);
    }
    public bool IsDragging
    {
        get { return isDragging; }
    }
    public void StopDragging()
    {
        isDragging = false;
        transform.rotation = initialRotation; // reset rotation
    }

    public void StayInRotatedState()
    {
        isDragging = false;
        transform.rotation = rotatedState; // stay in rotated state
    }

    void OnMouseUp()
    {
        if (!isDragging)
            return;

        isDragging = false;
        StopDragging();
    }


    void UpdateCableMiddle()
    {
        Vector3 direction = otherEnd.transform.position - gameObject.transform.position;
        Vector3 middlePos = gameObject.transform.position + direction / 2;

        cableMiddle.transform.SetPositionAndRotation(middlePos, Quaternion.LookRotation(direction));
        cableMiddle.transform.Rotate(0, 90, 0, Space.Self);

        float scaleX = direction.magnitude / 50;
        cableMiddle.transform.localScale = new Vector3(scaleX, cableMiddle.transform.localScale.y, cableMiddle.transform.localScale.z);
    }
}
