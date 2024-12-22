using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SyringeRiddle4 : MonoBehaviour
{
    private float moveSpeed = 3f;
    private float maxValue = 100f;
    private bool isMoving = false;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private int currentAmount;

    private int correctAnswer;
    private float startingPoint; // Agora configurado a partir de QuestionLoader
    private bool solved = false;

    public Camera riddle4Camera;
    public TMP_Text mlText;
    public Button okButton;
    public GameObject wallPart;
    public GameObject syringeRiddle;
    public GameObject redArrow;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + new Vector3(0, 0, 22);
        wallPart.SetActive(true);
        SetStartingML();

        // Inicia a corrotina para buscar a resposta correta do banco de dados
        StartCoroutine(GetCorrectAnswerFromDatabase(16)); // ID desejado
    }

    void Update()
    {
        if (isMoving && Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            MoveTowardsTarget();
        }
        else if (!isMoving && Vector3.Distance(startPosition, transform.position) <= 0.01f)
        {
            transform.position = startPosition;
        }

        float percentage = Mathf.InverseLerp(0f, Vector3.Distance(startPosition, targetPosition), Vector3.Distance(startPosition, transform.position));
        float mappedValue = percentage * maxValue;
        mappedValue = Mathf.Round(mappedValue);
        currentAmount = 100 - (int)mappedValue;

        mlText.text = currentAmount.ToString("0" + " ml");

        // Se chegar a 0 ml e o enigma não foi resolvido, resetar para a posição inicial
        if (currentAmount == 0 && !solved)
        {
            SetStartingML();  // Retorna à posição inicial automaticamente
        }

        if (solved)
        {
            wallPart.SetActive(false); // Revela o compartimento secreto
            syringeRiddle.SetActive(false);
            redArrow.SetActive(true);
        }

        if (riddle4Camera.enabled == false)
        {
            SetStartingML();
        }
    }

    public void SetStartingPoint(float value)
    {
        startingPoint = value;
    }

    void SetStartingML()
    {
        float startingPercentage = (100 - startingPoint) / maxValue;
        float initialPosition = startingPercentage * Vector3.Distance(startPosition, targetPosition);
        transform.position = Vector3.MoveTowards(startPosition, targetPosition, initialPosition);
    }

    void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    void OnMouseDown()
    {
        isMoving = true;
    }

    void OnMouseUp()
    {
        isMoving = false;
    }

    public void OnButtonClicked()
    {
        if (currentAmount == correctAnswer)
        {
            solved = true;
            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log("Wrong Answer! " + currentAmount);
            SetStartingML();  // Retorna à posição inicial após um erro, se o botão for pressionado
        }
    }

    private IEnumerator GetCorrectAnswerFromDatabase(int id)
    {
        string url = "http://localhost/unity/tablet_answer.php?id=" + id;
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            var jsonResult = www.downloadHandler.text;
            var jsonData = JsonUtility.FromJson<AnswerResponse>(jsonResult);
            if (jsonData != null)
            {
                if (int.TryParse(jsonData.AnswerText, out correctAnswer))
                {
                    Debug.Log("Correct answer fetched: " + correctAnswer);
                }
                else
                {
                    Debug.Log("Failed to parse correct answer.");
                }
            }
            else
            {
                Debug.Log("Failed to parse JSON response.");
            }
        }
    }

    [System.Serializable]
    public class AnswerResponse
    {
        public string AnswerText;
    }
}
