using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypoints : MonoBehaviour
{
  // Access from anywhere in the project
  public static Transform[] Waypoints;

  void Awake() {
    // Initialize the array of points
    Waypoints = new Transform[transform.childCount];

    // Set all the child points
    for (int i = 0; i < Waypoints.Length; i++) {
      Waypoints[i] = transform.GetChild(i);
    }
  }
}
