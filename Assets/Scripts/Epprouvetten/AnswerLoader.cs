using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections;

public class AnswerLoader : MonoBehaviour
{
    public Dictionary<int, string> correctAnswers = new Dictionary<int, string>();
    private bool textsLoaded = false; // Variável de controle para indicar se os textos foram carregados

    void Start()
    {
        StartCoroutine(GetAnswersFromDatabase());
    }

    private IEnumerator GetAnswersFromDatabase()
    {
        string answerIds = "29,30,31,32,33"; // IDs das respostas
        string url = $"http://localhost/unity/epprouvetten_answers.php?answer_ids={answerIds}";

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Erro na solicitação da Web: " + www.error);
        }
        else
        {
            var jsonResult = www.downloadHandler.text;
            if (string.IsNullOrEmpty(jsonResult))
            {
                Debug.LogError("Resposta do servidor é nula ou vazia.");
                yield break;
            }

            Debug.Log("Resposta do servidor: " + jsonResult);

            try
            {
                var jsonData = JsonConvert.DeserializeObject<AnswerResponse>(jsonResult);
                if (jsonData == null || jsonData.answers == null)
                {
                    Debug.LogError("Falha ao desserializar a resposta ou resposta vazia.");
                    yield break;
                }

                foreach (var pair in jsonData.answers)
                {
                    int id = int.Parse(pair.Key);
                    string text = pair.Value.ToLower();
                    correctAnswers[id] = text;

                    // Atribuir o texto correto ao slot
                    GameObject slot = GameObject.Find($"Slot{id}");
                    if (slot != null)
                    {
                        SlotChecker slotChecker = slot.GetComponent<SlotChecker>();
                        if (slotChecker != null)
                        {
                            slotChecker.correctAnswerText = text;
                            Debug.Log($"Texto da resposta correta para {slot.name}: {text}");
                        }
                        else
                        {
                            Debug.LogWarning($"SlotChecker não encontrado no {slot.name}");
                        }
                    }
                    else
                    {
                        Debug.LogWarning($"Slot com ID {id} não encontrado.");
                    }
                }

                textsLoaded = true; // Marca que os textos foram carregados
            }
            catch (JsonException ex)
            {
                Debug.LogError("Erro ao desserializar JSON: " + ex.Message);
            }
        }
    }

    public class AnswerResponse
    {
        [JsonProperty("answers")]
        public Dictionary<string, string> answers { get; set; }
    }

    // Método para verificar se os textos foram carregados
    public bool AreTextsLoaded()
    {
        return textsLoaded;
    }
}
