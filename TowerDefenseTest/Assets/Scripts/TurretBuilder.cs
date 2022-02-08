using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBuilder : MonoBehaviour
{
  public static TurretBuilder instance;

  private GameObject turret;
  //public GameObject basicTurretPref;
  public List<GameObject> Turrets;

  public int turretIndex = 0;
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
    turret = Turrets[turretIndex];
  }

  public GameObject GetTurret()
  {
    return Turrets[turretIndex];
  }

  public void SetBasicTurret()
  {
    if (LevelManager.instance.points >= 25)
    {
      turretIndex = 0;
    }
  }
  public void SetAdvancedTurret()
  {
    if (LevelManager.instance.points >= 50)
    {
      turretIndex = 1;
      
    }
  }
}
