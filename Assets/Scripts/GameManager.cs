using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public int collectedBobms = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI HighScoreText;
    public TextMeshProUGUI gameOverText;
    public int lives = 3;
    public PlayerController player;
    public GameObject gameOverCanvas;
    public AudioSource StartSource;
    public AudioSource winSoundSource;
    public AudioSource loseSoundSource;
    public GhostController[] ghosts;
    public GameObject[] livesArr;
    public BobmActivater activater;
    
    private int curLifeIndex = 2;
    private int highScore = 0;
    private const int BOMBS_NUMBER = 24;

    private void Start()    
    {
        UpdateScore();
        gameOverCanvas.SetActive(false);
        StartSource.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnQuitClick();
        }
    }

    private void OnQuitClick()
    {
        #if UNITY_EDITOR
                // If running in the Unity Editor, stop playing
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                    // If running as a built game, quit the application
                    Application.Quit();
        #endif
    }
    public void UpdateScore()
    {
        scoreText.text = "score: " + score;
        if (collectedBobms == BOMBS_NUMBER)
        {
            Win();
        }
    }

    public void StopGhostMovement()
    {
        foreach (var ghost in ghosts)
        {
            ghost.StopMoving();
        }
    }
    
    public void StartGhostMovement()
    {
        foreach (var ghost in ghosts)
        {
            ghost.StartMoving();
        }
    }

    public void RemoveLifeFromBoard()
    {
        if (curLifeIndex >= 0)
        {
            livesArr[curLifeIndex].SetActive(false);
            curLifeIndex--;
        }
    }

    public void GameOver()
    {
        loseSoundSource.Play();
        activater.gameObject.SetActive(false);
        gameOverCanvas.SetActive(true);
        gameOverText.text = "game\n \nover";
        StopGhostMovement();
        player.StopMovement(false);
    }

    public void GoToStartScreen()
    {
        SceneManager.LoadScene(0);
    }

    public void ResetGame()
    {
        gameOverCanvas.SetActive(false);
        activater.gameObject.SetActive(true);
        CheckHighScore();
        score = 0;
        collectedBobms = 0;
        UpdateScore();
        
        StartSource.Play();
        
        player.gameObject.SetActive(true);
        player.ResetPosition();

        activater.ResetBombs();
        
        curLifeIndex = 2;
        lives = 3;
        
        ResetGhosts();
        ResetLivesUI();
        activater.Activate();
    }

    private void ResetLivesUI()
    {
        foreach (var life in livesArr)
        {
            life.gameObject.SetActive(true);
        }
    }

    private void ResetGhosts()
    {
        foreach (var ghost in ghosts)
        {
            ghost.Reset();
        }
    }

    private void Win()
    {
        StopGhostMovement();
        winSoundSource.Play();
        player.StopMovement(true);
        gameOverText.text = "you\n \nwin!";
        gameOverCanvas.SetActive(true);
    }
    

    private void CheckHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            HighScoreText.text = "high score: " + highScore;    
        }
    }
}
