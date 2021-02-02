using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMaker : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject VulkanSmoke;
    private bool menuInitialized;
    void Start()
    {
        
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

    public void UpdateTabletText(string newText){
        FindObjectOfType<MissionTableScript>().UpdateText(newText);
    }
}
