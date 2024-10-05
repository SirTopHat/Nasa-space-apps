using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlanetInfoDisplay : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text detailsText;

    public GameObject planetInfoMenu;

    public Transform currentPlanetTransform;
    public GameObject planetInfoObjPanel;
    public TMP_Text planetNameTextObj; // Reference to the UI text element
    public Transform playerShip; // Reference to the player's ship
    public float displayRange = 1000f; // Distance within which the name will appear
    private Camera mainCamera;

    bool hasPlanetInSight;

    void Start() {
        mainCamera = Camera.main; // Get the main camera
        planetNameTextObj.gameObject.SetActive(false); // Initially hide the text
    }

    private void Update() {
        /*

        if (hasPlanetInSight) {
            float distance = Vector3.Distance(playerShip.position, currentPlanetTransform.position);
            if (distance <= displayRange) {
                planetInfoObjPanel.SetActive(true); // Show the name text
                planetNameTextObj.text = nameText.text; // Set the planet's name

                // Convert planet's world position to screen position
                Vector3 screenPos = mainCamera.WorldToScreenPoint(currentPlanetTransform.position);
                planetInfoObjPanel.transform.position = screenPos; // Set text position
            }
        } else {
            planetInfoObjPanel.gameObject.SetActive(false); // Hide the text when far
        }
        */
    }

    public void ShowPlanetDetails(PlanetInfo planetInfo) {
        hasPlanetInSight = true;

        currentPlanetTransform = planetInfo.GetComponent<Transform>();
        planetInfoMenu.SetActive(true);
        nameText.text = planetInfo.planetName;
        detailsText.text = planetInfo.planetDetails;
        planetInfo.hasDisplayedInfo = true;
        Debug.Log("Showing Planet Info");
    }

    public void ShowPlanetDetails(string name, string details, bool hasDisplayed) {
        hasPlanetInSight = true;

        planetInfoMenu.SetActive(true);
        nameText.text = name;
        detailsText.text = details;
        hasDisplayed = true;
        Debug.Log("Showing Planet Info");
    }

    public void HidePlanetDetails() {
        hasPlanetInSight = true;
        planetInfoMenu.SetActive(false);
        Debug.Log("Hiding Planet Info");
    }
}
