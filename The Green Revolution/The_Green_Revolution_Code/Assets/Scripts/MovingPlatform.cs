using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public List<Transform> points;
    public Transform platform;
    int goalpoint;
    public float moveSpeed = 2;

    private void Update()
    {
        MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        //Change the position of the platform(move towards the goal point)
        platform.position = Vector2.MoveTowards(platform.position, points[goalpoint].position, Time.deltaTime * moveSpeed);
        //Check if we are in very close proximity of the next point
        if (Vector2.Distance(platform.position, points[goalpoint].position) < 0.1f)
        {
            //If so change goal point to the next one
            //Check if we reached the last point, reset to first point
            if (goalpoint == points.Count - 1)
            {
                goalpoint = 0;
            }
            else
            {
                goalpoint++;
            }
        }
    }
}
