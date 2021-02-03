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
    public Material ProgressMessageReceived;
    private bool isScanning;
    public bool infoReceived;
    private float ScanEndTime;
    private float ScanStartTime;
    private float ScanTimePartial; // 1/7 der scanzeit -> 6 lampen + alle grün

    // Sound
    public AudioSource audioSource;


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
        infoScreen.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>().fontSize = 40;
        infoScreen.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>().supportRichText = true;
        infoScreen.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>().alignment = TextAnchor.MiddleCenter;
    }

    public void StartScan(int ScanTimeInSeconds){
        isScanning = true;
        ScanStartTime = Time.time;
        ScanEndTime = ScanStartTime + ScanTimeInSeconds;
        ScanTimePartial = ScanTimeInSeconds / 7.0f;
        infoReceived = false;
    }

    public void UpdateScanProgress(){

        float RemainingScanTime = ScanEndTime - Time.time;
        int partials = Mathf.FloorToInt(RemainingScanTime / ScanTimePartial);

        switch(partials)
        {
            case 5:
                ProgressLights[0].GetComponent<Renderer>().material = ProgressRed;
                break;
            case 4:
                ProgressLights[1].GetComponent<Renderer>().material = ProgressRed;
                break;
            case 3:
                ProgressLights[2].GetComponent<Renderer>().material = ProgressRed;
                break;
            case 2:
                ProgressLights[3].GetComponent<Renderer>().material = ProgressRed;
                break;
            case 1:
                ProgressLights[4].GetComponent<Renderer>().material = ProgressRed;
                break;
            case 0:
                ProgressLights[5].GetComponent<Renderer>().material = ProgressRed;
                break;
            case -1:
                EndScan();
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
        if(!infoReceived){
            audioSource.Play();
            infoReceived = true;
            ColorAllProgressLamps(ProgressGreen);
        }
    }

    public void AbortScan()
    {
        isScanning = false;
        //TODO: rot blinken
        ColorAllProgressLamps(ProgressOff);
        infoReceived = false;
    }

    public void ResetInfoText(){
        setInfoText("Nothing Scanned");
        infoReceived = false;
    }

    public void MessageWithLights(string message){
        ColorAllProgressLamps(ProgressMessageReceived);
        setInfoText(message);
    }

}
