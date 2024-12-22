using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class LoadTextTabletFromDatabase : MonoBehaviour
{
    public TextMeshPro textComponent; // Para textos em 3D

    void Start()
    {
        StartCoroutine(GetTextFromDatabase());
    }

    IEnumerator GetTextFromDatabase()
    {
        string url = "http://localhost/unity/tablet_text.php?id=12";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            DatabaseQuestionData questionData = JsonUtility.FromJson<DatabaseQuestionData>(json);

            if (questionData != null && questionData.question != null)
            {
                textComponent.text = questionData.question.QuestionText;
            }
            else if (questionData != null && questionData.error != null)
            {
                Debug.LogError("Erro: " + questionData.error);
            }
        }
        else
        {
            Debug.LogError("Erro na requisição: " + request.error);
        }
    }
}

[System.Serializable]
public class DatabaseQuestionData
{
    public DatabaseQuestion question;
    public string error;
}

[System.Serializable]
public class DatabaseQuestion
{
    public int QuestionID;
    public string QuestionText;
}
