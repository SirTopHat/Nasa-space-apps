using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetProximity : MonoBehaviour {

    public PlanetInfoDisplay planetInfoDisplay;
    public PlanetInfo planetInfo;

    private void Start() {
        planetInfoDisplay = FindObjectOfType<PlanetInfoDisplay>();
        planetInfo = GetComponent<PlanetInfo>();
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("Detected Entry");
        // Check if the object entering the trigger is the player's ship
        if (other.CompareTag("PlayerShip")) // Ensure the player ship has the tag "PlayerShip"
        {
            planetInfoDisplay.ShowPlanetDetails(planetInfo); // Show the popup when ship enters proximity
        }
    }

    void OnTriggerExit(Collider other) {
        // When the player ship leaves the trigger zone, hide the popup
        Debug.Log("Detected Exit");
        if (other.CompareTag("PlayerShip")) {
            //planetInfoDisplay.HidePlanetDetails();
        }
    }
}
