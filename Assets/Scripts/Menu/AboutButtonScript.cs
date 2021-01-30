using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AboutButtonScript : MonoBehaviour
{
    public Canvas MenuCanvas;
    public string AboutText;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Hand")){
            MenuCanvas.GetComponentInChildren<Text>().text = AboutText.Replace("<br>", "\n");
            MenuCanvas.GetComponentInChildren<Text>().lineSpacing = 1;
            MenuCanvas.GetComponentInChildren<Text>().fontSize = 14;
            MenuCanvas.GetComponentInChildren<Text>().supportRichText = true;
            MenuCanvas.GetComponentInChildren<Text>().alignment = TextAnchor.UpperCenter;
        }
    }
}
