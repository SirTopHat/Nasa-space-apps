using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSpaceApps : MonoBehaviour
{

    public void LoadGame() { 
    
    }

    public void LoadScene(string name) {
        SceneManager.LoadScene(name);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
