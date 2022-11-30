using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("FirstLevel");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
