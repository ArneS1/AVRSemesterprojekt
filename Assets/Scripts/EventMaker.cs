﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMaker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckTrash();
    }

    private void CheckTrash()
    {
        if(Gamestate.Instance.getFishIndexCapacity() >= 3)
        {
            Vector3 turtlePosition = Camera.main.transform.forward * - 5;
            FishSpawner.Instance.SpawnTurtle(turtlePosition);

        }
    }
}
