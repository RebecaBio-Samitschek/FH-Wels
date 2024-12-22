using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;

public class CodeValidator : MonoBehaviour
{
    public Text codeDisplayText;
    public Button validateButton;
    public GameObject correctImage;  // Imagem de acerto
    public GameObject wrongImage;    // Imagem de erro (novo)

    private string correctCode = ""; // Inicialmente vazio, será preenchido com o valor do banco de dados.

    void Start()
    {
        // "Correct picture" and "wrong picture" are hidden initially
        correctImage.gameObject.SetActive(false);
        if (wrongImage != null)
        {
            wrongImage.gameObject.SetActive(false);
        }

        // Add listener to the button to validate the code when pressed
        validateButton.onClick.AddListener(ValidateCode);

        // Start fetching the correct code from the database
        StartCoroutine(FetchCorrectCodeFromDatabase());
    }

    IEnumerator FetchCorrectCodeFromDatabase()
    {
        string url = "http://localhost/unity/periodic_table.php?id1=6&id2=7&id3=8&id4=9&id5=10"; // Ajuste o URL conforme necessário
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Erro na requisição: " + webRequest.error);
            }
            else
            {
                // Exibe o resultado bruto da resposta para depuração
                string jsonResult = webRequest.downloadHandler.text;
                Debug.Log("Resposta JSON recebida: " + jsonResult);

                // Processa o JSON para obter o código correto
                ProcessJsonResult(jsonResult);
            }
        }
    }

    void ProcessJsonResult(string jsonResult)
    {
        try
        {
            if (string.IsNullOrEmpty(jsonResult))
            {
                Debug.LogError("Resposta JSON está vazia ou nula.");
                return;
            }

            ResponseData response = JsonConvert.DeserializeObject<ResponseData>(jsonResult);

            if (response == null || response.answers == null)
            {
                Debug.LogError("Falha ao desserializar o JSON.");
                return;
            }

            // Constrói o código correto a partir das respostas
            List<string> answers = new List<string>();
            for (int i = 6; i <= 10; i++)
            {
                string key = i.ToString();
                if (response.answers.ContainsKey(key))
                {
                    string answerText = response.answers[key];
                    if (!string.IsNullOrEmpty(answerText) && answerText.Trim() != "")
                    {
                        answers.Add(answerText.Trim());
                    }
                }
            }

            correctCode = string.Join("-", answers); // Atualiza o código correto
            Debug.Log("Correct code set to: " + correctCode);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Erro ao processar JSON: " + e.Message);
        }
    }

    void ValidateCode()
    {
        // Comparação do código inserido com o código correto
        if (string.Equals(codeDisplayText.text, correctCode, System.StringComparison.OrdinalIgnoreCase))
        {
            // Mostra a imagem "correto" se o código estiver correto
            correctImage.gameObject.SetActive(true);

            // Oculta a imagem de erro, se aplicável
            if (wrongImage != null)
            {
                wrongImage.gameObject.SetActive(false);
            }

            // Inicia a coroutine para esconder a imagem após 1 segundos
            StartCoroutine(HideCorrectImageAfterDelay(1f));
        }
        else
        {
            // Mostra a imagem "errado" se o código estiver incorreto, se configurada
            if (wrongImage != null)
            {
                wrongImage.gameObject.SetActive(true);
            }

            // Oculta a imagem de acerto, se aplicável
            correctImage.gameObject.SetActive(false);

            // Inicia a coroutine para esconder a imagem de erro após 1 segundos
            if (wrongImage != null)
            {
                StartCoroutine(HideWrongImageAfterDelay(1f));
            }
        }
    }

    IEnumerator HideCorrectImageAfterDelay(float delay)
    {
        // Espera o tempo especificado (em segundos)
        yield return new WaitForSeconds(delay);

        // Oculta a imagem "correto"
        correctImage.gameObject.SetActive(false);
    }

    IEnumerator HideWrongImageAfterDelay(float delay)
    {
        // Espera o tempo especificado (em segundos)
        yield return new WaitForSeconds(delay);

        // Oculta a imagem "errado"
        wrongImage.gameObject.SetActive(false);
    }

    [System.Serializable]
    public class ResponseData
    {
        public Dictionary<string, string> answers;
    }
}
