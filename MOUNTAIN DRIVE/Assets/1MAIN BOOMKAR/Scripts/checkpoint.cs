using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    public Material red;
    private MeshRenderer MeshRenderer;

    private void Awake()
    {
        MeshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="player")
        {
            FindObjectOfType<gamemanagar>().checkpoint_Reached(gameObject);
            MeshRenderer.material = red;
           
        }
    }

}
