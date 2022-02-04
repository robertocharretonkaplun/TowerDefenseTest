using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public float speed = 10.0f;
  private int pointIndex = 0;
  private Transform target ;


  private void Start()
  {
    target = waypoints.Waypoints[0];
  }

  private void Update()
  {
    Vector3 direction = target.position - transform.position;

    // Move the object
    transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

    // Change the waypoint, if we reach the last point
    if (Vector3.Distance(transform.position, target.position) <= 0.2f)
    {
      ChangeToNextPoint();
    }
  }

  /*
   * brief: Method in charge of changing the waypoint index reference to keep 
   * moving the enemy.
   */
  void ChangeToNextPoint()
  {
    // If the enemy reach the last point, its destroyed
    if (pointIndex >= waypoints.Waypoints.Length - 1) {
      Destroy(gameObject);
    }
    else
    {
      // Change the waypoint index
      pointIndex++;
      target = waypoints.Waypoints[pointIndex];
    }
  }
}
