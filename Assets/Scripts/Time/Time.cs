using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public TMP_Text timeText;
    public float timeCount;
    public bool timeOver = false;
    public int minutes, seconds;
    public bool timerIsRunning = false;
    public AccessMenu menu;

    private string baseUrl = "http://localhost/unity/";

    void Start()
    {
        StartCoroutine(GetGameSettings());
    }

    private IEnumerator GetGameSettings()
    {
        string fullUrl = baseUrl + "time.php";

        UnityWebRequest www = UnityWebRequest.Get(fullUrl);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            ProcessGameSettings(www.downloadHandler.text);
        }
    }

    private void ProcessGameSettings(string json)
    {
        GameSetting[] settings = JsonHelper.FromJson<GameSetting>(json);

        if (settings.Length > 0)
        {
            timeCount = settings[0].game_time_limit;
            timerIsRunning = true;
        }
        else
        {
            Debug.LogError("No game settings found.");
        }
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            TimeCount();
        }
    }

    private void TimeCount()
    {
        if (!timeOver && timeCount > 0)
        {
            timeCount -= Time.deltaTime;

            minutes = (int)(timeCount / 60f);
            seconds = (int)(timeCount - minutes * 60f);
            StartTimer();

            if (timeCount <= 0)
            {
                timeCount = 0;
                timeOver = true;
                timerIsRunning = false;
                Debug.Log("Time has run out!");

                // Chama o evento de tempo esgotado
                OnTimeOver();
            }
        }
    }

    public void StartTimer()
    {
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Evento para quando o tempo acabar
    private void OnTimeOver()
    {
        // Tenta encontrar o MainMenu na cena atual e exibe o painel de Game Over
        MainMenu mainMenu = FindObjectOfType<MainMenu>();
        if (mainMenu != null)
        {
            mainMenu.OpenGameOver();
            SceneManager.UnloadSceneAsync("Level-1");  // Descarrega a cena Level-1
        }
        else
        {
            Debug.Log("Carregando a cena do Menu...");
            SceneManager.sceneLoaded += OnMainMenuSceneLoaded;  // Registra o evento
            SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        }
    }

    // Evento chamado quando a cena do Menu é carregada
    private void OnMainMenuSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu")
        {
            MainMenu mainMenu = FindObjectOfType<MainMenu>();
            if (mainMenu != null)
            {
                mainMenu.OpenGameOver();
            }
            SceneManager.UnloadSceneAsync("Level-1");  // Descarrega a cena Level-1
            SceneManager.sceneLoaded -= OnMainMenuSceneLoaded;  // Desregistra o evento
        }
    }

    [System.Serializable]
    public class GameSetting
    {
        public int game_time_limit;
    }

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            string newJson = "{\"Items\":" + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
            return wrapper.Items;
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}
