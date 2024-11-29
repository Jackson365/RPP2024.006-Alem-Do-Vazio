using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PularCutsceneInicial : MonoBehaviour
{
    public string cenaCarregar;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(cenaCarregar);
        }
    }
}
