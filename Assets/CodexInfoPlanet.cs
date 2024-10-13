using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodexInfoPlanet : MonoBehaviour {
    public string planetName;

    [TextArea(3, 10)]
    public string planetDesc;

    [TextArea(3, 10)]
    public string planetMoreDetails;
    public Sprite planetImage;

    public CodexInfoCanvas codexCanvas;

    private void Start() {
        codexCanvas = FindObjectOfType<CodexInfoCanvas>();
    }


    public void DisplayCodexPlanetInfo() {
        codexCanvas.ShowPlanetDetails(planetName, planetDesc, planetMoreDetails, planetImage);
    }
}
