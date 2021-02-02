using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionTableScript : MonoBehaviour
{
    public Text MissionTableText;
    private bool isMenuActive;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: wenn menu 120s lang an ist ausmachen
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
        UpdateText(menu);
    }

    private void UnloadMenu(){
        isMenuActive = false;
        UpdateText("Aktiver Auftrag:<br> Scanne so viele Lebewesen wie möglich, damit das Institut Informationen über diese Stelle des Meeres erfährt.<br><br>Halte den Scanner an das Tablett um die Daten abzurufen.");
    }

}
