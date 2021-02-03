using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour
{
    // Start is called before the first frame update
    private float fog_intensity;
    public GameObject player;
    public GameObject water_surface;
    public GameObject Light;
    private float distanceToSurface;
    private int max_depth = -100;

    private float stand_fog_dens = 0.006f;

    private bool fogActive = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float playerY = player.transform.position.y;
        float waterY = water_surface.transform.position.y;
        distanceToSurface = Vector3.Distance(player.transform.position, water_surface.transform.position);

        if (fogActive)
        {
            calculateLightAndFog();
        }

        if (!fogActive && playerY < waterY) {
            activateFog();
        }

        if (fogActive && playerY > waterY)
        {
            deactivateFog();
        }



    }

    private void calculateLightAndFog()
    {
        calculateFogColor();
        calculateLightIntensity();
    }

    private void calculateLightIntensity()
    {

        Debug.Log("Distance light " + distanceToSurface);

        float intensity_max = 0.1f;

        Light.GetComponent<Light>().intensity = intensity_max - (0.001f * distanceToSurface);
    }

    private void calculateFogColor()
    {
    }

    public void activateFog()
    {
        fogActive = true;
        RenderSettings.fog = true;
    }

    public void deactivateFog()
    {
        fogActive = false;
        RenderSettings.fog = false;
        Light.GetComponent<Light>().intensity = 1;
    }

}
