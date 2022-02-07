using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 
CameraRotation : MonoBehaviour {
  public Camera camera;
  public Transform target;
  private Vector3 previousPosition;
  // Start is called before the first frame update
  void Start()
  {
    camera.transform.position = target.position;
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetMouseButtonDown(1))
    {
      previousPosition = camera.ScreenToViewportPoint(Input.mousePosition);
    }

    if (Input.GetMouseButton(1))
    {
      Vector3 direction = previousPosition - camera.ScreenToViewportPoint(Input.mousePosition);

      camera.transform.position = target.position;

      camera.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
      //camera.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
      camera.transform.Translate(new Vector3( 0, 0, -10));

      previousPosition = camera.ScreenToViewportPoint(Input.mousePosition);
    }

    
  }
}
