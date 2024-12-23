using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameStarter : MonoBehaviour
{
    [SerializeField] string nextSceneName = "MainMenu";

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}