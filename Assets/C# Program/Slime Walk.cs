using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeWalk : MonoBehaviour
{
    public float speed;
    [SerializeField] private Waypoints Wpoints;

    private int waypointIndex;

    void Start()
    {
        if(Wpoints == null)
        {
            Wpoints = GameObject.FindGameObjectWithTag("Waypoints").GetComponent<Waypoints>();
        }
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Wpoints.waypoints[waypointIndex].position, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, Wpoints.waypoints[waypointIndex].position) < 0.1f){
            waypointIndex++;
        }
    }
}

