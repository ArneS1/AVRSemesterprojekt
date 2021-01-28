﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ToolController : MonoBehaviour
{

    public XRController RightHand;
    public InputHelpers.Button button = InputHelpers.Button.None;

    public GameObject fishingNet;
    public GameObject pliers;

    private bool fishingNetEquipped = false;
    private bool pliersEquipped = false;

    private bool toolSwitched;

    private bool isTouchingBackpack = false;
    private bool isTouchingToolbelt = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        checkIfBackPackisGrabbed(); 
        checkIfToolbeltisGrabbed();


    }

    private void checkIfToolbeltisGrabbed()
    {
        if (RightHand.inputDevice.IsPressed(button, out bool pressed, RightHand.axisToPressThreshold) && !(toolSwitched))
        {
            if (pressed)
            {
                //Debug.Log("Button pressed");
                if (isTouchingToolbelt && !(pliersEquipped) && !(fishingNetEquipped) && Gamestate.Instance.flag_pliersCollected)
                {
                    pliers.SetActive(true);
                    pliersEquipped = true;
                    toolSwitched = true;

                }
                else if (isTouchingToolbelt)
                {
                    pliers.SetActive(false);
                    pliersEquipped = false;
                    toolSwitched = true;

                }
            }
        }
    }

    private void checkIfBackPackisGrabbed()
    {
        if (RightHand.inputDevice.IsPressed(button, out bool pressed, RightHand.axisToPressThreshold) && !(toolSwitched))
        {
            if (pressed)
            {
                //Debug.Log("Button pressed");
                if (isTouchingBackpack && !(fishingNetEquipped) && !(pliersEquipped)  && Gamestate.Instance.flag_fishingNetCollected)
                {
                    fishingNet.SetActive(true);
                    fishingNetEquipped = true;
                    toolSwitched = true;

                }
                else if (isTouchingBackpack)
                {
                    fishingNet.SetActive(false);
                    fishingNetEquipped = false;
                    toolSwitched = true;

                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Backpack")
        {
            isTouchingBackpack = true;

        } else if (other.tag == "Toolbelt"){

            isTouchingToolbelt = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name =="FishingNetItem")
        {
            if (RightHand.inputDevice.IsPressed(button, out bool pressed, RightHand.axisToPressThreshold) && !pliersEquipped)
            {
                if (pressed)
                {
                    Gamestate.Instance.flag_fishingNetCollected = true;
                    fishingNetEquipped = true;
                    fishingNet.SetActive(true);
                    other.gameObject.SetActive(false);
                }
            }
        }

        if (other.CompareTag("Pliers"))
        {
            if (RightHand.inputDevice.IsPressed(button, out bool pressed, RightHand.axisToPressThreshold) && !fishingNetEquipped)
            {
                if (pressed)
                {
                    Gamestate.Instance.flag_pliersCollected = true;
                    pliersEquipped = true;
                    pliers.SetActive(true);
                    other.gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Backpack")
        {
            isTouchingBackpack = false;
            toolSwitched = false;

        }
        else if (other.tag == "Toolbelt")
        {

            isTouchingToolbelt = false;
            toolSwitched = false;

        }
    }
}
