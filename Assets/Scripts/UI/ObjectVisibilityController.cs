using System;
using UnityEngine;
using System.Collections;

public class ObjectVisibilityController : MonoBehaviour
{
    // Array to store references to all duplicated game objects
    public GameObject[] arrowChildren;


    void Start()
    {
        ToggleVisibility(false);
    }

    // Start is called before the first frame update
    // Function to initialize the visibility controller
    public void InitializeController()
    {
        // Toggle visibility to true immediately
        Console.WriteLine("setting vis to true");
        ToggleVisibility(true);

        // Start the coroutine to toggle visibility back to false after 5 seconds
        StartCoroutine(ToggleVisibilityAfterDelay(10f));
    }

    // Function to toggle visibility of duplicated objects
    public void ToggleVisibility(bool isVisible)
    {
        foreach (GameObject arrow in arrowChildren)
        {
            arrow.SetActive(isVisible);
        }
    }

    // Coroutine to toggle visibility after a delay
    IEnumerator ToggleVisibilityAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Toggle visibility back to false after the delay
        ToggleVisibility(false);
    }
}