using System.Collections;
using UnityEngine;

public class SpoonInteraction : MonoBehaviour
{
    public Camera taskCamera;  // Arraste a c�mera espec�fica no Inspector
    public GameObject lid;
    public Animator spoonAnimator;
    private bool lidRemoved = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (taskCamera == null)
            {
                Debug.LogError("A c�mera da tarefa n�o foi atribu�da no Inspector.");
                return;
            }

            RaycastHit hit;
            Ray ray = taskCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Objeto clicado: " + hit.collider.gameObject.name);

                // Verifica se o objeto clicado � a colher
                if (hit.collider.gameObject == gameObject)
                {
                    if (!lidRemoved)
                    {
                        if (lid == null)
                        {
                            Debug.LogError("O objeto 'LID' n�o foi atribu�do no Inspector.");
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
                            Debug.LogError("O componente 'Animator' n�o foi atribu�do no Inspector.");
                        }
                        else
                        {
                            Debug.Log("Anima��o da colher acionada");
                            spoonAnimator.SetTrigger("SpoonClicked");  // Dispara a anima��o uma vez
                        }
                    }
                }
            }
        }
    }
}
