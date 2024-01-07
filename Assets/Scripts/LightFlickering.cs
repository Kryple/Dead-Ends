using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlickering : MonoBehaviour
{
    [SerializeField] private new Light2D light; // Reference to your SpotLight2D
     private float minIntensity = 0.64f; // Minimum intensity during flicker
    private float maxIntensity = 1.35f; // Maximum intensity during flicker
    private float _flickerSpeed = 0.5f; // Flicker frequency (changes per second)

    private float _radiusFlickerSpeed = 0.25f;
    
    private float _maxRadius;
    private float _minRadius;
    private float _minRadius2;

    private float _flickerTimer; // Timer for controlling flicker cycle
    private float _radiusFlickerTimer; // Timer for controlling flicker cycle of the light's radius

    private void Start()
    {
        _maxRadius = light.pointLightOuterRadius;
        _minRadius = light.pointLightInnerRadius + 1.5f;
        _minRadius2 = light.pointLightInnerRadius - 1f;
        
         
        
        
    }

    void Update()
    {
        // Update flicker timer
        _flickerTimer += Time.deltaTime * _flickerSpeed;
        _radiusFlickerTimer += Time.deltaTime * _radiusFlickerSpeed;
        

        // Generate a random intensity based on timer and limits
        light.intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.PerlinNoise(_flickerTimer, Time.time));
        light.pointLightOuterRadius =
            Mathf.Lerp(_minRadius, _maxRadius, Mathf.PerlinNoise(_radiusFlickerTimer, Time.deltaTime));
    }
}