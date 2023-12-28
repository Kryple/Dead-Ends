using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlickering : MonoBehaviour
{
    [SerializeField] private new Light2D light; // Reference to your SpotLight2D
    [SerializeField] private float minIntensity = 0.64f; // Minimum intensity during flicker
    [SerializeField] private float maxIntensity = 1.35f; // Maximum intensity during flicker
    [SerializeField] float flickerSpeed = 5f; // Flicker frequency (changes per second)

    private float flickerTimer; // Timer for controlling flicker cycle

    private void Start()
    {
        /*
         minIntensity = light.pointLightInnerRadius;
        maxIntensity = light.pointLightOuterRadius;
         */
    }

    void Update()
    {
        // Update flicker timer
        flickerTimer += Time.deltaTime * flickerSpeed;

        // Generate a random intensity based on timer and limits
        light.intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.PerlinNoise(flickerTimer, Time.time));
        
    }
}