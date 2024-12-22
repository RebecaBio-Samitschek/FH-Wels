using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject painelMenu;
    [SerializeField] private GameObject painelOptionen;
    [SerializeField] private GameObject painelGameOver;

    private void Awake()
    {
        // Verifica��o de refer�ncia aos pain�is (deve ser configurado no Inspetor)
        if (painelMenu == null || painelOptionen == null || painelGameOver == null)
        {
            Debug.LogError("Um ou mais pain�is n�o est�o configurados no Inspetor!");
        }
    }

    public void Spielen()
    {
        // Carrega a cena Level-1 para iniciar o jogo
        SceneManager.LoadScene("Level-1");
    }

    public void OpenOptionen()
    {
        painelMenu.SetActive(false);
        painelOptionen.SetActive(true);
    }

    public void CloseOptionen()
    {
        painelOptionen.SetActive(false);
        painelMenu.SetActive(true);
    }

    public void Beenden()
    {
        Debug.Log("Sair do jogo");
        Application.Quit();
    }

    public void OpenGameOver()
    {
        if (painelGameOver != null)
        {
            painelGameOver.SetActive(true);  // Exibe o painel de Game Over
        }
        else
        {
            Debug.LogError("Painel Game Over n�o est� configurado no inspetor.");
        }
    }

    public void CloseGameOver()
    {
        if (painelGameOver != null)
        {
            painelGameOver.SetActive(false);  // Esconde o painel de Game Over
        }

        // Volta ao menu principal e reseta o jogo
        ResetToMainMenu();
    }

    private void ResetToMainMenu()
    {
        // Recarrega a cena do Menu para restaurar tudo ao estado inicial
        SceneManager.LoadScene("Menu");
    }
}

