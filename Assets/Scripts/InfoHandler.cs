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
        infoScreen.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>().text = infoText.Replace("<br>", "\n");
        infoScreen.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>().lineSpacing = 1;
        infoScreen.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>().fontSize = 14;
        infoScreen.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>().supportRichText = true;
        infoScreen.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>().alignment = TextAnchor.UpperCenter;
    }

    public void enableInfoScreen(){
        infoScreen.SetActive(true);
    }

    public void disableInfoScreen(){
        infoScreen.SetActive(false);
    }

}
