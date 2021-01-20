using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MenuTrigger : MonoBehaviour
{

    public XRController LeftHand;
    public InputHelpers.Button button = InputHelpers.Button.None;
    private bool MenuActive = true;
    public GameObject Menu;

    private float lastPressed;
    private float buffer = 0.5f;
    private bool onCooldown = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!onCooldown)
        {

            if (LeftHand.inputDevice.IsPressed(button, out bool pressed, LeftHand.axisToPressThreshold))
            {
                if (pressed)
                {
                    /* Damit man den knopf nicht 1000x hintereinander drückt
                     * TODO: gibt es ein onButtonPress der den button nur einmal registriert??
                     */
                    lastPressed = Time.time;
                    onCooldown = true;
                }


                if (pressed && MenuActive)
                {
                    Menu.SetActive(false);
                    MenuActive = false;
                }
                else if (pressed)
                {
                    Menu.SetActive(true);
                    MenuActive = true;
                }
            }
        }
        else
        {
            if (Time.time - lastPressed > buffer)
            {
                onCooldown = false;
            }
        }
    }


    
}
