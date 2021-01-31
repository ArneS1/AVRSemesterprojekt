using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour
{
    // Start is called before the first frame update
    private float fog_intensity;
    private Color color;
    public GameObject player;
    public GameObject water_surface;

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

        if (!fogActive && playerY < waterY) {
            activateFog();
        }

        if (fogActive && playerY > waterY)
        {
            deactivateFog();
        }

        RenderSettings.fogDensity = stand_fog_dens + calculateDensity(playerY, waterY) ;
    }

    private float calculateDensity(float playerY, float waterY)
    {
        int divider = -40000;

        float distance = (waterY - playerY);
        float fog_dens = distance / divider;
        return Math.Abs(fog_dens);

        //TODO: Farbe vom Fog ändern oder light intensity runter drehen?
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
    }

}
