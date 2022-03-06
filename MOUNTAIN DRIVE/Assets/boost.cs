using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boost : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            FindObjectOfType<carcontoller>().booston();
        }
        if (other.gameObject.tag == "ai")
        {
            FindObjectOfType<aicontroller>().booston();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            FindObjectOfType<carcontoller>().bootoff();
        }
    }
}
