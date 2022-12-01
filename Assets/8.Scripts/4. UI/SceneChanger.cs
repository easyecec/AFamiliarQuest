    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public void StartMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("FirstLevel");
    }
    public void StartMultiplayer()
    {
        SceneManager.LoadScene("FirstLevelMulti");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
