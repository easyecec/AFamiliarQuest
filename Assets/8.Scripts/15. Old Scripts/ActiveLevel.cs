using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveLevel : MonoBehaviour
{
    public void PlayLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
