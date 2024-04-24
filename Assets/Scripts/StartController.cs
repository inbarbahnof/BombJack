using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneManager.LoadScene(1);
    }
    
    public void OnQuitClick()
    {
        #if UNITY_EDITOR
                // If running in the Unity Editor, stop playing
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                    // If running as a built game, quit the application
                    Application.Quit();
        #endif
    }
}
