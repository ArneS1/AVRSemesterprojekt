using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Gamestate : MonoBehaviour
{
    // Singleton stuff
    private static Gamestate instance = null;
    private static readonly object padlock = new object();

    // Data to Set
    public int NumberOfFishToScan;

    // Flags
    public bool flag_scannerCollected;
    public bool flag_pliersCollected;
    public bool flag_fishingNetCollected;
    public bool flag_firstFishScanned;
    public bool flag_firstTabletScanned;
        
    // General Data
    public int SCAN_DURATION_IN_SECONDS = 3;

    // Progress List
    private List<string> FishIndex = new List<string>();

    private List<string> TrashIndex = new List<string>();
    public int TrashCollected;
    public int VulkanTrashCollected;



    Gamestate(){
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

    public static Gamestate Instance
    {
        get
        {
            lock(padlock)
            {
                if(instance == null)
                {
                    instance = new Gamestate();
                }
                return instance;
            }
        }
    }

    // Progress List Getter / Setter
    public void addFishToIndex(string name){
        if(!FishIndex.Contains(name)){
            FishIndex.Add(name);
            flag_firstFishScanned = true;
        }
    }
    public List<string> getFishIndex(){
        return FishIndex;
    }

    public int getFishIndexCapacity(){
        int counter = 0;
        foreach (var item in FishIndex)
        {
            counter++;
        }
        return counter;
    }

    public void addTrashToIndex(string name){
        if(!TrashIndex.Contains(name)){
            TrashIndex.Add(name);
        }
    }
    public List<string> getTrashIndex(){
        return TrashIndex;
    }

    public int getTrashIndexCapacity(){
        return TrashIndex.Capacity;
    }
}
