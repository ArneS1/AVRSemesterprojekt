using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class UnderwaterAI : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject target;
    public float  strength = .5f;
    public float speed;
    public List<GameObject> targets = new List<GameObject>();


    void Start()
    {
        chooseNewTarget();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 goalVector = target.transform.position - transform.position;
        if (goalVector != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(goalVector);
            float str = Mathf.Min(strength * Time.deltaTime, 1);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
        }


        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 0.01f);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "pathTarget")
        {
            chooseNewTarget();
        }
    }

    private void chooseNewTarget()
    {
        GameObject newTarget = target;
        while (newTarget == target)
        {
            int r = Random.Range(0, targets.Count);
            newTarget = targets[r];
        }
        target = newTarget;

    }
}
