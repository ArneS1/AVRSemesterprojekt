using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSelector : MonoBehaviour
{
    // Start is called before the first frame update

    public List<GameObject> trash_objects = new List<GameObject>();
    private GameObject selected_trash_prefab;

    public GameObject areaToSpawn;

    void Start()
    {
        int r = Random.Range(0, trash_objects.Count);
        selected_trash_prefab = Instantiate(trash_objects[r], choosePointInArea(areaToSpawn), Quaternion.identity);
        selected_trash_prefab.transform.parent = transform;
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
        return new Vector3(x, y, z);

    }
}
