using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIcontroller : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}
