using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ScaleRiddle : MonoBehaviour
{
    public float counter;
    public GameObject spatula;
    public Camera riddle7Camera;
    public GameObject liquid;
    private float originalHeight;
    private float newHeight;
    public Button submitButton;
    public GameObject cableCabinetDoor;

    private int correctAnswer;  // Será preenchido dinamicamente

    public Text gText;

    // URL do PHP que retorna o valor correto (correctAnswer)
    public string url = "http://localhost/unity/waage.php";

    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        originalHeight = liquid.transform.localScale.y;
        newHeight = originalHeight;

        StartCoroutine(FetchCorrectAnswer());  // Busca o valor correto do banco de dados
    }

    // Função que busca o valor do correctAnswer do banco de dados
    IEnumerator FetchCorrectAnswer()
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
            ScaleRiddleAnswerData answerData = JsonUtility.FromJson<ScaleRiddleAnswerData>(jsonResponse);

            if (answerData.error == null)
            {
                // Converte o valor recebido para um inteiro
                correctAnswer = int.Parse(answerData.correctAnswer);
                Debug.Log("Correct answer fetched: " + correctAnswer);
            }
            else
            {
                Debug.LogError("Erro: " + answerData.error);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && riddle7Camera.enabled)
        {
            RaycastHit hit;
            Ray ray = riddle7Camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == spatula && correctAnswer != counter)
                {
                    counter += 10;
                    gText.text = counter.ToString("0" + " g");
                    newHeight = originalHeight + (counter / 100);
                    Vector3 newScale = new Vector3(liquid.transform.localScale.x, newHeight, liquid.transform.localScale.z);
                    liquid.transform.localScale = newScale;
                    if (counter >= 1000)
                    {
                        newHeight = originalHeight;
                        newScale = new Vector3(liquid.transform.localScale.x, newHeight, liquid.transform.localScale.z);
                        liquid.transform.localScale = newScale;
                        counter = 0;
                        gText.text = counter.ToString("0" + " g");
                    }
                }
            }
        }
        if (riddle7Camera.enabled == false)
        {
            newHeight = originalHeight;
            Vector3 newScale = new Vector3(liquid.transform.localScale.x, newHeight, liquid.transform.localScale.z);
            liquid.transform.localScale = newScale;
            counter = 0;
            gText.text = counter.ToString("0" + " g");
        }
    }

    public void OnSubmitClicked()
    {
        Debug.Log("clicked");
        if (counter == correctAnswer)  // Usando o valor do banco de dados aqui
        {
            cableCabinetDoor.SetActive(false);
            spatula.SetActive(false);
            Debug.Log("correct");
        }
        else
        {
            Debug.Log("false");
        }
    }
}

// Renomeada a classe para evitar conflitos
[System.Serializable]
public class ScaleRiddleAnswerData
{
    public string correctAnswer;
    public string error;
}