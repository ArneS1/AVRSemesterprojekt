using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Spawner : MonoBehaviour
{
    // Singleton stuff
    private static Spawner instance = null;
    private static readonly object padlock = new object();

    public List<GameObject> newFish;
    public GameObject VulkanShells;

    public GameObject FishObjectCollector;
    public GameObject TrashObjectCollector;

    Spawner(){
        Debug.Log("Spawner created");
    }

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        }else{
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public static Spawner Instance
    {
        get
        {
            lock(padlock)
            {
                if(instance == null)
                {
                    instance = new Spawner();
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

    public void SpawnVulkanShells(){
        VulkanShells.SetActive(true);
    }

    public void spawnFishes(GameObject fish, int n)
    {
        for (int i = 0; i < n; i++)
        {
            GameObject newFish = Instantiate(fish, fish.GetComponent<UnderwaterAI>().chooseNewTarget(fish.GetComponent<UnderwaterAI>().chooseUnderwaterLevel()), Quaternion.identity);
            newFish.transform.parent = FishObjectCollector.transform;
            newFish.GetComponent<UnderwaterAI>().SpawnSelf();
        }
    }

    public void spawnTrash(GameObject trash, int n)
    {
        for (int i = 0; i < n; i++)
        {
            Instantiate(trash, trash.GetComponent<TrashSelector>().choosePointInArea(trash.GetComponent<TrashSelector>().areaToSpawn), Quaternion.identity).transform.parent = TrashObjectCollector.transform;
        }
    }
}
