using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Hand")){
            Gamestate.Instance.flag_playerReachedShip = true;
        }
    }
}
