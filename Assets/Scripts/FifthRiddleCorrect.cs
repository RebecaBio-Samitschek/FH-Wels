using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FifthRiddleCorrect : MonoBehaviour
{
    public List<FifthRiddle> flaskScripts; // Lista dos scripts FifthRiddle associados a cada tubo
    public Dictionary<int, string> correctMappings; // Mapeamento dos IDs corretos (ou nomes)

    private void Start()
    {
        correctMappings = new Dictionary<int, string>
        {
            { 15, "HNO₃" }, // Salpetersäure deve ser HNO₃
            { 16, "H₃PO₄" }, // Phosphorsäure deve ser H₃PO₄
            { 17, "HCN" },   // Blausäure deve ser HCN
            { 18, "H₂SO₄" }, // Schwefelsäure deve ser H₂SO₄
            { 19, "H₂CO₃" }  // Kohlensäure deve ser H₂CO₃
        };
    }

    private void Update()
    {
        CheckAllFlasks();
    }

    void CheckAllFlasks()
    {
        bool allCorrect = true;

        foreach (var flaskScript in flaskScripts)
        {
            TMP_Text flaskText = flaskScript.flask.GetComponentInChildren<TMP_Text>();

            if (flaskScript.isCorrectSpot && flaskText != null)
            {
                int slotId = GetSlotId(flaskScript.correctSpot.name); // Obtenha o ID do slot a partir do nome ou outro identificador
                if (correctMappings[slotId] == flaskText.text)
                {
                    // Tudo está correto para este tubo
                    continue;
                }
            }
            allCorrect = false; // Se qualquer um estiver incorreto, definimos como falso
        }

        if (allCorrect)
        {
            Debug.Log("Todos os tubos estão na posição correta!");
            // Execute alguma ação, como abrir uma porta ou mostrar um feedback positivo
        }
    }

    private int GetSlotId(string slotName)
    {
        // Implemente a lógica para extrair o ID do slot a partir do nome do slot
        // Por exemplo, se o slot for "Slot15", você pode retornar 15
        return int.Parse(slotName.Replace("Slot", ""));
    }
}
