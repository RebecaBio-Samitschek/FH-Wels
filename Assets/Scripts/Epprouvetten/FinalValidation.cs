using UnityEngine;
using System.Collections.Generic;

public class FinalValidation : MonoBehaviour
{
    public List<SlotChecker> slotCheckers; // Refer�ncia para os verificadores de slots
    private EpprouvettenLabelsLoader labelsLoader; // Refer�ncia ao EpprouvettenLabelsLoader

    void Start()
    {
        labelsLoader = FindObjectOfType<EpprouvettenLabelsLoader>(); // Encontra o EpprouvettenLabelsLoader
    }

    void Update()
    {
        if (AreAllSlotsCorrect()) // Verifica se todos os tubos est�o nos slots corretos
        {
            ApplyColorsToTubes(); // Aplica as cores aos tubos corretamente posicionados
        }
    }

    private bool AreAllSlotsCorrect()
    {
        foreach (SlotChecker slot in slotCheckers)
        {
            if (!slot.isSlotCorrect) // Se algum slot n�o estiver correto, retorna falso
            {
                return false;
            }
        }
        return true; // Todos os slots est�o corretos
    }

    private void ApplyColorsToTubes()
    {
        if (labelsLoader.colors == null || labelsLoader.colors.Count == 0)
        {
            Debug.LogError("Cores n�o foram carregadas.");
            return;
        }

        // Aplicar as cores aos tubos corretamente posicionados, na ordem das cores carregadas
        for (int i = 0; i < slotCheckers.Count; i++)
        {
            if (slotCheckers[i].isSlotCorrect)
            {
                Renderer tubeRenderer = slotCheckers[i].GetComponent<Renderer>();
                if (tubeRenderer != null)
                {
                    Color colorToApply;
                    if (ColorUtility.TryParseHtmlString(labelsLoader.colors[i], out colorToApply))
                    {
                        tubeRenderer.material.color = colorToApply; // Aplica a cor do banco de dados
                    }
                    else
                    {
                        Debug.LogWarning($"Cor inv�lida: {labelsLoader.colors[i]}");
                    }
                }
            }
        }
    }
}
