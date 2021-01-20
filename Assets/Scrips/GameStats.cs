using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{

    public int trash_collected = 0;
    public int different_fish_scanned = 0;
    public List<string> fishes_scanned = new List<string>();

    public GameObject newFish;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (trash_collected == 5)
        {
            newFish.SetActive(true);
        }
    }

    public void trashCollected(int n)
    {
        trash_collected += n;
    }

    public void addNewFish(string fish_name)
    {
        if (fishes_scanned.Contains(fish_name))
        {
            Debug.Log("Fisch bereis gescannt");
        } 
        else
        {
            fishes_scanned.Add(fish_name);
            different_fish_scanned++;
        }
    }
}
