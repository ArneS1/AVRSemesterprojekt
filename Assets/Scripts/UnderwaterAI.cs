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

    public float  strength = .5f;
    public float speed;

    public bool OceanFloor;

    public List<GameObject> underwaterArea = new List<GameObject>();



    void Start()
    {
        if(speed <= 0){
            speed = Gamestate.Instance.BASIC_FISH_SPEED;
        }
        if(fish_prefab != null){
            GameObject newFish = Instantiate(fish_prefab, transform.position, Quaternion.identity);
        newFish.transform.parent = transform;
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
            if(OceanFloor){

                transform.position = new Vector3(
                    transform.position.x,
                    Terrain.activeTerrain.SampleHeight(transform.position) + Terrain.activeTerrain.GetPosition().y, 
                    transform.position.z);
            }
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Terrain"))
        {
            chooseUnderwaterLevel();
        } else if (other.CompareTag("pathTarget")) {
            chooseUnderwaterLevel();
        }
    }

    public GameObject chooseUnderwaterLevel()
    {
        if (underwaterArea.Count > 1 )
        {
            int r = Random.Range(0, underwaterArea.Count);
            target = Instantiate(target, chooseNewTarget(underwaterArea[r]), Quaternion.identity);
            return underwaterArea[r];
        } 
        else
        {
            target = Instantiate(target, chooseNewTarget(underwaterArea[0]), Quaternion.identity);
            return underwaterArea[0];

        }

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

        if(OceanFloor){
            y = Terrain.activeTerrain.SampleHeight(new Vector3(x, y, z)) + Terrain.activeTerrain.GetPosition().y;
        }

       return new Vector3(x, y, z);

    }
}
