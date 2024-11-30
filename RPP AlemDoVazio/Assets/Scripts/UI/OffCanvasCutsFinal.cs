using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffCanvasCutsFinal : MonoBehaviour
{
    public GameObject CanvasObj;
    void Start()
    {
        GameController.instance.CanvasObj.SetActive(false);
    }
}
