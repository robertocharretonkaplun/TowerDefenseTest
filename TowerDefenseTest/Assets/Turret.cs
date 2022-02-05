using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class
Turret : MonoBehaviour {
  private Transform target;
  public Transform TurretHead;
  public float viewDistance = 20.0f;
  public float headRotOffset = 90.0f;
  public float turretTurnSpeed = 2.0f;
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

  }

  void 
  UpdateTarget() {
    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
    float shortDistance = Mathf.Infinity;
    GameObject closeEnemy = null;

    foreach (GameObject enemy in enemies) {
      float distance = Vector3.Distance(transform.position, enemy.transform.position);

      if (distance < shortDistance) {
        shortDistance = distance;
        closeEnemy = enemy;
      }
    }

    if (closeEnemy != null && shortDistance <= viewDistance) {
      target = closeEnemy.transform;
    }
    else {
      target = null;
    }
  }

  private void 
  OnDrawGizmosSelected() {
    Gizmos.color = Color.green;
    Gizmos.DrawWireSphere(transform.position, viewDistance);
    if (target != null) {
      Gizmos.DrawLine(target.position, transform.position);
    }
  }
}
