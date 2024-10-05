using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance;
    public int score;
    public TextMeshProUGUI scoreText; 
    public Button tapButton; 
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
        tapButton.gameObject.SetActive(true); // Show button
    }

    public void ScorePoint()
    {
        score++;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = "Dips Detected: " + score;
    }
}
