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
    public GameObject Crab;
    public GameObject Octopus;

    //Turtle Kram
    public GameObject Turtle;
    private AudioSource turtleAudioSource;
    public List<AudioClip> Dialogs;
    private List<int> clipsPlayed = new List<int>();



    void Start()
    {
        FishSpawner.Instance.spawnFishes(standard_fish, 150);
        FishSpawner.Instance.spawnFishes(shark, 10);
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

        if(Gamestate.Instance.getFishIndex().Contains("Gewöhnliche Krake") && !clipsPlayed.Contains(2)){
            startTurtleDialog(2);
            Turtle.GetComponent<FollowerAI>().ActivatePlasticWaste();
            clipsPlayed.Add(2);
        }

        if(Gamestate.Instance.flag_playerIsAtShip && clipsPlayed.Contains(2) && !clipsPlayed.Contains(3)){
            startTurtleDialog(3);
            clipsPlayed.Add(3);
        }

        if(clipsPlayed.Contains(3) && !Turtle.GetComponent<FollowerAI>().PlasticWaste.activeInHierarchy && !clipsPlayed.Contains(4)){
            startTurtleDialog(4);
            clipsPlayed.Add(4);
        }
    }

    private void CheckTrash()
    {
        if(Gamestate.Instance.getFishIndexCapacity() >= 3 && !turtleSpawned)
        {
            Debug.Log("spawned");
            turtleSpawned = true;
            Vector3 turtlePosition = Camera.main.transform.forward * - 5;
            Turtle.transform.position = turtlePosition;
            Turtle.SetActive(true);
            Turtle.GetComponent<FollowerAI>().setDialogWaypoint(0);
            turtleAudioSource = Turtle.GetComponent<AudioSource>();
            startTurtleDialog(0);
            clipsPlayed.Add(0);
        }

        if(Gamestate.Instance.getFishIndexCapacity() >= 10 && !clipsPlayed.Contains(1))
        {
            startTurtleDialog(1);
            clipsPlayed.Add(1);
            FishSpawner.Instance.spawnFishes(Octopus, 10);
        }
    }

    private void startTurtleDialog(int clipFromList){
        if(!turtleAudioSource.isPlaying){
            turtleAudioSource.clip = Dialogs[clipFromList];
            Turtle.GetComponent<FollowerAI>().isDialog = true;
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
