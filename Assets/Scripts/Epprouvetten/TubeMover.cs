using UnityEngine;
using TMPro;

public class TubeMover : MonoBehaviour
{
    public GameObject correctSlot;
    private Vector3 initialPosition;
    public Camera mainCamera;
    private Vector3 offset;
    private float snapDistance = 20.0f;
    public bool isPlacedCorrectly = false;
    private string tubeText;
    private AnswerLoader answerLoader;

    void Start()
    {
        initialPosition = transform.position;
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // Obter o texto associado ao tubo (TextMeshPro)
        TMP_Text textComponent = GetComponentInChildren<TMP_Text>();
        if (textComponent != null)
        {
            tubeText = textComponent.text.ToLower();
        }

        // Acessar o script AnswerLoader para saber se os textos já estão carregados
        answerLoader = FindObjectOfType<AnswerLoader>();
    }

    void OnMouseDown()
    {
        if (!answerLoader.AreTextsLoaded())
        {
            Debug.LogWarning("Textos ainda não carregados, por favor, aguarde!");
            return;
        }

        offset = transform.position - GetMouseWorldPosition();
    }

    void OnMouseDrag()
    {
        if (!answerLoader.AreTextsLoaded()) return;
        transform.position = GetMouseWorldPosition() + offset;
    }

    void OnMouseUp()
    {
        if (!answerLoader.AreTextsLoaded()) return;

        SlotChecker[] allSlots = FindObjectsOfType<SlotChecker>();
        foreach (SlotChecker slot in allSlots)
        {
            Vector3 slotPosition = slot.transform.position;
            float distance = Vector3.Distance(transform.position, slotPosition);

            if (distance <= snapDistance)
            {
                TMP_Text textComponent = GetComponentInChildren<TMP_Text>();
                tubeText = textComponent.text.ToLower(); // Atualizar o valor de tubeText após o carregamento
                Debug.Log($"Comparando: '{tubeText}' com '{slot.correctAnswerText}'");

                // Comparar o texto do tubo com o texto correto do slot
                if (slot.correctAnswerText == tubeText)
                {
                    transform.position = slotPosition;
                    isPlacedCorrectly = true;

                    // Atualiza o estado do slot como correto
                    slot.CheckSlotCorrectness(tubeText);

                    Debug.Log($"{gameObject.name} colocado corretamente no slot {slot.name}");
                    return;
                }
                else
                {
                    Debug.LogWarning("Texto do tubo não corresponde à resposta correta.");
                }
            }
        }

        transform.position = initialPosition;
        Debug.Log($"{gameObject.name} retornou à posição inicial.");
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mainCamera.WorldToScreenPoint(transform.position).z;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }
}
