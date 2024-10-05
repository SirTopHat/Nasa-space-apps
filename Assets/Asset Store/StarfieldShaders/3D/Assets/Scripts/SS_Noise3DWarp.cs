using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using VSX.UniversalVehicleCombat;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class SS_Noise3DWarp : SS_AGenerator
{
	protected override string shaderName { get { return "Noise 3D Warp"; } }
    
    public bool animate = true;
    public Vector3 animationSpeed1 = new Vector3(1, -1, 3);
    public Vector3 animationSpeed2 = new Vector3(-1, 1, 5);
    public Vector3 offset1, offset2;

    // mesh
    [Range(4, 255)]
    public int detail = 50;
    public float distance = 100f;
    public bool centerMesh = true;

    // shader
    [Tooltip("_Opacity - Noise opacity.")]
    [Range(0f, 1f)]
    public float opacity = 1f;

    [Tooltip("_Gradient - Color gradient.")]
    public Gradient gradient = new Gradient();
    [Tooltip("_Blackout - Blackout.")]
    [Range(-1f, 50f)]
    public float blackout = 10f;
    [Range(0.1f, 1f)]
    public float noiseScale1 = 1;
    [Range(0.1f, 1f)]
    public float noiseScale2 = 1;

    private Coroutine animationCoroutine;

    private float RandomFloat { get { return Random.Range(-5000f, 0f); } }
    private Vector3 RandomVector { get { return new Vector3(RandomFloat, RandomFloat, RandomFloat); } }

    protected override void OnMaterialCreated() { }
    
    private void Update()
    {
        if (animate)
            Animate();
    }

    // Coroutine to animate the values
    public IEnumerator AnimateValues(float targetOpacity, float targetBlackout, float duration) {
        float initialOpacity = opacity;
        float initialBlackout = blackout;

        float timeElapsed = 0f;

        while (timeElapsed < duration) {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / duration;

            // Lerp the values based on the time elapsed
            opacity = Mathf.Lerp(initialOpacity, targetOpacity, t);
            blackout = Mathf.Lerp(initialBlackout, targetBlackout, t);

            // Apply the changes to the material
            SetShaderData();

            yield return null;
        }

        // Ensure the final values are set exactly at the end
        opacity = targetOpacity;
        blackout = targetBlackout;
        SetShaderData();
    }

    public void StartAnimating() {
        // Stop any ongoing animation coroutine
        if (animationCoroutine != null) {
            StopCoroutine(animationCoroutine);
        }

        // Start animating to the target values
        animationCoroutine = StartCoroutine(AnimateValues(0.3f, 0.7f, 5f));
    }

    public void BoostWarpAnimation() {
        if (animationCoroutine != null) {
            StopCoroutine(animationCoroutine);
        }

        // Start animating to the target values
        animationCoroutine = StartCoroutine(AnimateValues(0.9f, 3f, 0.5f));
    }

    public void EndWarpAnimation() {
        if (animationCoroutine != null) {
            StopCoroutine(animationCoroutine);
        }

        // Start animating to the target values
        animationCoroutine = StartCoroutine(AnimateValues(0f, -1f, 0.5f));
    }

    private void Animate()
    {
        offset1 += animationSpeed1 * Time.deltaTime * noiseScale1;
        offset2 += animationSpeed2 * Time.deltaTime * noiseScale2;
        MyMaterial.SetVector("_OffsetVector1", offset1);
        MyMaterial.SetVector("_OffsetVector2", offset2);
    }

    /// <summary>
    /// Also sets noise and color.
    /// </summary>
    public override void GenerateMesh()
    {
        distance = SS_Noise3DWarpMesh.ValidateDistance(detail, distance);
        MyMesh = SS_Noise3DWarpMesh.GenerateMesh(detail, distance, centerMesh);
    }

    public void SetSeed(int _seed) { seed = _seed; SetSeed(); }
    public void SetSeed()
    {
        Random.State prevState = Random.state;
        Random.InitState(seed);
        offset1 = RandomVector;
        offset2 = RandomVector;
        Material mat = ValidatedMaterial();
        mat.SetVector("_OffsetVector1", offset1);
        mat.SetVector("_OffsetVector2", offset2);
        Random.state = prevState;
    }

    public override void SetShaderData()
    {
        Material mat = ValidatedMaterial();
        mat.SetTexture("_Gradient", SS_GradientGenerator.GenerateGradient(gradient));
        mat.SetFloat("_Opacity", opacity);
        mat.SetFloat("_Blackout", blackout);
        mat.SetFloat("_NoiseScale1", noiseScale1);
        mat.SetFloat("_NoiseScale2", noiseScale2);
    }
}
