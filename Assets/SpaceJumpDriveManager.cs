using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using VSX.UniversalVehicleCombat;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class SpaceJumpDriveManager : MonoBehaviour
{
    public GameObject playerShip;
    public GameObject shipCamera;

    public GameObject warpShaderMesh;
    public SS_Starfield3DWarp warpSS;
    public GameObject warpNoiseShaderMesh;
    public SS_Noise3DWarp warpNoiseSS;

    public GameObject solarSystemOrigin;
    public GameObject solarSystemDestination;

    public Transform jumpFinishPosition;

    public Material originSkybox;
    public Material destinationSkybox;


    public Vector3 originalCameraPosition;  // To store the original camera position
    public Quaternion originalCameraRotation;  // To store the original camera rotation
    private Coroutine jumpCoroutine;  // To track the running coroutine

    private void Start() {
        // Save the camera's current position and rotation
        originalCameraPosition = shipCamera.transform.position;
        originalCameraRotation = shipCamera.transform.rotation;

        originSkybox = RenderSettings.skybox;
    }

    private void Update() {
        if (Input.GetKeyUp(KeyCode.J)) {
            StartJumpSequence();
        }
    }

    public void StartJumpSequence() {

        if (jumpCoroutine != null) {
            StopCoroutine(jumpCoroutine);
        }

        jumpCoroutine = StartCoroutine(SpaceJumpSequenceIE());
    }

    public IEnumerator SpaceJumpSequenceIE() {

        warpShaderMesh.SetActive(true);
        warpNoiseShaderMesh.SetActive(true);

        warpSS.StartAnimating();
        warpNoiseSS.StartAnimating();

        // Wait for the jump animation to play out
        yield return new WaitForSeconds(5f);

        Debug.Log("Boosting");

        warpSS.BoostWarpAnimation();
        warpNoiseSS.BoostWarpAnimation();

        yield return new WaitForSeconds(3f);

        solarSystemOrigin.SetActive(false);
        solarSystemDestination.SetActive(true);

        RenderSettings.skybox = destinationSkybox;

        // Teleport the player to the center
        playerShip.transform.localPosition = jumpFinishPosition.localPosition;
        playerShip.transform.localRotation = jumpFinishPosition.localRotation;

        shipCamera.transform.position = originalCameraPosition;
        shipCamera.transform.rotation = originalCameraRotation;


        warpSS.EndWarpAnimation();
        warpNoiseSS.EndWarpAnimation();

        yield return new WaitForSeconds(1f);
        // End coroutine
        warpShaderMesh.SetActive(false);
        warpNoiseShaderMesh.SetActive(false);
        jumpCoroutine = null;
    }
}
