using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoHandler : MonoBehaviour
{
    public GameObject infoScreen;
    private string infoText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setInfoText(string info){
        Debug.Log("INFO RECEIVED");
        infoText = info;
        infoScreen.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>().text = infoText;
    }

    public void enableInfoScreen(){
        infoScreen.SetActive(true);
    }

    public void disableInfoScreen(){
        infoScreen.SetActive(false);
    }

}
