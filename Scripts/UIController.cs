using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{

    public TextMeshProUGUI scoreText;


    public GameObject mainCanvas;
    public GameObject settings;
    public TMP_Dropdown dropdown;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Highscore: " + PlayerPrefs.GetInt("score"); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void play()
    {
        SceneManager.LoadScene(1);
    }

    public void quit()
    {
        Application.Quit();
    }

    public void setting()
    {
        mainCanvas.SetActive(false);
        settings.SetActive(true);
    }

    public void back()
    {
        mainCanvas.SetActive(true);
        settings.SetActive(false);
    }

    public void setInput()
    {

        Debug.Log(dropdown.value);

        if(dropdown.value ==  0)
        {
            InputManager.controller = "Mouse & Keyboard";
        }
        else if (dropdown.value == 1)
        {
            InputManager.controller = "PS4";
        }
        else if (dropdown.value == 2)
        {
            InputManager.controller = "XBOX";
        }
    }


}
