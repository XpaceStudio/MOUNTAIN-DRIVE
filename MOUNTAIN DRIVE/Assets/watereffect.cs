using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watereffect : MonoBehaviour
{
    [SerializeField]
    private carcontoller carcontoller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="player")
        {
            FindObjectOfType<carcontoller>().waterin();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "player")
        {
            FindObjectOfType<carcontoller>().waterout();
        }
    }
}
