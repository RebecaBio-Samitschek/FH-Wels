using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json;

public class SecondRiddle : MonoBehaviour
{
    public Text codeDisplayText;
    private string correctCode;
    public GameObject Passcode;
    public bool solved;
    public GameObject LockerOpen;
    public GameObject LockerClosed;
    private string currentCode = "";
    public Camera riddle2CameraName;

    private void Awake()
    {
        solved = false;
        LockerOpen.SetActive(false);
        LockerClosed.SetActive(true);
        codeDisplayText = GetComponentInChildren<Text>();

        // Inicia a busca dos dados do servidor
        StartCoroutine(FetchCorrectCode());
    }

    IEnumerator FetchCorrectCode()
    {
        string url = "http://localhost/unity/periodic_table.php?id1=6&id2=7&id3=8&id4=9&id5=10";
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

                // Tenta processar o JSON
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

            Debug.Log("Processando JSON: " + jsonResult);

            ResponseData response = JsonConvert.DeserializeObject<ResponseData>(jsonResult);

            if (response == null)
            {
                Debug.LogError("Falha ao desserializar JSON: response é nulo.");
                return;
            }

            if (response.answers == null)
            {
                Debug.LogError("Falha ao desserializar JSON: answers é nulo.");
                return;
            }

            Debug.Log("Resposta desserializada: " + string.Join(", ", response.answers));

            List<string> answers = new List<string>();

            // Adiciona os textos das respostas na lista
            for (int i = 6; i <= 10; i++)
            {
                string key = i.ToString();
                if (response.answers.ContainsKey(key))
                {
                    string answerText = response.answers[key];
                    // Considera apenas respostas não vazias
                    if (!string.IsNullOrEmpty(answerText) && answerText.Trim() != "")
                    {
                        answers.Add(answerText.Trim());
                    }
                }
            }

            // Constrói o correctCode
            correctCode = string.Join("-", answers);
            Debug.Log("Correct code set to: " + correctCode);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Erro ao processar JSON: " + e.Message);
        }
    }

    public void OnNumberButtonClicked(Button button)
    {
        int count = currentCode.Split('-').Length - 1;

        if (count < 3 && !solved && riddle2CameraName.enabled)
        {
            if (string.IsNullOrEmpty(currentCode))
            {
                currentCode += button.name;
            }
            else
            {
                currentCode += "-" + button.name;
            }
        }

        UpdateCodeDisplay();
    }

    public void OnOkButtonClicked()
    {

        Debug.Log("Botão OK foi clicado.");

        Debug.Log("Verificando códigos: currentCode = " + currentCode + ", correctCode = " + correctCode);

        // Comparação com case-insensitive
        if (string.Equals(currentCode, correctCode, System.StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log("Codes match. Setting solved to true.");
            solved = true;

            // Feedback visual de que foi resolvido
            LockerOpen.SetActive(true);
            LockerClosed.SetActive(false);
        }
        else
        {
            Debug.Log("Codes do not match. Not solved.");
        }
    }

    public void OnClearButtonClicked()
    {
        if (!solved)
        {
            currentCode = "";
            UpdateCodeDisplay();
        }
    }

    private void UpdateCodeDisplay()
    {
        codeDisplayText.text = currentCode;
    }

    [System.Serializable]
    public class ResponseData
    {
        public Dictionary<string, string> answers;
    }
}
