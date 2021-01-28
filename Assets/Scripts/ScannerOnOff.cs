using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScannerOnOff : MonoBehaviour
{
    public GameObject ScannerLaser;
    public GameObject ScannerToCollect;
    public GameObject ScannerLight;
    public XRController LeftHand;

    public GameObject ScannerTool;
    public InputHelpers.Button TriggerButton = InputHelpers.Button.None;
    public InputHelpers.Button GrabButton = InputHelpers.Button.None;

    private bool previousPress = false;
    // Start is called before the first frame update
    void Start()
    {
        ScannerTool.SetActive(Gamestate.Instance.flag_scannerCollected);
        ScannerLight.SetActive(Gamestate.Instance.flag_scannerCollected);
    }

    // Update is called once per frame
    void Update()
    {
        if(LeftHand.inputDevice.IsPressed(TriggerButton, out bool pressed, LeftHand.axisToPressThreshold)){
            if(previousPress != pressed){
                previousPress = pressed;

                if(pressed && Gamestate.Instance.flag_scannerCollected){
                    ScannerOn();
                } else {
                    ScannerOff();
                }

            }
        }
    }

    public void ScannerOn(){
        ScannerLaser.SetActive(true);
    }

    public void ScannerOff(){
        ScannerLaser.SetActive(false);
    }

    public void ShowScannerTool(){
        ScannerToCollect.SetActive(false);
        ScannerTool.SetActive(true);
        ScannerLight.SetActive(true);
        Gamestate.Instance.flag_scannerCollected = true;
    }
}
