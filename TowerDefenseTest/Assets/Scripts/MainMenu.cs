using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class 
MainMenu : MonoBehaviour {
  public TextMeshProUGUI Points;
  public TextMeshProUGUI Waves;

  private void 
  Start() {
    // Get the stored points from the memory
    string previousPoints = PlayerPrefs.GetInt("Points").ToString();
    string previousWaves = PlayerPrefs.GetInt("Waves").ToString();
    // Show the points at screen
    Points.text = "Score: " + previousPoints;
    Waves.text = "Waves: " + previousWaves;
  }

  /*
   * brief: Method in charge of changing the scene to the game.
   */
  public void
  LoadGame() {
    SceneManager.LoadScene(1);
  }

  /*
   * brief: Method in charge of closing the game.
   */
  public void 
  ExitGame() {
    Application.Quit();
  }

}
