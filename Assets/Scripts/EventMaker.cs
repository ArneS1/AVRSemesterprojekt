using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMaker : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject VulkanSmoke;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckTrash();
        CheckVulkan();
    }

    private void CheckTrash()
    {
        if(Gamestate.Instance.getFishIndexCapacity() >= 3)
        {
            Vector3 turtlePosition = Camera.main.transform.forward * - 5;
            FishSpawner.Instance.SpawnTurtle(turtlePosition);

        }
    }

    private void CheckVulkan(){
        if(Gamestate.Instance.VulkanTrashCollected == 2 ){
            FishSpawner.Instance.SpawnVulkanShells();
        }
        if(Gamestate.Instance.VulkanTrashCollected == 4){
            FishSpawner.Instance.SpawnVulkanCrabs();
            VulkanSmoke.SetActive(true);
        }
    }
}
