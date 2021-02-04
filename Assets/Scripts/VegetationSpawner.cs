using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetationSpawner : MonoBehaviour
{
    public List<GameObject> areasToSpawnVegetation = new List<GameObject>();
    public GameObject plantPrefab;
    public int density;

    // Start is called before the first frame update
    void Start()
    {
       for (int i = 0; i < areasToSpawnVegetation.Count; i++)
        {
            spawnVegetation(i);
        }
    }

    private void spawnVegetation(int area)
    {
        for (int i = 0; i < density; i++)
        {
            Instantiate(plantPrefab, choosePointInArea(areasToSpawnVegetation[area]), Quaternion.identity).transform.parent = transform;
        }
    }

    public Vector3 choosePointInArea(GameObject areaToSpawn)
    {

        float minX = areaToSpawn.GetComponent<BoxCollider>().bounds.min.x;
        float minY = areaToSpawn.GetComponent<BoxCollider>().bounds.min.y;
        float minZ = areaToSpawn.GetComponent<BoxCollider>().bounds.min.z;

        float maxX = areaToSpawn.GetComponent<BoxCollider>().bounds.max.x;
        float maxY = areaToSpawn.GetComponent<BoxCollider>().bounds.max.y;
        float maxZ = areaToSpawn.GetComponent<BoxCollider>().bounds.max.z;


        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        float z = Random.Range(minZ, maxZ);

        y = Terrain.activeTerrain.SampleHeight(new Vector3(x, y, z)) + Terrain.activeTerrain.GetPosition().y;

        return new Vector3(x, y, z);

    }
}
