using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro; // Certifique-se de incluir esta linha para usar TextMeshPro

public class CableTextManager : MonoBehaviour
{
    private string url = "http://localhost/unity/dr%c3%a4hte.php"; // Substitua pela URL real do seu script PHP

    // Referências para os componentes TextMeshProUGUI no Unity
    public TextMeshProUGUI questionTMP;
    public TextMeshProUGUI answerTMP;

    void Start()
    {
        StartCoroutine(GetTextFromDB());
    }

    IEnumerator GetTextFromDB()
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.error);
        }
        else
        {
            ProcessResponse(www.downloadHandler.text);
        }
    }

    void ProcessResponse(string jsonResponse)
    {
        // Desserializa o JSON e atualiza os componentes TMP com os textos
        var data = JsonUtility.FromJson<ResponseData>(jsonResponse);
        questionTMP.text = data.questionText;
        answerTMP.text = data.answerText;

        Debug.Log("Question: " + data.questionText);
        Debug.Log("Answer: " + data.answerText);
    }

    [System.Serializable]
    public class ResponseData
    {
        public string questionText;
        public string answerText;
    }
}
