using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Scannable : XRSimpleInteractable
{
    public string infoText;
    private InfoHandler infoHandler;
    public Material highlightMaterial; // Shimmer around the fish while scanning
    public GameObject GameStats;

    // Start is called before the first frame update
    void Start()
    {
        if(infoHandler == null){
            infoHandler = GameObject.Find("Scanner").GetComponentInChildren<InfoHandler>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 protected override void OnHoverEntered(XRBaseInteractor interactor) {
        
        //kann auch sein das es interactor.name.Equals("Left Hand - Scannerlaser") sein müsste
        if (interactor.name.Equals("Left Hand - Scannerlaser"))
        {
            infoHandler.setInfoText(infoText);
            infoHandler.enableInfoScreen();
            GameStats.GetComponent<GameStats>().addNewFish(infoText);
        }

        
    }

    protected override void OnHoverExited(XRBaseInteractor interactor) {
        infoHandler.disableInfoScreen();
    }
        
}
