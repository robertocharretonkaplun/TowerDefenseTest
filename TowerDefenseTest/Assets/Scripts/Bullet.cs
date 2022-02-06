using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  private Transform target;

  public float speed = 50.0f;

  public GameObject destroyEffect;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    // Check if there is a target to follow
    if (target != null) {

    }
    else
    {
      Destroy(gameObject);
      return;
    }

    // Get Direction of the target
    Vector3 direction = target.position - transform.position;
    float distance = speed * Time.deltaTime;

    // Check if the distance is valid from the direction from the target
    if (direction.magnitude <= distance) {
      Hit();
      return;
    }

    transform.Translate(direction.normalized * distance, Space.World);
  }

  public void 
  FollowTarget(Transform _target) {
    target = _target;
  }

  private void Hit()
  {
    LevelManager.instance.points += 1;
    LevelManager.instance.amounOfEnemies--;
    var Particle = Instantiate(destroyEffect, transform.position, transform.rotation);
    Destroy(Particle, 1.5f);
    Destroy(target.gameObject);
    Destroy(gameObject);
  }

}
