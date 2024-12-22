using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System;

public class LoadImageFromDatabase : MonoBehaviour
{
    public Image image; // Referência para o componente de imagem no Unity
    private string url = "https://localhost/unity/tipp.php";

    void Start()
    {
        if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
        {
            StartCoroutine(GetImageFromDatabase());
        }
        else
        {
            Debug.LogError("Malformed URL: " + url);
        }
    }

    IEnumerator GetImageFromDatabase()
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            string imageUrl = www.downloadHandler.text;
            if (Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
            {
                StartCoroutine(LoadImage(imageUrl));
            }
            else
            {
                Debug.LogError("Malformed image URL: " + imageUrl);
            }
        }
    }

    IEnumerator LoadImage(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
    }
}
