using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
  public Transform target;
  public float viewDistance = 20.0f;
  // Start is called before the first frame update
  void Start()
  {
    InvokeRepeating("UpdateTarget", 0.0f, 0.5f);
  }

  // Update is called once per frame
  void Update()
  {
    if (target == null)
    {
      return;
    }
  }

  void UpdateTarget()
  {
    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
    float shortDistance = Mathf.Infinity;
    GameObject closeEnemy = null;

    foreach (GameObject enemy in enemies)
    {
      float distance = Vector3.Distance(transform.position, enemy.transform.position);

      if (distance < shortDistance)
      {
        shortDistance = distance;
        closeEnemy = enemy;
      }
    }

    if (closeEnemy != null && shortDistance <= viewDistance)
    {
      target = closeEnemy.transform;
    }
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.green;
    Gizmos.DrawWireSphere(transform.position, viewDistance);
  }
}
