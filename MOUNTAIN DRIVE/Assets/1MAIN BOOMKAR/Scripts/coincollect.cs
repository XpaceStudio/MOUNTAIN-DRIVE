using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coincollect : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {

            FindObjectOfType<playercon>().increasegold();
            Destroy(gameObject);
        }
    }
}
