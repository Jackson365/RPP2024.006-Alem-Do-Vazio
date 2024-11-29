using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarregarCenaCutscenes : MonoBehaviour
{
    public string cenaCarregar;
    
    void Start()
    {
        SceneManager.LoadScene(cenaCarregar);
    }
}
