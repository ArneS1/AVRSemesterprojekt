using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Gamestate : MonoBehaviour
{
    // Singleton stuff
    private static Gamestate instance = null;
    private static readonly object padlock = new object();

    // Flags
    public bool flag_scannerCollected;
    public bool flag_pliersCollected;
    public bool flag_fishingNetCollected;
        
    // General Data
    public int SCAN_DURATION_IN_SECONDS = 3;

    // Progress List
    private List<string> FishIndex = new List<string>();

    private List<string> TrashIndex = new List<string>();
    public int TrashCollected;

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

    //Progress List Getter / Setter
    public void addFishToIndex(string name){
        if(!FishIndex.Contains(name)){
            FishIndex.Add(name);
        }
    }
    public List<string> getFishIndex(){
        return FishIndex;
    }

    public int getFishIndexCount(){
        return FishIndex.Count;
    }

    public void addTrashToIndex(string name){
        if(!TrashIndex.Contains(name)){
            TrashIndex.Add(name);
        }
    }
    public List<string> getTrashIndex(){
        return TrashIndex;
    }

    public int getTrashIndexCount(){
        return TrashIndex.Count;
    }
}
