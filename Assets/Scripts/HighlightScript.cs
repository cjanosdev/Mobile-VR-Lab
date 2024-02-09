using UnityEngine;

public class HighlightScript : MonoBehaviour
{
    public Material highlightMaterial; // Reference to the highlight material
    private Material defaultMaterial; // Original material of the object
    private Renderer cubeRenderer;
    public GameObject cube;

    private void Start()
    {
        // Get the renderer of the cube
        cubeRenderer = GetComponent<Renderer>();

        // Store the original material of the object
        defaultMaterial = cubeRenderer.material;
    }

    public void HighlightFrontSide()
    {
        // Apply the highlight material to the cube
        cubeRenderer.material = highlightMaterial;
    }

    public void Unhighlight()
    {
        // Revert to the default material
        cubeRenderer.material = defaultMaterial;
    }
}
