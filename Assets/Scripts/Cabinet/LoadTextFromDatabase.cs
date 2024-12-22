using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections.Generic;

public class LoadTextFromDatabase : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // Referência ao componente TMP
    private string url = "http://localhost/unity/cabinet.php?id=11"; 

    void Start()
    {
        StartCoroutine(GetTextFromDatabase());
    }

    IEnumerator GetTextFromDatabase()
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Erro ao acessar o banco de dados: " + www.error);
        }
        else
        {
            // Parse do JSON recebido
            var jsonResponse = JsonUtility.FromJson<ServerResponse>(www.downloadHandler.text);

            if (jsonResponse != null && jsonResponse.question != null)
            {
                // Atualiza o texto do TMP
                textComponent.text = jsonResponse.question.QuestionText;
            }
            else
            {
                Debug.LogWarning("Texto não encontrado para o ID especificado.");
            }
        }
    }
}

// Classe para deserializar a resposta JSON
[System.Serializable]
public class ServerResponse
{
    public Question question;
}

[System.Serializable]
public class Question
{
    public int QuestionID;
    public string QuestionText;
}
