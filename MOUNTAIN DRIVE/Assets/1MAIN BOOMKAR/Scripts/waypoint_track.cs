using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypoint_track : MonoBehaviour
{
    public List<Transform> node = new List<Transform>();
    public Color linecolor;

    private void OnDrawGizmos()
    {

        Transform[] path = GetComponentsInChildren<Transform>();
        Gizmos.color = linecolor;

        node = new List<Transform>();
        for (int i = 1; i < path.Length; i++)
        {
            node.Add(path[i]);
        };

        for (int i = 0; i < node.Count; i++)
        {
            Vector3 currentwaypoint = node[i].position;
            Vector3 previouswaypoint = Vector3.zero;

            if (i != 0) previouswaypoint = node[i - 1].position;
          //  else if (i == 0) previouswaypoint = node[node.Count - 1].position;
            Gizmos.DrawLine(previouswaypoint, currentwaypoint);
            Gizmos.DrawSphere(currentwaypoint, .5f);
        }
    }

}
