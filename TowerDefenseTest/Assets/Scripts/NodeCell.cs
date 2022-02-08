using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 
NodeCell : MonoBehaviour {
  private Renderer render;
  private Color OriginalCellColor;
  private GameObject turretRef;
  private Vector3 turretOffset = new Vector3(0, 0.5f, 0.0f);
  // Start is called before the first frame update
  void 
  Start() {
    render = GetComponent<Renderer>();
    OriginalCellColor = render.material.color;
  }

  private void 
  OnMouseEnter() {
    // Check if the player has the necessary points to set a turret
    if (LevelManager.instance.points >= 25)
    {
      render.material.color = Color.cyan;
    }
    else
    {
      // If the points are less than 25, the nodes will be shown as a red color.
      render.material.color = Color.red;
    }
  }

  private void 
  OnMouseExit() {
    render.material.color = OriginalCellColor;
  }

  private void 
  OnMouseDown() {
    if (turretRef != null) {
      // If the node already has a turret it will show a block signal
      render.material.color = Color.red;
      return;
    }
    else {
      // If the amount of basic turrets is equal to cero, stop instantiating
      if (LevelManager.instance.amounOfBasicTurrets == 0)
      {
        LevelManager.instance.amounOfBasicTurrets = 0;
        return;
      }
      // Instance new turrets if the user has more that 25 points and enough turrets.
      if (LevelManager.instance.amounOfBasicTurrets <= 5) {
        // Check that there is enough points
        if (LevelManager.instance.points < 25) {
          return;
        }
        // Set new object reference
        GameObject newTorret = TurretBuilder.instance.GetTurret();

        if (newTorret.gameObject.tag == "BasicTurret" && LevelManager.instance.points >= 25)
        {
          // Remove points from player
          LevelManager.instance.amounOfBasicTurrets -= 1;
          newTorret.GetComponent<Turret>().shootRate = 1.0f;
          LevelManager.instance.points -= 25;
          //turretRef = Instantiate(newTorret, transform.position + turretOffset, transform.rotation);
        }
        if (newTorret.gameObject.tag == "AdvanceTurret" && LevelManager.instance.points >= 50)
        {
          // Remove points from player
          LevelManager.instance.amounOfBasicTurrets -= 1;
          newTorret.GetComponent<Turret>().shootRate = 3f;
          LevelManager.instance.points -= 50;
        }

        turretRef = Instantiate(newTorret, transform.position + turretOffset, transform.rotation);        
      }
      else {
        // Show warning color if the player try to place a turret, when he hasnt 
        // enough points
        render.material.color = Color.yellow;
      }
    }
  }
}
