using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        Debug.Log("Das Boat Collidet am been mit: " + other.gameObject.tag);
        if(other.gameObject.CompareTag("Hand")){
            Gamestate.Instance.flag_playerReachedShip = true;
        }
    }
}
