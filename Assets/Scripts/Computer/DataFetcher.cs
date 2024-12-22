using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class DataFetcher : MonoBehaviour
{
    public string url = "http://localhost/unity/computer.php"; // URL do PHP

    public TMP_Text[] questionTexts;  // Campos de texto para as perguntas
    public TMP_Text[] answerButtons;  // Campos de texto ou botões para as respostas

    void Start()
    {
        StartCoroutine(FetchData());
    }

    IEnumerator FetchData()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            // Parse do JSON recebido
            string jsonResponse = request.downloadHandler.text;
            DataResponse dataResponse = JsonUtility.FromJson<DataResponse>(jsonResponse);

            // Processa perguntas
            for (int i = 0; i < dataResponse.questions.Length && i < questionTexts.Length; i++)
            {
                questionTexts[i].text = dataResponse.questions[i].QuestionText;
            }

            // Processa respostas
            for (int i = 0; i < dataResponse.answers.Length && i < answerButtons.Length; i++)
            {
                answerButtons[i].text = dataResponse.answers[i].AnswerText;
            }
        }
    }
}

// Renomeamos a classe Question para QuestionData
[System.Serializable]
public class QuestionData
{
    public int QuestionID;
    public string QuestionText;
}

// Renomeamos a classe Answer para AnswerData
[System.Serializable]
public class AnswerData
{
    public int AnswerID;
    public string AnswerText;
}

// Renomeamos a classe que contém o JSON de resposta para DataResponse
[System.Serializable]
public class DataResponse
{
    public QuestionData[] questions;
    public AnswerData[] answers;
}
