using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_player : MonoBehaviour
{
    public GameObject countdown;
    public GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        countdown.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void end()
    {
        if(gameObject.name!= "C-GO")
        countdown.SetActive(false);
        cam.SetActive(true);
        gameObject.SetActive(false);
    }
}
