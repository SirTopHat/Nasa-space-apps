using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MiniGameStartingSceneManager : MonoBehaviour
{
    public Image fadeImage; // Assign your full-screen fade Image here
    public float transitionTime = 3f; // Duration of the transition

    void Start()
    {
        // Start with the fade image hidden
        fadeImage.rectTransform.sizeDelta = new Vector2(0, Screen.height); // Start from the bottom
        fadeImage.color = new Color(0, 0, 0, 0); // Fully transparent
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(ChangeScene("Transit Detection Game"));
        }
    }

    private IEnumerator ChangeScene(string sceneName)
    {
        // Start the fade effect from bottom to top
        float elapsedTime = 0f;
        while (elapsedTime < transitionTime)
        {
            // Calculate the current height of the fade image
            float t = elapsedTime / transitionTime;
            fadeImage.color = new Color(0, 0, 0, t); // Fade to black

            // Increase the height of the fade image
            fadeImage.rectTransform.sizeDelta = new Vector2(0, t * Screen.height); // Increase height from bottom

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the fade is complete
        fadeImage.color = new Color(0, 0, 0, 1); // Fully black
        fadeImage.rectTransform.sizeDelta = new Vector2(0, Screen.height); // Full height

        // Load the new scene
        SceneManager.LoadScene(sceneName);
    }
}