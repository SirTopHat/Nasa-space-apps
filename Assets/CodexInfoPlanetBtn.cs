using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodexInfoPlanetBtn : MonoBehaviour
{
    public CodexInfoPlanet infoPlanet;

    public CodexInfoCanvas codexCanvas;

    private void Start() {
        //codexCanvas = FindObjectOfType<CodexInfoCanvas>();
        infoPlanet = GetComponent<CodexInfoPlanet>();
    }


    public void DisplayCodexPlanetInfo() {
        codexCanvas.ShowPlanetDetails(infoPlanet);
    }
}
