using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip overwaterAmbient;
    public AudioClip underwaterAmbient;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > 0){
            audioSource.volume = Mathf.Clamp(transform.position.y, 0.1f, 0.7f);
            if(audioSource.clip != overwaterAmbient){
                audioSource.clip = overwaterAmbient;
                audioSource.Play();
            }
        } else if(audioSource.clip != underwaterAmbient){
            audioSource.volume = 1;
            audioSource.clip = underwaterAmbient;
            audioSource.Play();
        }
    }
}
