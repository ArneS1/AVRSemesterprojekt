using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionTableScript : MonoBehaviour
{
    public Text MissionTableText;
    public string ScannerExplanation;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        checkFlags();
    }

    private void checkFlags(){

        if(Gamestate.Instance.flag_scannerCollected){
            MissionTableText.text = ScannerExplanation.Replace("<br>", "\n");
            MissionTableText.lineSpacing = 1;
            MissionTableText.fontSize = 14;
            MissionTableText.supportRichText = true;
            MissionTableText.alignment = TextAnchor.UpperCenter;
        }
    }

}
