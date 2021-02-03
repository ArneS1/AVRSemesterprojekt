using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulkanDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Hand")){
            Gamestate.Instance.flag_playerReachedVulkan = true;
        }
    }
}
