using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCollector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //When Trash enters the Trashbin
    private void OnTriggerEnter(Collider other){

        if(other.gameObject.CompareTag("Trash")){

            other.gameObject.SetActive(false);
            Debug.Log("Trash Collected.");
            Gamestate.Instance.TrashCollected += 1;
        }
    }
}
