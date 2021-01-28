using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionTableScript : MonoBehaviour
{
    public Text MissionTableText;
    private string ScannerExplanation;
    
    // Start is called before the first frame update
    void Start()
    {
        ScannerExplanation = "Von nun an hast du diesen Scanner immer in der Hand. Benutze ihn mit der Trigger-Taste am linken Controller! Scanne zum Testen dieses Tablet 3 Sekunden lang.";
    }

    // Update is called once per frame
    void Update()
    {
        checkFlags();
    }

    private void checkFlags(){

        if(Gamestate.Instance.flag_scannerCollected){
            MissionTableText.text = ScannerExplanation;
        }
    }

}
