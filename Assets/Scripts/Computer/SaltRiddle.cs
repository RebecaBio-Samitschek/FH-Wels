using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SaltRiddle : MonoBehaviour
{
    public int rowNumber;
    public int buttonNumber;
    public bool isCorrectButton = false;

    private bool isHighlighted = false;
    public GameObject saltRiddleCanvas;
    public GameObject solvedCanvas;
    public GameObject spatula;

    public void OnButtonClick()
    {
        if (!isHighlighted)
        {
            DeselectAllButtonsInRow(rowNumber);

            HighlightButton();

            if (isCorrectButton)
            {
                CheckCorrectButtonsSelected();
            }
        }
    }

    private void HighlightButton()
    {
        GetComponent<UnityEngine.UI.Image>().color = Color.green;
        isHighlighted = true;
    }

    private void DeselectAllButtonsInRow(int row)
    {
        SaltRiddle[] allButtons = FindObjectsOfType<SaltRiddle>();

        foreach (SaltRiddle button in allButtons)
        {
            if (button.rowNumber == row)
            {
                button.DeselectButton();
            }
        }
    }

    private void DeselectButton()
    {
        GetComponent<UnityEngine.UI.Image>().color = Color.clear;
        isHighlighted = false;
    }

    private void CheckCorrectButtonsSelected()
    {
        SaltRiddle[] allButtons = FindObjectsOfType<SaltRiddle>();

        int correctButtonsSelected = 0;

        foreach (SaltRiddle button in allButtons)
        {
            if (button.isCorrectButton && button.isHighlighted)
            {
                correctButtonsSelected++;
            }
        }

        if (correctButtonsSelected == 4)
        {
            saltRiddleCanvas.SetActive(false);
            solvedCanvas.SetActive(true);
            spatula.SetActive(true);
        }
    }
}
