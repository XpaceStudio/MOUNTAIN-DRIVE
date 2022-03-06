using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelcomplete : MonoBehaviour {


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            FindObjectOfType<gamemanagar>().levelreached(this.transform.parent);
        }
    }
}

