using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using VSX.UniversalVehicleCombat;

public class SolarSystemBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public string solarSystemName; // The name of the solar system
    public GameObject hoverPanel; // Reference to the hover panel
    public TMP_Text hoverText; // Reference to the text inside the hover panel
    public TMP_Text confirmText;
    //public GameObject playerShip; // Reference to the player's ship
    //public Vector3 destination; // Position of the solar system in space

    public SimpleMenuManager galaxyMapMenu;

    SpaceJumpDriveManager spaceJumpDriveManager;

    public GameObject solarSystemObj;
    public Transform playerAppearTransform;

    public Material skyboxMat;

    void Start() {
        spaceJumpDriveManager = FindObjectOfType<SpaceJumpDriveManager>();
    }

    // Called when the mouse enters the button
    public void OnMouseEnter() {
        hoverPanel.SetActive(true); // Show the hover panel
        hoverText.text = solarSystemName; // Set the text to the solar system's name
    }

    // Called when the mouse exits the button
    public void OnMouseExit() {
        hoverPanel.SetActive(false); // Hide the hover panel
    }

    // Called when the button is clicked
    public void OnClick() {
        
    }

    public void SetConfirmationTitle() {
        confirmText.text = "Jump to " + solarSystemName + "?";
    }

    public void SetNextDestinationDetails() {
        spaceJumpDriveManager.SetupNextDestination(solarSystemObj, playerAppearTransform, skyboxMat);
        galaxyMapMenu.firstSelected = gameObject;
    }

    public void OnPointerEnter(PointerEventData pointerEventData) {
        hoverPanel.SetActive(true); // Show the hover panel
        hoverText.text = solarSystemName; // Set the text to the solar system's name
    }

    public void OnPointerExit(PointerEventData pointerEventData) {
        hoverPanel.SetActive(false); // Hide the hover panel
    }
}
