using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] public string mainMenu = "MainMenu";
    [SerializeField] public string test = "Main";
    public void PlayGame()
    {
        SceneManager.LoadScene(test);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
