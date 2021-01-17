using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Scannable : XRSimpleInteractable
{
    private bool showInfo = false;

    public GameObject info;
    public Material highlightMaterial; // Shimmer around the fish while scanning

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnHoverEntered(XRBaseInteractor interactor) {
        Debug.Log("Fish Scanned!");
        enableInfo();
    }

    protected override void OnHoverExited(XRBaseInteractor interactor) {
        Debug.Log("Fish Not Scanned!");
        disableInfo();
    }

    private void enableInfo(){
        showInfo = true;
        info.SetActive(true);
    }

    private void disableInfo(){
        showInfo = false;
        info.SetActive(false);
    }
        
}
