using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Scannable : XRSimpleInteractable
{
    public string nameForIndex;
    public string infoText;
    private InfoHandler infoHandler;
    //ScanState must include done state so it doesnt instantly restart.
    private int ScanState = 0; // 0 - not scanned; 1 - scanning; 2 - done;
    private float ScanEndTimeInMilliseconds;
    public int ScanTimeInSeconds;

    // Start is called before the first frame update
    void Start()
    {
        if(infoHandler == null){
            infoHandler = GameObject.Find("Scanner").GetComponentInChildren<InfoHandler>();
        }

        if(ScanTimeInSeconds == 0){
            ScanTimeInSeconds = Gamestate.Instance.SCAN_DURATION_IN_SECONDS;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(ScanState == 1){
            Debug.Log("Scanning for: " + (Time.time - ScanEndTimeInMilliseconds));
            if(Time.time >= ScanEndTimeInMilliseconds){
                ScanFinished();
            }
        }
    }

    protected override void OnHoverEntered(XRBaseInteractor interactor)
    {
        if (interactor.name.Equals("Left Hand - Scannerlaser"))
        {
            StartScanning();
        }
        
    }

    protected override void OnHoverExited(XRBaseInteractor interactor)
    {
        AbortScan();
    }

    private void StartScanning()
    {
        Debug.Log("Scan started");
        ScanState = 1;
        ScanEndTimeInMilliseconds = Time.time + ScanTimeInSeconds;

    }

    private void AbortScan()
    {
        ScanState = 0;
        infoHandler.disableInfoScreen();
    }

    private void ScanFinished()
    {
        ScanState = 2;

        //info for Scanner/UI
        infoHandler.setInfoText(infoText);
        infoHandler.enableInfoScreen();

        //info for Gamestate
        if(this.gameObject.CompareTag("Fish")){
            Gamestate.Instance.addFishToIndex(nameForIndex);
        }
        if(this.gameObject.CompareTag("Trash")){
            Gamestate.Instance.addTrashToIndex(nameForIndex);
        }
    }
        
}
