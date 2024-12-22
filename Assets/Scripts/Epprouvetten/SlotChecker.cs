using UnityEngine;

public class SlotChecker : MonoBehaviour
{
    public string correctAnswerText; // Texto correto atribu�do pelo AnswerLoader
    public bool isSlotCorrect = false; // Status do slot, se est� correto ou n�o

    public void CheckSlotCorrectness(string tubeText)
    {
        if (tubeText == correctAnswerText)
        {
            isSlotCorrect = true;
            Debug.Log($"{gameObject.name} est� correto com o texto {tubeText}");
        }
        else
        {
            isSlotCorrect = false;
        }
    }
}
