using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetindicator : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetpos = target.position;
        targetpos.y = transform.position.y;
     //  var dir = target.position - transform.position;
      //  var angle = Mathf.Atan2(dir.z,dir.x) * Mathf.Rad2Deg;
      transform.LookAt(targetpos);
       //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
