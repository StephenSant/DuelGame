using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    public GameObject waypointParent;
    public Transform[] waypoints;
    public int currentWaypoint;
    public GameObject platform;
    public float speed;

    // Use this for initialization
    void Start()
    {
        currentWaypoint = 0;
        waypoints = waypointParent.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWaypoint == 0) { currentWaypoint = 1; }

        platform.transform.Translate((waypoints[currentWaypoint].position - platform.transform.position).normalized *(0.01f*speed)) ;

        float distance = Vector3.Distance(waypoints[currentWaypoint].position, platform.transform.position);
        if (0.1f >= distance)
        {
            currentWaypoint++;
        }
        if (currentWaypoint == waypoints.Length)
        {
            currentWaypoint = 1;
        }
    }
}
