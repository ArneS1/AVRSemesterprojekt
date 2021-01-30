using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour
{
    public string SceneToLoad;
    private string STARTING_TEXT = "STARTING...";
    public Text MenuText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision with " + other.gameObject.tag);
        if(other.gameObject.CompareTag("Hand")){
            MenuText.text = STARTING_TEXT;
            SceneManager.LoadSceneAsync(SceneToLoad);
        }
    }
}
