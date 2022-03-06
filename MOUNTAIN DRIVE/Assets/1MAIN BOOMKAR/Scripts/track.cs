using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class track : MonoBehaviour {

    public Transform tracking;
    public Transform center;
    Transform pivot;

 public void Start()
    {
        pivot = new GameObject().transform;
    }

    public void  Update()
    {
        pivot.position = center.position;
        var dist = Vector3.Distance(center.position, transform.position);
        pivot.LookAt(tracking);
        transform.position = center.position + pivot.forward * dist;
        transform.LookAt(tracking);
        transform.parent = pivot;
    }
}
