using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBuilder : MonoBehaviour
{
  public static TurretBuilder instance;

  private GameObject turret;
  public GameObject basicTurretPref;

  private void Awake()
  {
    if (instance != null)
    {
      return;
    }
    instance = this;
  }

  // Start is called before the first frame update
  void Start()
  {
    turret = basicTurretPref;
  }

  public GameObject GetTurret()
  {
    return turret;
  }
}
