using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimapp_script : MonoBehaviour {

    public Transform player;
    private float previousShadowDistance;
    // Use this for initialization
    void Start () {
		
	}

    void OnPreRender()
    {
        previousShadowDistance = QualitySettings.shadowDistance;
        QualitySettings.shadowDistance = 0;
    }
    void OnPostRender()
    {
        QualitySettings.shadowDistance = previousShadowDistance;
    }
    // Update is called once per frame
    void LateUpdate () {
        Vector3 newpostion = player.position;
        newpostion.y = transform.position.y;
        transform.position=newpostion;
        transform.rotation = Quaternion.Euler(90, player.eulerAngles.y, 0f);
	}
}
