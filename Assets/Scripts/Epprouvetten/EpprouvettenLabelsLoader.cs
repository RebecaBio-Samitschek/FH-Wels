using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using Newtonsoft.Json;
using System.Linq;

public class EpprouvettenLabelsLoader : MonoBehaviour
{
    public List<TMP_Text> questionTextDisplays; // Referência para os textos das perguntas (parte superior)
    public List<TMP_Text> answerTextDisplays; // Referência para os textos das respostas (parte inferior)
    private List<Renderer> tubeRenderers; // Referência para os renderizadores dos tubos
    public List<string> colors; // Lista de cores

    void Start()
    {
        tubeRenderers = FindObjectsOfType<Renderer>().ToList(); // Encontra todos os renderizadores de tubos
        StartCoroutine(GetTextsFromDatabase());
        StartCoroutine(GetColorsFromDatabase()); // Carregar as cores
    }

    // Carregar textos das perguntas e respostas
    private IEnumerator GetTextsFromDatabase()
    {
        string questionIds = "15,16,17,18,19";
        string answerIds = "29,30,31,32,33,34,35,36,37,38";
        string url = $"http://localhost/unity/epprouvetten_text.php?question_ids={questionIds}&answer_ids={answerIds}";

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            var jsonResult = www.downloadHandler.text;
            Debug.Log("Resposta do servidor: " + jsonResult);

            TextsResponse jsonData = JsonConvert.DeserializeObject<TextsResponse>(jsonResult);

            if (jsonData == null || jsonData.questions == null || jsonData.answers == null)
            {
                Debug.LogError("Falha ao parsear a resposta JSON ou dados nulos.");
                yield break;
            }

            // Embaralhar as respostas
            var shuffledAnswers = jsonData.answers.Values.ToList();
            shuffledAnswers = shuffledAnswers.OrderBy(a => Random.value).ToList(); // Embaralha as respostas

            // Atualizar os textos das perguntas (parte superior)
            for (int i = 0; i < questionTextDisplays.Count; i++)
            {
                int questionId = 15 + i;
                if (jsonData.questions.ContainsKey(questionId.ToString()))
                {
                    questionTextDisplays[i].text = jsonData.questions[questionId.ToString()];
                }
            }

            // Atualizar os textos das respostas (parte inferior) com respostas embaralhadas
            for (int i = 0; i < answerTextDisplays.Count; i++)
            {
                answerTextDisplays[i].text = shuffledAnswers[i];
            }
        }
    }

    // Carregar cores (sem embaralhar)
    private IEnumerator GetColorsFromDatabase()
    {
        string colorIds = "39,40,41,42,43"; // IDs das cores no banco de dados
        string url = $"http://localhost/unity/epprouvetten_answers.php?answer_ids={colorIds}";

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            var jsonResult = www.downloadHandler.text;
            Debug.Log("Resposta do servidor (cores): " + jsonResult);

            TextsResponse jsonData = JsonConvert.DeserializeObject<TextsResponse>(jsonResult);

            if (jsonData == null || jsonData.answers == null)
            {
                Debug.LogError("Falha ao parsear a resposta JSON ou dados nulos.");
                yield break;
            }

            // Carregar as cores na ordem que vêm do banco de dados (sem embaralhar)
            colors = jsonData.answers.Values.ToList();

            // Verificar se as cores estão corretas
            for (int i = 0; i < colors.Count; i++)
            {
                Debug.Log($"Cor {i + 1}: {colors[i]}");
            }
        }
    }

    public class TextsResponse
    {
        public Dictionary<string, string> questions { get; set; }
        public Dictionary<string, string> answers { get; set; }
    }
}
