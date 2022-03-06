using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiinput : MonoBehaviour
{
    
    public float vertical;
    public float horizontal;
    public int currentnode;
    private int maxnode;
    public bool handbrake;
    public bool boosting;



    public waypoint_track waypoints;
    public Transform currentwaypoint;
    public Transform previouswaypoint;
    public List<Transform> nodes = new List<Transform>();
    [Range(0, 10)] public int distanceoffset;
    [Range(0, 10)] public float steerforce;

    private int levelwaypoint;
    // Start is called before the first frame update
    private void Awake()
    {
        waypoints = GameObject.FindGameObjectWithTag("path").GetComponent<waypoint_track>();
        nodes = waypoints.node;
        int activelevel= PlayerPrefs.GetInt("levels", 0);
        switch(activelevel)
        {
            case 0:levelwaypoint = 0;
                break;
            case 1:
                levelwaypoint = 70;
                break;
            case 2:
                levelwaypoint = 162;
                break;
            case 3:
                levelwaypoint = 0;
                break;
            case 4:
                levelwaypoint = 0;
                break;
            case 5:
                levelwaypoint = 0;
                break;
            case 6:
                levelwaypoint = 0;
                break;
            case 7:
                levelwaypoint = 0;
                break;
            case 8:
                levelwaypoint = 0;
                break;
            case 9:
                levelwaypoint = 0;
                break;

        }
        switch (activelevel)
        {
            case 0:
                maxnode = 71;
                break;
            case 1:
                maxnode = 160;
                break;
            case 2:
                maxnode = 165;
                break;
            case 3:
                maxnode = 0;
                break;
            case 4:
                maxnode = 0;
                break;
            case 5:
                maxnode = 0;
                break;
            case 6:
                maxnode = 0;
                break;
            case 7:
                maxnode = 0;
                break;
            case 8:
                maxnode = 0;
                break;
            case 9:
                maxnode = 0;
                break;

        }
    }


    public void aispwan()
    {
        if(previouswaypoint!=null)
        {
            gameObject.transform.position = previouswaypoint.position;
        }

    }

    private void FixedUpdate()
    {

            calculateDistanceofWaypoint();
            AIdrive();
        
    }
    public void AIdrive()
    {
        Aisteeer();
    }
    void Aisteeer()
    {
        Vector3 relative = transform.InverseTransformPoint(currentwaypoint.transform.position);
        relative /= relative.magnitude;
        horizontal = (relative.x +Random.Range(0,.15f)/ relative.magnitude ) * steerforce;
    }
    void calculateDistanceofWaypoint()
    {
        Vector3 postion = gameObject.transform.position;
        float distance = Mathf.Infinity;
        for (int i = levelwaypoint; i < maxnode; i++)
        {
            if (i > maxnode)
            {
                return;
            }
            Vector3 difference = nodes[i].transform.position - postion;
            float currentDistance = difference.magnitude;
            if(currentDistance<distance)
            {
                if(i>2)
                previouswaypoint = nodes[i-3];
                currentwaypoint = nodes[i + distanceoffset];
                distance = currentDistance;
                currentnode = i;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(currentwaypoint.position, 3);
    }

}
