using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coincollectbig : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            FindObjectOfType<playercon>().doubleincreasegold();
            Destroy(gameObject);
        }
    }
}
