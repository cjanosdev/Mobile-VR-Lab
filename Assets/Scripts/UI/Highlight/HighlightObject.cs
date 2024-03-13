using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObject : MonoBehaviour
{
    public Material outlineMaterial; // Reference to your custom outline material

    void Start()
    {
        ApplyOutlineEffect();
    }

    public void ApplyOutlineEffect()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            // Clone the outline material to prevent modifying the original material
            Material[] materials = renderer.materials;
            Material[] outlineMaterials = new Material[materials.Length];

            for (int i = 0; i < materials.Length; i++)
            {
                outlineMaterials[i] = new Material(outlineMaterial);
            }

            // Apply the outline material to each submesh
            renderer.materials = outlineMaterials;
        }
        else
        {
            Debug.LogWarning("Renderer component not found on the object: " + gameObject.name);
        }
    }
}
