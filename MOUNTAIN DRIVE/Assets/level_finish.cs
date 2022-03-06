using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level_finish : MonoBehaviour
{
    public GameObject finishcam;
    private gamemanagar gamemanagar;
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
        if (gamemanagar.checkpoint.Count < 1)
        {
                return;
        }
        if (other.CompareTag("player"))
        {
            finishcam.SetActive(true);
            Time.timeScale = .7f;
        }

    }
}
