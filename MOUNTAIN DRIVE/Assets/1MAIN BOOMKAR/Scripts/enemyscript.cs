using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyscript : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, player.position, time);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {

        }
    }
}
