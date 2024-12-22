using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ImageLoaderComputer : MonoBehaviour
{
    public string url = "http://localhost/unity/computer_img.php"; // URL do PHP
    public SpriteRenderer spriteRenderer;  // O Sprite Renderer no qual você vai exibir a imagem

    void Start()
    {
        StartCoroutine(FetchImagePath());
    }

    IEnumerator FetchImagePath()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            // Parse da resposta JSON
            string jsonResponse = request.downloadHandler.text;
            ImageData imageData = JsonUtility.FromJson<ImageData>(jsonResponse);

            if (imageData.error == null)
            {
                StartCoroutine(LoadImage(imageData.ImagePath));
            }
            else
            {
                Debug.LogError("Erro: " + imageData.error);
            }
        }
    }

    IEnumerator LoadImage(string imagePath)
    {
        UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(imagePath);
        yield return textureRequest.SendWebRequest();

        if (textureRequest.result == UnityWebRequest.Result.ConnectionError || textureRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(textureRequest.error);
        }
        else
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(textureRequest);
            Sprite newSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            spriteRenderer.sprite = newSprite; // Atribui o sprite ao Sprite Renderer
        }
    }
}

[System.Serializable]
public class ImageData
{
    public string ImagePath;
    public string error;
}
