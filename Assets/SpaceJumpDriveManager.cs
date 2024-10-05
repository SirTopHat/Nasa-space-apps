using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using VSX.Effects;
using VSX.UniversalVehicleCombat;
using VSX.UniversalVehicleCombat.Space;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class SpaceJumpDriveManager : MonoBehaviour
{
    public SimpleMenuManager galaxyMapMenu;
    bool galaxyMapIsOpen = false;

    public GameObject playerShip;
    public GameObject shipCamera;

    public AddRumble warpRumble;

    public SpaceFighterCameraController spaceFighterCameraController;

    public GameObject warpShaderMesh;
    public SS_Starfield3DWarp warpSS;
    public GameObject warpNoiseShaderMesh;
    public SS_Noise3DWarp warpNoiseSS;

    public GameObject solarSystemOrigin;
    public GameObject solarSystemDestination;

    public Transform jumpFinishPosition;

    public Material originSkybox;
    public Material destinationSkybox;

    public AudioSource audioSource;
    public AudioClip audioStartWarp;
    public AudioClip audioWarp;

    public GameObject mapDisclaimer;


    public Vector3 originalCameraPosition;  // To store the original camera position
    public Quaternion originalCameraRotation;  // To store the original camera rotation
    private Coroutine jumpCoroutine;  // To track the running coroutine

    private void Start() {
        // Save the camera's current position and rotation
        //originalCameraPosition = shipCamera.transform.position;
        //originalCameraRotation = shipCamera.transform.rotation;

        originSkybox = RenderSettings.skybox;
    }

    public void ToggleGalaxyMap() {

        if (!galaxyMapIsOpen) {
            galaxyMapMenu.OpenMenu();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        } else {
            galaxyMapMenu.CloseMenu();
            Cursor.visible = false;
        }
        galaxyMapIsOpen = !galaxyMapIsOpen;
    }

    private void Update() {
        if (Input.GetKeyUp(KeyCode.J)) {
            //StartJumpSequence();
        }

        if (Input.GetKeyUp(KeyCode.M)) {

            ToggleGalaxyMap();
        }
    }

    public void PerformWarpDrive() {
        StartJumpSequence();
    }

    public void StartJumpSequence() {

        if (jumpCoroutine != null) {
            StopCoroutine(jumpCoroutine);
        }

        jumpCoroutine = StartCoroutine(SpaceJumpSequenceIE());
    }

    public IEnumerator SpaceJumpSequenceIE() {

        mapDisclaimer.SetActive(false);
        warpShaderMesh.SetActive(true);
        warpNoiseShaderMesh.SetActive(true);

        warpSS.StartAnimating();
        warpNoiseSS.StartAnimating();

        warpRumble.Run();

        audioSource.PlayOneShot(audioStartWarp);

        // Wait for the jump animation to play out
        yield return new WaitForSeconds(5.1f);

        Debug.Log("Boosting");
        audioSource.PlayOneShot(audioWarp);

        spaceFighterCameraController.isWarping = true;

        warpSS.BoostWarpAnimation();
        warpNoiseSS.BoostWarpAnimation();

        yield return new WaitForSeconds(7f);

        solarSystemOrigin.SetActive(false);
        solarSystemDestination.SetActive(true);

        RenderSettings.skybox = destinationSkybox;

        // Teleport the player to the center
        playerShip.transform.localPosition = jumpFinishPosition.localPosition;
        playerShip.transform.localRotation = jumpFinishPosition.localRotation;

        shipCamera.transform.position = originalCameraPosition;
        shipCamera.transform.rotation = originalCameraRotation;


        spaceFighterCameraController.isWarping = false;

        warpSS.EndWarpAnimation();
        warpNoiseSS.EndWarpAnimation();

        SetDestToOrigin();

        yield return new WaitForSeconds(1f);
        // End coroutine
        warpShaderMesh.SetActive(false);
        warpNoiseShaderMesh.SetActive(false);
        jumpCoroutine = null;

        yield return new WaitForSeconds(1f);
        mapDisclaimer.SetActive(true);
    }

    void SetDestToOrigin() {
        solarSystemOrigin = solarSystemDestination;
        originSkybox = destinationSkybox;
    }

    public void SetupNextDestination(GameObject nextSolarSystem, Transform nextTransform, Material nextSkybox) {
        solarSystemDestination = nextSolarSystem;
        destinationSkybox = nextSkybox;
        jumpFinishPosition = nextTransform;
    }
}
