using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerAI : MonoBehaviour
{
    private GameObject target;
    public float  strength = .5f;
    public float speed;
    public List<GameObject> targets = new List<GameObject>();
    public GameObject DialogTarget;
    private bool isDialog;

    // Start is called before the first frame update
    void Start()
    {
        target = DialogTarget;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDialog)
        {
            Move();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pathTarget") && !isDialog)
        {
            ChooseNewTarget();
        }
    }

    private void Move()
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

    private void ChooseNewTarget()
    {
        GameObject newTarget = target;
        while (newTarget == target)
        {
            int r = Random.Range(0, targets.Count);
            newTarget = targets[r];
        }
        target = newTarget;

    }

    public void StartWaypoint()
    {
        isDialog = true;
        target = DialogTarget;
    }

    public void EndDialog()
    {
        isDialog = false;
        ChooseNewTarget();
    }
    
}
