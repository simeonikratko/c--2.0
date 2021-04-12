using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    #region Fields
    public float moveSpeed;
    public GameObject[] wayPoints;

    int nextWaypoint = 1;
    float distToPoint;
    #endregion

    void Update()
    {
        Move();
    }

    void Move()
    {
        #region Moving
        distToPoint = Vector2.Distance(transform.position, wayPoints[nextWaypoint].transform.position);
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[nextWaypoint].transform.position, moveSpeed * Time.deltaTime);
        if(distToPoint < 0.001f)
        {
            TakeTurn();
        }
        #endregion
    }

    void TakeTurn()
    {
        #region Rotating
        Vector3 currRot = transform.eulerAngles;
        currRot.z += wayPoints[nextWaypoint].transform.eulerAngles.z;
        transform.eulerAngles = currRot;
        ChooseNextWaypoint();
        #endregion
    }

    void ChooseNextWaypoint()
    {
        #region Choose the next Waypoint
        nextWaypoint++;
        if(nextWaypoint == wayPoints.Length)
        {
            nextWaypoint = 0;
        }
        #endregion
    }
}
