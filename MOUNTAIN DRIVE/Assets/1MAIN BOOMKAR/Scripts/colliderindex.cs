using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderindex : MonoBehaviour {


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            FindObjectOfType<tutorial>().tutorial3active();
            Destroy(gameObject);
        }
    }
}
