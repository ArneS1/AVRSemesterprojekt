using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Scannable : XRSimpleInteractable
{
    public string nameForIndex;
    public string infoText;

    //ScanState must include done state so it doesnt instantly restart.
    private int ScanState = 0; // 0 - not scanned; 1 - scanning; 2 - done;
    private float ScanEndTimeInMilliseconds;
    public int ScanTimeInSeconds;

    // Start is called before the first frame update
    void Start()
    {
        if(ScanTimeInSeconds == 0){
            ScanTimeInSeconds = Gamestate.Instance.SCAN_DURATION_IN_SECONDS;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(ScanState == 1){
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
        InfoHandler.Instance.StartScan(ScanTimeInSeconds);

    }

    private void AbortScan()
    {
        ScanState = 0;
        InfoHandler.Instance.AbortScan();
    }

    private void ScanFinished()
    {
        ScanState = 2;

        //info for Scanner/UI
        InfoHandler.Instance.setInfoText(infoText);

        //info for Gamestate
        if(this.gameObject.CompareTag("Fish")){
            Gamestate.Instance.addFishToIndex(nameForIndex);
        }
        if(this.gameObject.CompareTag("Trash")){
            Gamestate.Instance.addTrashToIndex(nameForIndex);
        }
        if(this.gameObject.CompareTag("Tablet") && !Gamestate.Instance.flag_firstTabletScanned){
            Gamestate.Instance.flag_firstTabletScanned = true;
            this.infoText = "Dein Missions-Tablet. <br>Wenn hier kein Auftrag steht, erforsche das Meer!<br>Es gibt viel zu entdecken.";
            FindObjectOfType<EventMaker>().UpdateTabletText("Sehr Gut! <br><br>Oben auf deinem Scanner ist ein Display mit Informationen zu dem gescannten Lebewesen.<br><br> Tauche ab und scanne deinen ersten Fisch!<br><br>Komme danach erstmal wieder an Board.");
        }
    }
        
}
