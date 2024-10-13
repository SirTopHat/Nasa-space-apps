using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CodexInfoCanvas : MonoBehaviour {
    public TMP_Text planetName;
    public TMP_Text planetDescription;
    public TMP_Text planetDetails;


    public void ShowPlanetDetails(CodexInfoPlanet ip) {
        planetName.text = ip.planetName;
        planetDescription.text = ip.planetDesc;
        planetDetails.text = ip.planetMoreDetails;
    }

    public void ShowPlanetDetails(string name, string desc, string details) {
        planetName.text = name;
        planetDescription.text = desc;
        planetDetails.text = details;
    }

    public void ShowPlanetDetails(string name, string desc, string details, Sprite planetImg) {
        planetName.text = name;
        planetDescription.text = desc;
        planetDetails.text = details;

    }
}
