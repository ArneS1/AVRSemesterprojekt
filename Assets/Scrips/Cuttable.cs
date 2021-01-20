using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuttable : MonoBehaviour
{
    private GameObject self;
    // Start is called before the first frame update
    void Start()
    {
        self = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Plier")){

            Debug.Log("I was cut by a Plier.");
            self.SetActive(false);

        }
    }
}
