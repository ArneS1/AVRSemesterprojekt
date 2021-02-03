using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerAI : MonoBehaviour
{
    private GameObject target;
    public float  strength = .5f;
    public float speed;
    public List<GameObject> targets = new List<GameObject>();
    public GameObject DialogTarget;

    public bool isDialog;
    private bool isInDialog;

    public GameObject PlasticWaste;
    
    public List<AudioClip> audioClips = new List<AudioClip>();
    public int activeAudioClip = 0;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        target = DialogTarget;
        if(speed == 0){
            speed = Gamestate.Instance.BASIC_FISH_SPEED;
        }
    }

    // Update is called once per frame
    void Update()
    {

        Move();

    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("other.CompareTag(pathTarget)" + other.CompareTag("pathTarget"));
        if (other.CompareTag("pathTarget") && !isDialog)
        {
            ChooseNewTarget();
        } 
        else if (other.CompareTag("pathTarget") && isDialog)
        {
            startDialogAudio();
        }
    }



    private void Move()
    {
        if(target.transform.position.y < -1 )
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

            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);

        }
    }

    public void startDialogAudio()
    {
        isInDialog = true;
        //Debug.Log("Play Audio sound");
        //audioSource.clip = audioClips[activeAudioClip];
        //audioSource.Play();
        StartCoroutine(waitForSound(audioSource));

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
        isInDialog = false;
        isDialog = false;
    }

    private void ChooseNewTarget()
    {
        GameObject newTarget = target;
        while (newTarget == target)
        {
            int r = Random.Range(0, targets.Count);
            newTarget = targets[r];
        }
        target = newTarget;
    }

    public void setDialogWaypoint(int clip)
    {
        activeAudioClip = clip;
        isDialog = true;
        target = DialogTarget;
    }

    public void EndDialog()
    {
        isDialog = false;
        ChooseNewTarget();
    }

    public void ActivatePlasticWaste(){
        PlasticWaste.SetActive(true);
    }
    
}
