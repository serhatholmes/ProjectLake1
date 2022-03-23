using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PressAnyKey : MonoBehaviour
{
    
    void Update()
    {
        if(Input.anyKeyDown)
        {
            LoadMainMenu();
        }

    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        
    }
   
}
