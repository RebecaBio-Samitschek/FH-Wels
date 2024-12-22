using System.Collections;
using UnityEngine;

public class SpoonInteraction : MonoBehaviour
{
    public Camera taskCamera;  // Arraste a câmera específica no Inspector
    public GameObject lid;
    public Animator spoonAnimator;
    private bool lidRemoved = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (taskCamera == null)
            {
                Debug.LogError("A câmera da tarefa não foi atribuída no Inspector.");
                return;
            }

            RaycastHit hit;
            Ray ray = taskCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Objeto clicado: " + hit.collider.gameObject.name);

                // Verifica se o objeto clicado é a colher
                if (hit.collider.gameObject == gameObject)
                {
                    if (!lidRemoved)
                    {
                        if (lid == null)
                        {
                            Debug.LogError("O objeto 'LID' não foi atribuído no Inspector.");
                        }
                        else
                        {
                            Debug.Log("Tampa removida");
                            lid.SetActive(false);  // Desativa a tampa
                            lidRemoved = true;
                        }
                    }
                    else
                    {
                        if (spoonAnimator == null)
                        {
                            Debug.LogError("O componente 'Animator' não foi atribuído no Inspector.");
                        }
                        else
                        {
                            Debug.Log("Animação da colher acionada");
                            spoonAnimator.SetTrigger("SpoonClicked");  // Dispara a animação uma vez
                        }
                    }
                }
            }
        }
    }
}
