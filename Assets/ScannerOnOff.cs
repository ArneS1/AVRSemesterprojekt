using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScannerOnOff : MonoBehaviour
{
    public GameObject scanner;
    public XRController LeftHand;
    public InputHelpers.Button button = InputHelpers.Button.None;

    private bool previousPress = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(LeftHand.inputDevice.IsPressed(button, out bool pressed, LeftHand.axisToPressThreshold)){
            if(previousPress != pressed){
                previousPress = pressed;

                if(pressed){
                    ScannerOn();
                } else {
                    ScannerOff();
                }

            }
        }
    }

    public void ScannerOn(){
        scanner.SetActive(true);
    }

    public void ScannerOff(){
        scanner.SetActive(false);
    }
}
