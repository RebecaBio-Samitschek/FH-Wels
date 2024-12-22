using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.EventSystems;

// Classe serializável para armazenar os valores deserializados do JSON
[System.Serializable]
public class AnswerValues
{
    public int[] values;
}

public class ThirdRiddleSolved : MonoBehaviour
{
    public bool alreadyPlayed;
    public Slider slider1;
    int val1;

    public Slider slider2;
    int val2;

    public Slider slider3;
    int val3;

    public Slider slider4;
    int val4;

    public GameObject counterDoor;
    public GameObject sliders;
    public Button okButton;

    public AudioSource soundCorrect;
    public AudioClip clip;

    public CameraSwitcher cameraSwitcher;

    public bool solved;

    void Start()
    {
        solved = false;
        alreadyPlayed = false;
        counterDoor = GameObject.Find("cabinetDoorRiddle");
        sliders = GameObject.Find("Sliders");
        sliders.SetActive(true);
        cameraSwitcher = FindObjectOfType<CameraSwitcher>();

        StartCoroutine(GetValuesFromDatabase());
    }

    IEnumerator GetValuesFromDatabase()
    {
        string url = "http://localhost/unity/cabinet_solved.php?AnswerIDs=11,12,13,14";

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            AnswerValues answerValues = JsonUtility.FromJson<AnswerValues>(json);

            if (answerValues.values.Length == 4)
            {
                val1 = answerValues.values[0];
                val2 = answerValues.values[1];
                val3 = answerValues.values[2];
                val4 = answerValues.values[3];
            }
            else
            {
                Debug.LogError("Erro: Quantidade incorreta de valores recebidos.");
            }
        }
        else
        {
            Debug.LogError("Erro na requisição: " + request.error);
        }
    }

    void Update()
    {
        if (!alreadyPlayed && slider1.value == val1 && slider2.value == val2 && slider3.value == val3 && slider4.value == val4)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.currentSelectedGameObject == okButton)
                {
                    soundCorrect.PlayOneShot(clip);
                    alreadyPlayed = true;
                    counterDoor.SetActive(false);
                    sliders.SetActive(false);
                    solved = true;
                }
            }
        }
    }
}
