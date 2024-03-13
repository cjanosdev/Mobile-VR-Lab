using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class SubMeshHelper : MonoBehaviour
    {
        public Material outlineMaterial; // Reference to your custom outline material

        void Start()
        {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

            // Iterate through each material in the mesh renderer's materials array
            for (int i = 0; i < meshRenderer.materials.Length; i++)
            {
                // Assign the outline material to each material in the array
                meshRenderer.materials[i] = outlineMaterial;
            }
        }
    }
}
