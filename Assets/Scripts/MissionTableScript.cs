using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionTableScript : MonoBehaviour
{
    public Text MissionTableText;
    public string Auftrag;
    private bool isMenuActive;
    private float MenuClosingTime;
    public float MenuActiveForSeconds;
    
    // Update is called once per frame
    void Update()
    {
        UnloadMenu();
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Scanner")){
            if(Gamestate.Instance.flag_firstFishScanned){
                LoadMenu();
            }
        }
    }

    public void UpdateText(string newText){
        MissionTableText.text = newText.Replace("<br>", "\n");
        MissionTableText.lineSpacing = 1;
        MissionTableText.fontSize = 14;
        MissionTableText.supportRichText = true;
        MissionTableText.alignment = TextAnchor.UpperCenter;    
    }

    private void LoadMenu(){
        isMenuActive = true;
        string menu = "Gescannte Lebewesen: (";
        menu += Gamestate.Instance.getFishIndexCapacity();
        menu += " von " + Gamestate.Instance.NumberOfFishToScan + ")<br>";
        foreach (var fish in Gamestate.Instance.getFishIndex())
        {
            menu += "<br>" + fish;
        }
        MenuClosingTime = Time.time + MenuActiveForSeconds;
        UpdateText(menu);
    }

    private void UnloadMenu(){
        if(Time.time >= MenuClosingTime && isMenuActive){
            isMenuActive = false;
            UpdateText(Auftrag); 
        }
    }

}
