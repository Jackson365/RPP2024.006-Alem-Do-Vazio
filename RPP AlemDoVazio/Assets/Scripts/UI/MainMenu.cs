using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject CanvasObj;

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
        StartCoroutine(TimeCanvasOBJ());
    }
    
    private IEnumerator TimeCanvasOBJ()
    {
        yield return new WaitForSeconds(0.1f);
        CanvasObj.SetActive(true);
    }
}