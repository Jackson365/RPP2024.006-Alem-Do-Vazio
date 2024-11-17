using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject CanvasObj; 
    public GameObject MenuObj;

    public void LoadGame()
    {
        StartCoroutine(ActivateMenuAfterLoad());
        
        SceneManager.LoadScene(1);
        StartCoroutine(ActivateCanvasAfterLoad());
        
    }

    private IEnumerator ActivateCanvasAfterLoad()
    {
        yield return null;
        CanvasObj.SetActive(true);
    }
    
    private IEnumerator ActivateMenuAfterLoad()
    {
        yield return null;
        MenuObj.SetActive(false);
    }
}