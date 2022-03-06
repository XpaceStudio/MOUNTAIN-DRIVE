 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraround : MonoBehaviour {

    // Use this for initialization
    public float distance;
    public Transform Player;
    public Vector3 offset;
    public float speed;

    void Update()
    {

        transform.position = transform.forward * distance + Player.position;
        offset = new Vector3(Player.position.x, 10, Player.position.z);

        //Player.position = offset;
        transform.RotateAround(Player.position, Vector3.up, speed*Time.unscaledDeltaTime);


    }

}
