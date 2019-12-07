using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] public MainMenuManager manager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision");
            SceneManager.LoadScene(manager.GetComponent<MainMenuManager>().mainMenu);
        }
    }
}
