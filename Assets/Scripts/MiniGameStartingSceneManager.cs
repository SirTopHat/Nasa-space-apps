using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MiniGameStartingSceneManager : MonoBehaviour
{
    public GameObject MinigameStartingUI;
    public GameObject MinigameUI;
    public GameObject TaskCompletedUI;
    public GameObject GameInstructionsUI;
    public float transitionTime = 3f; 

    void Start() 
    {
        MinigameUI.SetActive(false);
        TaskCompletedUI.SetActive(false);
        GameInstructionsUI.SetActive(false);
        MinigameStartingUI.SetActive(true);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            MinigameStartingUI.SetActive(false);
            MinigameUI.SetActive(true);
            GameInstructionsUI.SetActive(true);
        }
    }
}