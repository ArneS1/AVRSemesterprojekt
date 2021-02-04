using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerAI : MonoBehaviour
{
    public GameObject target;
    public float strength = .5f;
    public float speed = 0.01f;
    public List<GameObject> targets = new List<GameObject>();
    public GameObject DialogTarget;

    public bool isDialog = false;
    public bool isInDialog;

    public GameObject PlasticWaste;

    public int activeAudioClip = 0;

    public AudioSource audioSource;
    private bool isRoaming;

    public float distanceToPlayer;
    public GameObject boatPostion;
    public GameObject vulkanPosition;
    public bool boatSequenzActive = false;
    public bool vulkanSequenzActive = false;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        target = DialogTarget;
        if (speed == 0) {
            speed = Gamestate.Instance.BASIC_FISH_SPEED;
        }
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("at Goal und isDialog: " + isDialog);

        if (other.CompareTag("pathTarget"))
        {
            if (isDialog)
            {
                audioSource.Play();
                StartCoroutine(waitForSound(audioSource));

            } else if (isRoaming)
            {
                chooseRoamingPoint();
            }
        }
        
    }

    //Funktion die den Mainthread nicht blockiert und auf etwas wartet
    IEnumerator waitForSound(AudioSource source)
    {
        //Wait Until Sound has finished playing
        while (source.isPlaying)
        {
            yield return null;
        }
        //Audio is done, set inDialog false

        Debug.Log("vulkanistaktiv: " + vulkanSequenzActive);
   
        EndDialog();
    }

    internal void activateBoatSequenz()
    {
        vulkanSequenzActive = false;
        boatSequenzActive = true;
        startDialogAudio();

    }

    internal void activateVulkanSequenz()
    {
        boatSequenzActive = false;
        vulkanSequenzActive = true;
        startDialogAudio();

    }

    private void Move()
    {
        if (target.transform.position.y < -1)
        {
            Vector3 goalVector = target.transform.position - transform.position;
            
            if (goalVector != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(-goalVector);
                float str = Mathf.Min(strength * Time.deltaTime, 1);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
            }
            else
            {
                Quaternion targetRotation = Quaternion.LookRotation(goalVector);
                float str = Mathf.Min(strength * Time.deltaTime, 1);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
            }


            float additionalSpeed;
            if (boatSequenzActive || vulkanSequenzActive)
            {
                if (distanceToPlayer <= 20f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
                }
            } 
            else
            {
                if (distanceToPlayer >= 15f)
                {
                    additionalSpeed = 0.2f;
                }
                else
                {
                    additionalSpeed = 0f;
                }

                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed + additionalSpeed);
            }


        }
    }

    public void startDialogAudio()
    {
        speed = 0.2f;
        setDialogWaypoint();
        isDialog = true;
        isInDialog = true;
        isRoaming = false;   
    }

    

    private void startRoaming()
    {
        speed = 0.01f;
        Debug.Log("Turtle starts roaming");
        isRoaming = true;
        chooseRoamingPoint();
    }

    private void chooseRoamingPoint()
    {
        Debug.Log("got roaming point");
        GameObject newTarget = target;
        while (newTarget == target)
        {
            int r = Random.Range(0, targets.Count);
            newTarget = targets[r];
        }
        target = newTarget;
    }


    public void setDialogWaypoint()
    {
        target = DialogTarget;
    }

    public void EndDialog()
    {

        isInDialog = false;
        isDialog = false;
        Debug.Log("vulkanistaktiv: " + vulkanSequenzActive);
        if (boatSequenzActive)
        {
            target = boatPostion;
        }
        else if (vulkanSequenzActive)
        {

            target = vulkanPosition;
        } 
        else
        {
            startRoaming();
        }
    }

    public void ActivatePlasticWaste(){
        PlasticWaste.SetActive(true);
    }
    
}
