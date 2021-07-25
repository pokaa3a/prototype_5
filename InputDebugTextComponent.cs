using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputDebugTextComponent : MonoBehaviour
{
    public Text textbox;
    public string debugText;
    
    void Start()
    {
        textbox = GetComponent<Text>();
    }

    void Update()
    {
        textbox.text = debugText;
    }

    public void SetDebugText(string text)
    {
        debugText = text;
    }
}
