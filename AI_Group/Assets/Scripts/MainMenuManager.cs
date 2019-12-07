using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] public string mainMenu = "MainMenu";
    [SerializeField] public string main = "Main";
    public void PlayGame()
    {
        SceneManager.LoadScene(main);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
