using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraatest : MonoBehaviour
{
    public GameObject[] cam;
    public GameObject playercam;
    int n = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onit()
    {
        playercam.SetActive(false);
        for (int i = 0; i < cam.Length; i++)
        {
            cam[i].SetActive(false);
        }
        cam[n].SetActive(true);
        n++;
        if(n>3)
        {
            n = 0;
        }
    }
    public void normalcam()
    {
        playercam.SetActive(true);
    }
}
