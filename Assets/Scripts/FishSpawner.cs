using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class FishSpawner : MonoBehaviour
{
    // Singleton stuff
    private static FishSpawner instance = null;
    private static readonly object padlock = new object();

    public List<GameObject> newFish;

    public GameObject VulkanCrabs;
    public GameObject VulkanShells;

    public GameObject turtle;

    FishSpawner(){
        Debug.Log("FishSpawner created");
    }

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        }else{
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public static FishSpawner Instance
    {
        get
        {
            lock(padlock)
            {
                if(instance == null)
                {
                    instance = new FishSpawner();
                }
                return instance;
            }
        }
    }


    public void SpawnNewFish(Vector3 position){
        if(newFish.Count > 0){
            GameObject fishToSpawn = Instantiate(newFish[0]) as GameObject;
            fishToSpawn.transform.position = position;
            newFish.RemoveAt(0);
        } else {
            Debug.Log("no new Fish to Spawn");
        }
    }

    public void SpawnTurtle(Vector3 position){
        turtle.transform.position = position;
        turtle.SetActive(true);
    }

    public void SpawnVulkanCrabs(){
        VulkanCrabs.SetActive(true);
    }

    public void SpawnVulkanShells(){
        VulkanShells.SetActive(true);
    }
}
