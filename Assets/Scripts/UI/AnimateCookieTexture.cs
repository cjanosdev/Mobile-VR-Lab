using UnityEngine;
using System.Collections;

public class AnimateCookieTexture : MonoBehaviour
{

    public enum AnimMode
    {
        forwards,
        backwards,
        random
    }

    private Texture2D[] textures;
    public int textureCount = 120;
    public float fps = 15;

    public AnimMode animMode = AnimMode.forwards;

    private int frameNr = 0;
    private Light cLight;

    void Start()
    {
        if (!TryGetComponent<Light>(out cLight))
        {
            Debug.LogWarning("AnimateCookieTexture: No light found on this gameObject", this);
            enabled = false;
        }

        textures = new Texture2D[textureCount];

        for (int i = 1; i <= textureCount; i++)
        {
            textures[i - 1] = Resources.Load("Caustics/CausticsExampel_" + i.ToString("D3")) as Texture2D;
        }

        StartCoroutine(SwitchCookie());
    }

    IEnumerator SwitchCookie()
    {
        while (true)
        {
            cLight.cookie = textures[frameNr];

            yield return new WaitForSeconds(1.0f / fps);

            switch (animMode)
            {
                case AnimMode.forwards: frameNr++; if (frameNr >= textures.Length) frameNr = 0; break;
                case AnimMode.backwards: frameNr--; if (frameNr < 0) frameNr = textures.Length - 1; break;
                case AnimMode.random: frameNr = Random.Range(0, textures.Length); break;
            }
        }
    }

}
