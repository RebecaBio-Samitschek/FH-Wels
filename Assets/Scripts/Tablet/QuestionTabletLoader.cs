using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class QuestionLoader : MonoBehaviour
{
    public TMP_Text questionTextDisplay; // Referência ao componente TextMeshPro para exibir o texto da pergunta
    public TMP_Text mlAnzeigeTextDisplay; // Referência ao componente TextMeshPro para exibir o valor no objeto mlAnzeige

    void Start()
    {
        StartCoroutine(GetQuestionTextFromDatabase(13, questionTextDisplay)); // Busca a pergunta com ID 13
        StartCoroutine(GetQuestionTextFromDatabase(14, mlAnzeigeTextDisplay)); // Busca o texto para mlAnzeige com ID 14
    }

    private IEnumerator GetQuestionTextFromDatabase(int id, TMP_Text display)
    {
        string url = "http://localhost/unity/tablet_text.php?id=" + id;
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Parse JSON result
            var jsonResult = www.downloadHandler.text;
            var jsonData = JsonUtility.FromJson<QuestionResponse>(jsonResult);
            if (jsonData != null && jsonData.question != null)
            {
                string text = jsonData.question.QuestionText;
                UpdateText(display, text);

                if (id == 14) // ID 14 é o que corresponde a mlAnzeigeTextDisplay
                {
                    Debug.Log("Texto recuperado para mlAnzeige: " + text);
                    // Tenta converter o texto para um valor float
                    if (float.TryParse(text, out float result))
                    {
                        // Atualiza o TextMeshPro com o valor e passa o valor para SyringeRiddle4
                        mlAnzeigeTextDisplay.text = text;

                        Debug.Log("Valor recuperado para mlAnzeige: " + result);

                        SyringeRiddle4 syringeRiddle4 = FindObjectOfType<SyringeRiddle4>();
                        if (syringeRiddle4 != null)
                        {
                            syringeRiddle4.SetStartingPoint(result);
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Failed to parse mlAnzeigeTextDisplay value to float.");
                    }
                }
            }
            else
            {
                Debug.Log("Failed to parse JSON response.");
            }
        }
    }

    private void UpdateText(TMP_Text display, string text)
    {
        if (display != null)
        {
            display.text = text;
        }
    }

    [System.Serializable]
    public class QuestionResponse
    {
        public Question question;
    }

    [System.Serializable]
    public class Question
    {
        public int QuestionID;
        public string QuestionText;
    }
}
