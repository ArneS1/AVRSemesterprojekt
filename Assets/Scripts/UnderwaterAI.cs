using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class UnderwaterAI : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject fish_prefab;
    public GameObject target;

    public float strength = .5f;
    public float speed;

    public bool OceanFloor;

    public bool Hunter;
    public string PreyName;
    private bool isHunting;
    public List<GameObject> underwaterArea = new List<GameObject>();

    public GameObject WaypointCollector;



    void Start()
    {
        if (speed <= 0)
        {
            speed = Gamestate.Instance.BASIC_FISH_SPEED;
        }
        chooseUnderwaterLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 goalVector = target.transform.position - transform.position;
            if (goalVector != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(goalVector);
                float str = Mathf.Min(strength * Time.deltaTime, 1);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
            }
            else
            {
                chooseUnderwaterLevel();
            }


            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
            if (OceanFloor)
            {

                transform.position = new Vector3(
                    transform.position.x,
                    Terrain.activeTerrain.SampleHeight(transform.position) + Terrain.activeTerrain.GetPosition().y,
                    transform.position.z);
            }
        }


    }

    public void SpawnSelf(){
        if (fish_prefab != null)
        {
            GameObject newFish = Instantiate(fish_prefab, transform.position, Quaternion.identity);
            newFish.transform.parent = transform;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Terrain":
                chooseUnderwaterLevel();
                break;
            case "pathTarget":
                chooseUnderwaterLevel();
                break;
            case "Fish":
                if (isHunting)
                {
                    isHunting = false;
                    speed /= 3;
                    other.gameObject.SetActive(false);
                }
                break;
        }
    }

    public GameObject chooseUnderwaterLevel()
    {
        if (Hunter && !isHunting)
        {
            float randomHunt = Random.Range(0.00f, 1.00f);
            if (randomHunt <= Gamestate.Instance.HUNTER_CHANCE_TO_HUNT)
            {
                isHunting = true;
                speed *= 3;
                choosePrey();
            }
            else
            {
                if (underwaterArea.Count > 1)
                {
                    int r = Random.Range(0, underwaterArea.Count);
                    initialiseTarget(r);
                }
                else
                {
                    initialiseTarget(0);

                }
            }
        }
        else
        {
            if (underwaterArea.Count > 1)
            {
                int r = Random.Range(0, underwaterArea.Count);
                initialiseTarget(r);

            }
            else
            {
                initialiseTarget(0);

            }
        }
        if (underwaterArea.Count > 1)
        {
            int r = Random.Range(0, underwaterArea.Count);
            return underwaterArea[r];
        }
        else
        {
            return underwaterArea[0];
        }

    }

    private void initialiseTarget(int r)
    {
        target = Instantiate(target, chooseNewTarget(underwaterArea[r]), Quaternion.identity);
        target.transform.parent = WaypointCollector.transform;
    }

    public Vector3 chooseNewTarget(GameObject underwaterArea)
    {

        float minX = underwaterArea.GetComponent<BoxCollider>().bounds.min.x;
        float minY = underwaterArea.GetComponent<BoxCollider>().bounds.min.y;
        float minZ = underwaterArea.GetComponent<BoxCollider>().bounds.min.z;

        float maxX = underwaterArea.GetComponent<BoxCollider>().bounds.max.x;
        float maxY = underwaterArea.GetComponent<BoxCollider>().bounds.max.y;
        float maxZ = underwaterArea.GetComponent<BoxCollider>().bounds.max.z;


        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        float z = Random.Range(minZ, maxZ);

        if (OceanFloor)
        {
            y = Terrain.activeTerrain.SampleHeight(new Vector3(x, y, z)) + Terrain.activeTerrain.GetPosition().y;
        }

        return new Vector3(x, y, z);

    }

    private void choosePrey()
    {
        UnderwaterAI[] potential_prey_AIs = FindObjectsOfType<UnderwaterAI>();
        List<GameObject> potential_prey = new List<GameObject>();
        int i = 0;
        foreach (var scannable in potential_prey_AIs)
        {
            if (scannable.gameObject.name.StartsWith(PreyName))
            {
                i++;
                potential_prey.Add(scannable.gameObject);
            }
        }
        int r = Random.Range(0, i - 5);

        target = potential_prey[r];
        Debug.Log("target found: " + target.name);
    }
}
