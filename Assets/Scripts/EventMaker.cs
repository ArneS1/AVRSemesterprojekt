using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMaker : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject VulkanSmoke;
    private bool menuInitialized;
    private bool turtleSpawned;

    public GameObject standard_fish;
    public GameObject shark;
    private GameObject turtle;
    public GameObject Crab;

    void Start()
    {
        FishSpawner.Instance.spawnFishes(standard_fish, 100);
        FishSpawner.Instance.spawnFishes(shark, 5);
    }

    // Update is called once per frame
    void Update()
    {
        CheckFish();
        CheckTrash();
        CheckVulkan();
    }

    private void CheckFish(){
        if(Gamestate.Instance.flag_firstFishScanned && !menuInitialized){
            menuInitialized = true;
            UpdateTabletText("Aktiver Auftrag:<br> Scanne so viele Lebewesen wie möglich, damit das Institut Informationen über diese Stelle des Meeres erfährt.<br><br>Halte den Scanner an das Tablett um die Daten abzurufen.");
        }
    }

    private void CheckTrash()
    {
        if(Gamestate.Instance.getFishIndexCapacity() >= 3 && !turtleSpawned)
        {
            turtleSpawned = true;
            Vector3 turtlePosition = Camera.main.transform.forward * - 5;
            turtle = FishSpawner.Instance.SpawnTurtle(turtlePosition);
            turtle.GetComponent<FollowerAI>().setDialogWaypoint(0);
        }
    }





    private void CheckVulkan(){
        if(Gamestate.Instance.VulkanTrashCollected == 2 ){
            FishSpawner.Instance.SpawnVulkanShells();
        }
        if(Gamestate.Instance.VulkanTrashCollected == 4 && !VulkanSmoke.activeInHierarchy){
            FishSpawner.Instance.spawnFishes(Crab, 10);
            VulkanSmoke.SetActive(true);
        }
    }

    public void UpdateTabletText(string newText){
        FindObjectOfType<MissionTableScript>().UpdateText(newText);
    }
}
