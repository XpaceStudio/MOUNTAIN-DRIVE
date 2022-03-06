using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class inputmanager : MonoBehaviour
{

    [SerializeField]
    public TextMeshProUGUI Total_time;
    public float vertical;
    public float horizontal;
    public bool handbrake;
    public bool boosting;
    [SerializeField]
    public bool keybord;

    public int currentnode;
    public float timer;

    public waypoint_track waypoints;
    public Transform currentwaypoint;
    public Transform previouswaypoint;
    public List<Transform> nodes = new List<Transform>();

    private bool move = false;
    private void Awake()
    {
        waypoints = GameObject.FindGameObjectWithTag("path").GetComponent<waypoint_track>();
        nodes = waypoints.node;
    }
    // Update is called once per frame
    void Update()
    {

        if (!move)
        {
            return;
        }
        else
        {
            timer += Time.deltaTime;
            float t = timer;
            int t_minutes = ((int)t / 60); // t(seconds) / 60 = total minutes
            int t_seconds = ((int)t % 60); // t(seconds) % 60 = remaining seconds 
            int t_milliseconds = ((int)(t * 100)) % 100; // (total seconds * 1000) % 1000 = remaining milliseconds

            //display the text in a 00:00:00 format
            Total_time.text = string.Format("{0:00}:{1:00}:{2:00}", t_minutes, t_seconds, t_milliseconds);
        }
        if (keybord == true)
        {
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");
            handbrake = (Input.GetAxis("Jump") != 0) ? true : false;
        }
    }

    private void FixedUpdate()
    {

        calculateDistanceofWaypoint();
    }

    void calculateDistanceofWaypoint()
    {
        Vector3 postion = gameObject.transform.position;
        float distance = Mathf.Infinity;
        for (int i = 0; i < nodes.Count; i++)
        {
            Vector3 difference = nodes[i].transform.position - postion;
            float currentDistance = difference.magnitude;
            if (currentDistance < distance)
            {
                if (i > 2)
                    previouswaypoint = nodes[i - 3];
                currentwaypoint = nodes[i];
                distance = currentDistance;
                currentnode = i;
            }

        }
    }
    public void go()
    {
        move = true;
    }

    // Start is called before the first frame update

}
