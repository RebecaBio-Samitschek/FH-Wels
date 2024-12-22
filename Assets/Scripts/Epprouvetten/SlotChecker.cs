using UnityEngine;

public class SlotChecker : MonoBehaviour
{
    public string correctAnswerText; // Texto correto atribuído pelo AnswerLoader
    public bool isSlotCorrect = false; // Status do slot, se está correto ou não

    public void CheckSlotCorrectness(string tubeText)
    {
        if (tubeText == correctAnswerText)
        {
            isSlotCorrect = true;
            Debug.Log($"{gameObject.name} está correto com o texto {tubeText}");
        }
        else
        {
            isSlotCorrect = false;
        }
    }
}
