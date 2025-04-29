using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Update()
    {
        // Quit game when Esc is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }

        // Restart scene when R is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        // Stop play mode in the editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Quit application in build
        Application.Quit();
#endif
    }

    void RestartScene()
    {
        // Reload the currently active scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}

