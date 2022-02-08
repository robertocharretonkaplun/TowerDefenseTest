using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class
Turret : MonoBehaviour {
  // Reference to enemy target
  private Transform target;

  // Turret Attributes
  [Header("Turret Attributes")]
  public Transform TurretHead;
  public float viewDistance = 20.0f;
  public float headRotOffset = 90.0f;
  public float turretTurnSpeed = 2.0f;

  // Shoot Attributes
  [Header("Shoot Attributes")]
  public GameObject bulletPref;
  public Transform ShootPosition;
  public float shootRate = 1.0f;
  private float shootTimer = 0.0f;

  // Start is called before the first frame update
  void 
  Start() {
    InvokeRepeating("UpdateTarget", 0.0f, 0.5f);
  }

  // Update is called once per frame
  void 
  Update() {
    if (target == null) {
      return;
    }

    Vector3 direction = target.position - transform.position;

    Quaternion lookRot = Quaternion.LookRotation(direction);
    //Vector3 realRot = Quaternion.Lerp(TurretHead.rotation ,
    //                                  lookRot, 
    //                                  Time.deltaTime * turretTurnSpeed).eulerAngles;
    Vector3 realRot = lookRot.eulerAngles;

    TurretHead.rotation = Quaternion.Euler(0f, realRot.y - headRotOffset, 0f);

    // Shoot validation
    if (shootTimer <= 0.0f)
    {
      Shoot();
      shootTimer = 1.0f / shootRate;
    }

    shootTimer -= Time.deltaTime;

    
  }

  /*
   * brief: Method in charge of detecting new enemies at the ground.
   */
  void 
  UpdateTarget() {
    // Set a reference to the list of enemies in the game
    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
    float shortDistance = Mathf.Infinity;
    GameObject closeEnemy = null;

    foreach (GameObject enemy in enemies) {
      // Get the distance from the enemy position and the turret position
      float distance = Vector3.Distance(transform.position, enemy.transform.position);
      // Check what enemy is closer and set it as the principal enemu
      if (distance < shortDistance) {
        shortDistance = distance;
        closeEnemy = enemy;
      }
    }
    // Check that the enemy that is closer to the turret became the main target
    if (closeEnemy != null && shortDistance <= viewDistance) {
      target = closeEnemy.transform;
    }
    else {
      target = null;
    }
  }

  /*
   * brief: Generate bullets that will follow the enemy targets.
   */
  private void
  Shoot() {
    // Instantiate a reference from the bullet prefab
    var bulletObj = Instantiate(bulletPref, ShootPosition.position, ShootPosition.rotation);
    var bullet = bulletObj.GetComponent<Bullet>();

    // Is the bullet was generated, then it will follow the enemy.
    if (bullet != null) {
      bullet.FollowTarget(target);
    }
  }

  /*
   * brief: Method in charge of debuggin purposes for the area detection from enemies on
   * the turrets.
   */
  private void 
  OnDrawGizmosSelected() {
    Gizmos.color = Color.green;
    Gizmos.DrawWireSphere(transform.position, viewDistance);
    if (target != null) {
      Gizmos.DrawLine(target.position, transform.position);
    }
  }
}
