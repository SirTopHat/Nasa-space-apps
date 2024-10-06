using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance;
    private int score;
    public TextMeshProUGUI scoreText; 
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        StartGame();
    }

    public void StartGame()
    {
        score = 0;
        UpdateScore();
    }

    public void ScorePoint()
    {
        score++;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = "Dips Detected: " + score + " /2";
    }
}
