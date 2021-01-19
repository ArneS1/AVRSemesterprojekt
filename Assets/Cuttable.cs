using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuttable : MonoBehaviour
{
    public GameObject self;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collided with" + other.tag);
        
        if(other.gameObject.CompareTag("Plier")){

            Debug.Log("I was cut by a Plier.");
            self.SetActive(false);

        }
    }
}
