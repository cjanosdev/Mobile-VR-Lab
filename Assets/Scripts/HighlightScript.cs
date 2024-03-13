using System.Threading.Tasks;
using UnityEngine;

public class HighlightScript : MonoBehaviour
{
    public Material highlightMaterial; // Reference to the highlight material
    private Material defaultMaterial; // Original material of the object
    private Renderer renderer;

    private void Start()
    {
        // Get the renderer of the cube
        renderer = GetComponent<Renderer>();

        // Store the original material of the object
        defaultMaterial = renderer.material;
    }

    public Task HighlightFrontSide()
    {
        // Apply the highlight material to the cube
        renderer.material = highlightMaterial;
        return Task.CompletedTask;
    }

    public void Unhighlight()
    {
        // Revert to the default material
        renderer.material = defaultMaterial;
    }
}
