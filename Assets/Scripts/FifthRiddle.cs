using UnityEngine;

public class FifthRiddle : MonoBehaviour
{
    public GameObject correctSpot;
    public GameObject flask;
    public bool isCorrectSpot = false;
    public Camera secretShelveCamera;
    private Vector3 mOffset;
    private float mZCoord;
    private float snapDistance = 50f;

    private void OnMouseDown()
    {
        Debug.Log("Dragging " + flask.name);
        mZCoord = secretShelveCamera.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = GetMouseWorldPos() + mOffset;
        newPosition.x = gameObject.transform.position.x; // Mantém a posição no eixo X
        transform.position = newPosition;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return secretShelveCamera.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseUp()
    {
        Vector3 currentPos = flask.transform.position;
        Vector3 correctPos = correctSpot.transform.position;
        float distance = Vector3.Distance(currentPos, correctPos);

        if (distance <= snapDistance)
        {
            isCorrectSpot = true;
            flask.transform.position = correctSpot.transform.position; // Snap to position
            Debug.Log(flask.name + " dropped in the correct position!");
        }
        else
        {
            isCorrectSpot = false;
            Debug.Log(flask.name + " is not in the correct position.");
        }
    }
}
