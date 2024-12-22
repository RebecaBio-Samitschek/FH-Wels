using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ColorCode : MonoBehaviour
{
    public GameObject Colorcode;
    public GameObject SaltRiddle;
    public GameObject wrongCode;

    private string[] correctCode;
    private string[] currentCode;

    public CameraSwitcher index;
    public bool solved = false;

    // URL do PHP que retorna a sequência de cores
    public string url = "http://localhost/unity/colorcode.php";

    void Start()
    {
        StartCoroutine(FetchColorCode());
        currentCode = new string[5];  // Assume que serão sempre 5 cores
        index.inputIndex = 0;
        wrongCode.SetActive(false);
    }

    // Função chamada ao clicar no botão
    public void OnButtonClick(Button button)
    {
        if (index.inputIndex < correctCode.Length)
        {
            currentCode[index.inputIndex] = button.name;
            index.inputIndex++;
        }

        if (index.inputIndex == correctCode.Length)
        {
            solved = true;
            for (int i = 0; i < correctCode.Length; i++)
            {
                if (currentCode[i] != correctCode[i])
                {
                    solved = false;
                    break;
                }
            }

            if (solved)
            {
                Debug.Log("Correct code entered!");
                Colorcode.SetActive(false);
                SaltRiddle.SetActive(true);
            }
            else
            {
                currentCode = new string[correctCode.Length];
                index.inputIndex = 0;
                Debug.Log("Incorrect code. Try again!");
                Invoke("ActivateObject", 0f);
            }
        }
    }

    void ActivateObject()
    {
        // Ativa o objeto que indica que o código está errado
        wrongCode.SetActive(true);

        // Chama o método DeactivateObject após 1 segundo
        Invoke("DeactivateObject", 1f);
    }

    void DeactivateObject()
    {
        // Desativa o objeto
        wrongCode.SetActive(false);
    }

    public bool ColorCodeSolved()
    {
        return solved;
    }

    // Função para buscar a sequência de cores do banco de dados
    IEnumerator FetchColorCode()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;
            ColorData colorData = JsonUtility.FromJson<ColorData>(jsonResponse);

            if (colorData.error == null)
            {
                correctCode = colorData.colors;  // Armazena a sequência de cores correta
                Debug.Log("Color sequence fetched: " + string.Join(", ", correctCode));
            }
            else
            {
                Debug.LogError("Erro: " + colorData.error);
            }
        }
    }
}

// Classe para deserializar o JSON
[System.Serializable]
public class ColorData
{
    public string[] colors;
    public string error;
}
