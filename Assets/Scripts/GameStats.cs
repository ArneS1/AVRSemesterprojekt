using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamestate.Instance.TrashCollected == 5)
        {
            Spawner.Instance.SpawnNewFish(new Vector3(2, -18, 5));
        }
    }
}
