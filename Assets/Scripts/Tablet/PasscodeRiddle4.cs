using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PasscodeRiddle4 : MonoBehaviour
{
    public Text codeDisplayText;
    public GameObject Passcode;
    public GameObject Riddle4;
    public bool solved = false;

    private string currentCode = "";
    private string correctCode = "";

    private void Awake()
    {
        codeDisplayText = GetComponentInChildren<Text>();
        StartCoroutine(GetCorrectCodeFromDatabase(15)); //  AnswerID 
    }

    public void OnNumberButtonClicked(Button button)
    {
        if (currentCode.Length < 4)
        {
            currentCode += button.name; // Use the element's name as the number.
            UpdateCodeDisplay();
        }
    }

    public void OnClearButtonClicked()
    {
        currentCode = "";
        UpdateCodeDisplay();
    }

    public void OnSubmitButtonClicked()
    {
        if (currentCode == correctCode)
        {
            solved = true;
            Passcode.SetActive(false);
            Riddle4.SetActive(true);
        }
        else
        {
            Debug.Log("Incorrect code. Try again!");
        }

        currentCode = "";
        UpdateCodeDisplay();
    }

    private void UpdateCodeDisplay()
    {
        codeDisplayText.text = currentCode;
    }

    private IEnumerator GetCorrectCodeFromDatabase(int id)
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
            // Parse JSON result
            var jsonResult = www.downloadHandler.text;
            var jsonData = JsonUtility.FromJson<AnswerTextResponse>(jsonResult);
            if (jsonData != null)
            {
                correctCode = jsonData.AnswerText;
            }
            else
            {
                Debug.Log("Failed to parse JSON response.");
            }
        }
    }

    [System.Serializable]
    public class AnswerTextResponse
    {
        public string AnswerText;
    }
}
