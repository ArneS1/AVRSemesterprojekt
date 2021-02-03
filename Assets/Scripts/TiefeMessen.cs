using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TiefeMessen : MonoBehaviour
{

    public Text text;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        text.text = "" + Mathf.FloorToInt((transform.position.y)) * 10  + " m";
    }
}
