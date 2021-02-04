using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMaker : MonoBehaviour
{
    public GameObject VulkanSmoke;
    private bool menuInitialized;
    private bool turtleSpawned;

    public GameObject standard_fish;
    public int standardFish_number;
    public GameObject shark;
    public int shark_number;
    public GameObject Crab;
    public int Crab_number;
    public GameObject Octopus;
    public int Octopus_number;
    public GameObject Whale;
    public int Whale_number;
    public GameObject atlantischer_hering;
    public int atlantischer_hering_number;

    public GameObject random_trash_prefab;
    public int random_trash_number;

    public string FinalerAuftrag;


    //Turtle Kram
    public GameObject Turtle;
    private AudioSource turtleAudioSource;
    public List<AudioClip> Dialogs;
    private List<int> clipsPlayed = new List<int>();




    void Start()
    {
        Spawner.Instance.spawnFishes(standard_fish, standardFish_number);

        Spawner.Instance.spawnFishes(shark, shark_number);

        Spawner.Instance.spawnTrash(random_trash_prefab, random_trash_number);

        Spawner.Instance.spawnFishes(atlantischer_hering, atlantischer_hering_number);
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
            FindObjectOfType<MissionTableScript>().Auftrag = "Auftrag:<br> Scanne so viele Lebewesen wie möglich, damit das Institut Informationen über diese Stelle des Meeres erfährt.<br><br>Halte den Scanner an das Tablett um die Daten abzurufen.";
            UpdateTabletText("Auftrag:<br> Scanne so viele Lebewesen wie möglich, damit das Institut Informationen über diese Stelle des Meeres erfährt.<br><br>Halte den Scanner an das Tablett um die Daten abzurufen.");
        }



        if(Gamestate.Instance.getFishIndex().Contains("Gewöhnliche Krake") && !clipsPlayed.Contains(2)){
            startTurtleDialog(2);

            Turtle.GetComponent<FollowerAI>().activateBoatSequenz();
            Turtle.GetComponent<FollowerAI>().ActivatePlasticWaste();
            clipsPlayed.Add(2);
        }

        if(Gamestate.Instance.flag_playerReachedShip && clipsPlayed.Contains(2) && !clipsPlayed.Contains(3)){
            startTurtleDialog(3);
            clipsPlayed.Add(3);
        }

        if(clipsPlayed.Contains(3) && !Turtle.GetComponent<FollowerAI>().PlasticWaste.activeInHierarchy && !clipsPlayed.Contains(4)){
            startTurtleDialog(4);
            Turtle.GetComponent<FollowerAI>().activateVulkanSequenz();
            clipsPlayed.Add(4);
        }




        // Play mikroplastik
        if (Gamestate.Instance.TrashCollected == 1 && !clipsPlayed.Contains(5))
        {
            startTurtleDialog(5);
            clipsPlayed.Add(5);
        }

        // Play mikroplastik
        if (Gamestate.Instance.TrashCollected == 3 && !clipsPlayed.Contains(6))
        {
            startTurtleDialog(6);
            clipsPlayed.Add(6);
        }

        // Play mülleimer
        if (Gamestate.Instance.TrashCollected == 5 && !clipsPlayed.Contains(7))
        {
            startTurtleDialog(7);
            clipsPlayed.Add(7);
        }

        // Play plastik korallen
        if (Gamestate.Instance.TrashCollected == 7 && !clipsPlayed.Contains(8))
        {
            startTurtleDialog(8);
            clipsPlayed.Add(8);
        }

        // Play schildkröte verwechseln
        if (Gamestate.Instance.TrashCollected == 9 && !clipsPlayed.Contains(9))
        {
            startTurtleDialog(9);
            clipsPlayed.Add(9);
        }

        // Play was können tuen
        if (Gamestate.Instance.TrashCollected == 10 && !clipsPlayed.Contains(10))
        {
            startTurtleDialog(10);
            clipsPlayed.Add(10);
        }

        if(Gamestate.Instance.TrashCollected == 11 && !clipsPlayed.Contains(4) && Gamestate.Instance.getFishIndexCapacity() >= Gamestate.Instance.NumberOfFishToScan){
            FindObjectOfType<MissionTableScript>().Auftrag = FinalerAuftrag;
            UpdateTabletText(FinalerAuftrag);
            Spawner.Instance.spawnFishes(Whale, Whale_number);
        }

        

    }

    private void CheckTrash()
    {
        if(Gamestate.Instance.getFishIndexCapacity() >= 2 && !turtleSpawned)
        {
            Debug.Log("spawned");
            turtleSpawned = true;
            Vector3 turtlePosition = Camera.main.transform.forward * - 5;
            Turtle.transform.position = turtlePosition;
            Turtle.SetActive(true);
            turtleAudioSource = Turtle.GetComponent<AudioSource>();
            startTurtleDialog(0);
            clipsPlayed.Add(0);
        }

        if(Gamestate.Instance.TrashCollected >= 2 && !clipsPlayed.Contains(1))
        {
            startTurtleDialog(1);
            clipsPlayed.Add(1);
            Spawner.Instance.spawnFishes(Octopus, Octopus_number);
        }
    }

    private void startTurtleDialog(int clipFromList){
        StartCoroutine(waitForDialogToFinish(clipFromList));
    }

    IEnumerator waitForDialogToFinish(int clipFromList)
    {
        //Wait Until Sound has finished playing
        while (turtleAudioSource.isPlaying)
        {
            yield return null;
        }
        turtleAudioSource.clip = Dialogs[clipFromList];
        Turtle.GetComponent<FollowerAI>().startDialogAudio();

    }

    private void CheckVulkan(){
        if(Gamestate.Instance.VulkanTrashCollected == 2 ){
            Spawner.Instance.SpawnVulkanShells();
        }
        if(Gamestate.Instance.VulkanTrashCollected == 4 && !VulkanSmoke.activeInHierarchy){
            Spawner.Instance.spawnFishes(Crab, Crab_number);
            VulkanSmoke.SetActive(true);
        }
    }

    public void UpdateTabletText(string newText){
        FindObjectOfType<MissionTableScript>().UpdateText(newText);
        if(!newText.StartsWith("Gut")){
            InfoHandler.Instance.MessageWithLights("NEUER AUFTRAG! KOMM AN BOARD!");
        }
    }
}
