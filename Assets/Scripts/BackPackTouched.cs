using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class BackPackTouched : MonoBehaviour
{
    // Start is called before the first frame update

    private bool isTouchingBackpack = false;
    public XRController RightHand;
    public InputHelpers.Button button = InputHelpers.Button.None;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (RightHand.inputDevice.IsPressed(button, out bool pressed, RightHand.axisToPressThreshold))
        {
            if (pressed)
            {
                Debug.Log("Button pressed");

                if (isTouchingBackpack)
                {
                    Debug.Log("Kescher gegriffen");
                }
            }

        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hand")
        {
            isTouchingBackpack = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Hand")
        {
            isTouchingBackpack = false;
        }
        else
        {
            Debug.Log("Backpack Collided");
        }
    }
}
