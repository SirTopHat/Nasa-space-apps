using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class SS_Starfield3DWarp : SS_AGeneratorStarfield
{
	protected override string shaderName { get { return "Starfield 3D Warp"; } }

    public bool animate = true;
    [Range(0f, 10f)]
    public float animationSpeed = 5;

    // mesh
    [Range(4, 255)]
    public int detailX = 50;
    [Range(4, 255)]
    public int detailY = 50;

    // shader
    [Tooltip("_Opacity - Star opacity.")]
    [Range(0f, 1f)]
    public float opacity = 1f;
    [Tooltip("_Warp - Stretch stars.")]
    [Range(0f, 1f)]
    public float warp = 0.5f;

    [Tooltip("_Size - Star size.")]
    [Range(0.1f, 2f)]
    public float size = 1f;
    [Tooltip("_SizeNoise - Star size noise.")]
    [Range(0f, 1f)]
    public float sizeNoise = 0.9f;
    [Tooltip("_MinSize - Minimum star size (when using noise).")]
    [Range(0f, 2f)]
    public float minSize = 0.1f;

    [Tooltip("_Bounds - Bounds triangle position.z (local).")]
    [Range(0f, 100f)]
    public float bounds = 20f;
    [Tooltip("_Offset - Bounds triangle position.z (local).")]
    [Range(0f, 999f)]
    public float offset;

    private Coroutine animationCoroutine;

    protected override void OnStart()
    {
        base.OnStart();
        SetMeshBounds();
        SetShaderData();
    }

    protected override void OnMaterialCreated() { }
    
    void Update()
    {
        Animate();
    }

    // Coroutine to animate the values
    public IEnumerator AnimateValues(float targetOpacity, float targetWarp, float targetSize, float duration) {
        float initialOpacity = opacity;
        float initialWarp = warp;
        float initialSize = size;

        float timeElapsed = 0f;

        while (timeElapsed < duration) {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / duration;

            // Lerp the values based on the time elapsed
            opacity = Mathf.Lerp(initialOpacity, targetOpacity, t);
            warp = Mathf.Lerp(initialWarp, targetWarp, t);
            size = Mathf.Lerp(initialSize, targetSize, t);

            // Apply the changes to the material
            SetShaderData();

            yield return null;
        }

        // Ensure the final values are set exactly at the end
        opacity = targetOpacity;
        warp = targetWarp;
        size = targetSize;
        SetShaderData();
    }

    public void StartAnimating() {
        // Stop any ongoing animation coroutine
        if (animationCoroutine != null) {
            StopCoroutine(animationCoroutine);
        }

        // Start animating to the target values
        animationCoroutine = StartCoroutine(AnimateValues(1f, 0.55f, 1f, 5f));
    }

    public void BoostWarpAnimation() {
        if (animationCoroutine != null) {
            StopCoroutine(animationCoroutine);
        }

        // Start animating to the target values
        animationCoroutine = StartCoroutine(AnimateValues(1f, 0.85f, 1.5f, 0.5f));
    }

    public void EndWarpAnimation() {
        if (animationCoroutine != null) {
            StopCoroutine(animationCoroutine);
        }

        // Start animating to the target values
        animationCoroutine = StartCoroutine(AnimateValues(0f, 0f, 0.1f, 0.5f));
    }

    private void Animate()
    {
        if (!animate)
            return;

        offset += Time.deltaTime * animationSpeed;
        if (offset > 999f)
            offset -= 999f;

        // ADDED BY ME
        //MyMaterial = GetComponent<Renderer>().material;
        // END OF MODIFICATION
        MyMaterial.SetFloat("_Offset", offset);
    }

    public void SetMeshBounds()
    {
        Mesh mesh = MyMesh;
        mesh.RecalculateBounds();
        mesh.bounds = new Bounds(Vector3.zero, new Vector3(mesh.bounds.size.x, mesh.bounds.size.y, ValidatedMaterial().GetFloat("_Bounds")));
    }

    /// <summary>
    /// Also sets noise and color.
    /// </summary>
    public override void GenerateMesh()
    {
        MyMesh = SS_Starfield3DWarpMesh.GenerateMesh(seed, detailX, detailY, gradient);
    }

    public override void SetShaderData()
    {
        // Original
        Material mat = ValidatedMaterial();

        // Modified
        //Material mat = MeshR.sharedMaterial;

        mat.SetFloat("_Opacity", opacity);
        mat.SetFloat("_Size", size);
        mat.SetFloat("_SizeNoise", sizeNoise);
        mat.SetFloat("_MinSize", minSize);
        mat.SetFloat("_Warp", warp);
        mat.SetFloat("_Bounds", bounds);
        mat.SetFloat("_Offset", offset);
        SetMeshBounds();
    }

    public override void SetNoise()
    {
        if (MyMesh == null)
            return;
        MyMesh.uv = SS_Starfield3DWarpMesh.GetWhiteNoise(seed, MyMesh.vertices.Length);
    }

    public override void SetColors()
    {
        if (MyMesh == null)
            return;
        MyMesh.colors = SS_Starfield3DWarpMesh.GetColors(seed, MyMesh.vertices.Length, gradient);
    }
}
