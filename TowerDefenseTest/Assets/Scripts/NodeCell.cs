using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeCell : MonoBehaviour
{
  private Renderer render;
  private Color OriginalCellColor;
  private GameObject turretRef;
  private Vector3 turretOffset = new Vector3(0, 0.5f, 0.0f);
  // Start is called before the first frame update
  void Start()
  {
    render = GetComponent<Renderer>();
    OriginalCellColor = render.material.color;
  }

  private void OnMouseEnter()
  {
    if (LevelManager.instance.points >= 25)
    {
      render.material.color = Color.cyan;
    }
    else
    {
      render.material.color = Color.red;
    }
  }

  private void OnMouseExit()
  {
    render.material.color = OriginalCellColor;
  }

  private void OnMouseDown()
  {
    if (turretRef != null)
    {
      // If the node already has a turret it will show a block signal
      render.material.color = Color.red;
      return;
    }
    else
    {
      if (LevelManager.instance.points >= 25 && LevelManager.instance.amounOfBasicTurrets >= 0)
      {
        // Remove points from player
        LevelManager.instance.points -= 25;
        LevelManager.instance.amounOfBasicTurrets -= 1;
        // Set new object reference
        GameObject newTorret = TurretBuilder.instance.GetTurret();
        turretRef = Instantiate(newTorret, transform.position + turretOffset, transform.rotation);
      }
      else
      {
        // Show warning color if the player try to place a turret, when he hasnt 
        // enough points
        render.material.color = Color.yellow;
      }
    }
  }
}
