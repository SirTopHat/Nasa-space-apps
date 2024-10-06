using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VSX.UniversalVehicleCombat;

public class DetailedInfoPlanetManager : MonoBehaviour
{
    public SimpleMenuManager detailedInfoMenu;
    public GameObject informationPrompt;
    bool detailedInfoIsOpen = false;

    bool canOpenInformationMenu = false;

    public Image planetImage;
    public TMP_Text planetName;
    public TMP_Text planetDetailed;
    public TMP_Text planetType;


    public void HideCursor() {
        Cursor.visible = false;
    }

    public void ShowCursor() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ToggleDetailedStateFromButton() {
        detailedInfoIsOpen = !detailedInfoIsOpen;
    }

    public void ToggleDetailedInfo() {

        if (!detailedInfoIsOpen) {
            detailedInfoMenu.OpenMenu();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        } else {
            detailedInfoMenu.CloseMenu();
            Cursor.visible = false;
        }
        detailedInfoIsOpen = !detailedInfoIsOpen;
    }

    public void SetCanOpenMenu(bool canOpen) {
        canOpenInformationMenu = canOpen;

    }

    public void SetInformationPromptVisible(bool isVisible) {
        informationPrompt.SetActive(isVisible);
    }

    public void SetDetailedMenuInformation(PlanetInfo pi) {

        if (pi.planetImage == null) {
            Color transparentColor = planetImage.color;
            transparentColor.a = 0f;
            planetImage.color = transparentColor;
        } else {
            Color opaqueColor = planetImage.color;
            opaqueColor.a = 1f;
            planetImage.color = opaqueColor;
            planetImage.sprite = pi.planetImage;
        }

        
        planetName.text = pi.planetName;
        planetDetailed.text = pi.planetMoreDetails;
        planetType.text = pi.planetType;
    }

    public void SetDetailedMenuInformation(string name, string details, string type, Sprite _planetImage) {
        planetImage.sprite = _planetImage;
        planetName.text = name;
        planetDetailed.text = details;
        planetType.text = type;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.I) & canOpenInformationMenu) {
            ToggleDetailedInfo();
        }
    }
}
