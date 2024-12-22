using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CodeCorrect : MonoBehaviour
{
    public Text codeDisplayText;
    public Button validateButton;
    public GameObject correctImage;

    private string correctCode;  // Valor correto ser� preenchido dinamicamente

    // URL do PHP que retorna o valor correto (correctCode)
    public string url = "https://localhost/unity/waage.php";

    void Start()
    {
        // Inicialmente esconde a imagem
        correctImage.gameObject.SetActive(false);

        // Inicia o processo para buscar o valor correto do banco de dados
        StartCoroutine(FetchCorrectCode());

        // Atribui a fun��o ao bot�o de valida��o
        validateButton.onClick.AddListener(ValidateCode);
    }

    // Fun��o que busca o valor do correctCode do banco de dados
    IEnumerator FetchCorrectCode()
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
            CodeData codeData = JsonUtility.FromJson<CodeData>(jsonResponse);

            if (codeData.error == null)
            {
                // Armazena o valor correto obtido
                correctCode = codeData.correctAnswer + " g" ;
                Debug.Log("Correct code fetched: " + correctCode);
            }
            else
            {
                Debug.LogError("Erro: " + codeData.error);
            }
        }
    }

    // Valida��o do c�digo inserido pelo usu�rio
    void ValidateCode()
    {
        // Compara o c�digo digitado com o c�digo correto
        if (codeDisplayText.text == correctCode)
        {
            // Exibe a imagem quando o c�digo est� correto
            correctImage.gameObject.SetActive(true);
        }
        else
        {
            // Opcional: Esconde a imagem se o c�digo estiver incorreto
            correctImage.gameObject.SetActive(false);
        }
    }
}

// Classe para deserializar o JSON
[System.Serializable]
public class CodeData
{
    public string correctAnswer;
    public string error;
}
