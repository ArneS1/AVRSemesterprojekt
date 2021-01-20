using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishTextScript : MonoBehaviour
{
    public GameObject game_stats;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = "Fische entdeckt: " + game_stats.GetComponent<GameStats>().different_fish_scanned;
    }
}
