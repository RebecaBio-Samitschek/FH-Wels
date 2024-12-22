using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ImageLoader : MonoBehaviour
{
    public Renderer[] targetRenderers; // Array de Renderers onde os materiais serão aplicados
    public string baseUrl = "http://127.0.0.1/unity/frame.php?id="; // URL base para o PHP

    // Chama automaticamente o carregamento das imagens no início
    private void Start()
    {
        LoadAllImages(); // Inicia o carregamento das imagens de ID 1 a 10
    }

    public void LoadAllImages()
    {
        for (int id = 1; id <= 10; id++)
        {
            if (id - 1 < targetRenderers.Length) // Garante que não exceda o número de Renderers
            {
                StartCoroutine(GetImageFromDatabase(id, targetRenderers[id - 1]));
            }
        }
    }

    private IEnumerator GetImageFromDatabase(int id, Renderer targetRenderer)
    {
        string url = baseUrl + id;
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string imageUrl = request.downloadHandler.text.Trim();
            Debug.Log("URL received from PHP for ID " + id + ": " + imageUrl);

            if (Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
            {
                Debug.Log("Attempting to load image from URL: " + imageUrl);
                UnityWebRequest imageRequest = UnityWebRequestTexture.GetTexture(imageUrl);
                yield return imageRequest.SendWebRequest();

                if (imageRequest.result == UnityWebRequest.Result.Success)
                {
                    Texture2D texture = DownloadHandlerTexture.GetContent(imageRequest);

                    if (texture != null)
                    {
                        Debug.Log("Texture successfully loaded for ID " + id);

                        // Criando um material com shader "Unlit/Texture" para garantir a visibilidade da textura
                        Material material = new Material(Shader.Find("Unlit/Texture"));
                        material.mainTexture = texture;

                        // Aplicando o material no Renderer alvo
                        targetRenderer.material = material;
                    }
                    else
                    {
                        Debug.LogError("Texture is null for ID " + id);
                    }
                }
                else
                {
                    Debug.LogError("Error loading image content for URL " + imageUrl + ": " + imageRequest.error);
                }
            }
            else
            {
                Debug.LogError("Invalid URL returned from PHP for ID " + id + ": " + imageUrl);
            }
        }
        else
        {
            Debug.LogError("Error loading URL for ID " + id + ": " + request.error);
        }
    }
}
