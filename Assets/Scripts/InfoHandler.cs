using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class InfoHandler : MonoBehaviour
{
    // Singleton variables
    private static InfoHandler instance = null;
    private static readonly object padlock = new object();

    public GameObject infoScreen;
    private string infoText;

    // Scanning Progress Bar
    public List<GameObject> ProgressLights;
    public Material ProgressOff;
    public Material ProgressRed;
    public Material ProgressGreen;
    private bool isScanning;
    private float ScanEndTime;
    private float ScanStartTime;
    private float ScanTimePartial; // 1/7 der scanzeit -> 6 lampen + alle grün


    // Singleton functions
    InfoHandler(){
        Debug.Log("Gamestate created");
    }

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        }else{
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public static InfoHandler Instance
    {
        get
        {
            lock(padlock)
            {
                if(instance == null)
                {
                    instance = new InfoHandler();
                }
                return instance;
            }
        }
    }

    void Update(){
        if(isScanning){
            UpdateScanProgress();
        }
    }

    public void setInfoText(string info){
        Debug.Log("INFO RECEIVED");
        infoText = info;
        infoScreen.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>().text = infoText.Replace("<br>", "\n");
        infoScreen.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>().lineSpacing = 1;
        infoScreen.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>().fontSize = 36;
        infoScreen.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>().supportRichText = true;
        infoScreen.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>().alignment = TextAnchor.UpperCenter;
    }

    public void StartScan(int ScanTimeInSeconds){
        isScanning = true;
        ScanStartTime = Time.time;
        ScanEndTime = ScanStartTime + ScanTimeInSeconds;
        ScanTimePartial = ScanTimeInSeconds / 7.0f;

        Debug.Log("Scan Time in Seconds Time: " + ScanTimeInSeconds);
        Debug.Log("berechnung: " + ScanTimeInSeconds + " / 7 = " + (ScanTimeInSeconds / 7.0f));
        Debug.Log("Partial Time: " + ScanTimePartial);
    }

    public void UpdateScanProgress(){
        //Debug.Log("Scan End Time: " + ScanEndTimeInMilliseconds);
        //Debug.Log("Time: " + Time.time);

        float RemainingScanTime = ScanEndTime - Time.time;
        //Debug.Log("Time remaining: " + RemainingScanTime);

        int partials = Mathf.FloorToInt(RemainingScanTime / ScanTimePartial);

        Debug.Log("Scan Progress Partial: " + partials);

        switch(partials)
        {
            case 0:
                ColorAllProgressLamps(ProgressGreen);
                EndScan();
                break;
            case 6:
                ProgressLights[0].GetComponent<Renderer>().material = ProgressRed;
                break;
            case 5:
                ProgressLights[1].GetComponent<Renderer>().material = ProgressRed;
                break;
            case 4:
                ProgressLights[2].GetComponent<Renderer>().material = ProgressRed;
                break;
            case 3:
                ProgressLights[3].GetComponent<Renderer>().material = ProgressRed;
                break;
            case 2:
                ProgressLights[4].GetComponent<Renderer>().material = ProgressRed;
                break;
            case 1:
                ProgressLights[5].GetComponent<Renderer>().material = ProgressRed;
                break;   
        }

    }

    private void ColorAllProgressLamps(Material material)
    {
        foreach (GameObject light in ProgressLights)
        {
            light.GetComponent<Renderer>().material = material;
        }
    }

    private void EndScan()
    {
        ColorAllProgressLamps(ProgressGreen);
    }

    public void AbortScan()
    {
        isScanning = false;
        //TODO: rot blinken
        ColorAllProgressLamps(ProgressOff);
    }

}
