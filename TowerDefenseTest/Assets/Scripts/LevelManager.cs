using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class
LevelManager : MonoBehaviour {
  public static LevelManager instance;
  public GameObject looseScreen;
  public TextMeshProUGUI pointsTxt;
  public TextMeshProUGUI BasicTurretBtn;
  public int points = 25;
  public bool isBaseDestroyed = false;
  public int amounOfBasicTurrets = 5;
  public int amounOfEnemies = 0;

  // Start is called before the first frame update
  void 
  Start() {
    // Set instance of the object
    if (instance != null) {
      return;
    }
    instance = this;
    // Remove Pause state
    Time.timeScale = 1;
    looseScreen.SetActive(false);
  }

  // Update is called once per frame
  void 
  Update() {
    // Show in the canvas the information related to points and available turrets
    pointsTxt.text = "Points: " + points.ToString();
    BasicTurretBtn.text = "Basic x " + amounOfBasicTurrets.ToString();
  }

  public void 
  LooseScreen() {
    // Stop the game if the player loose
    Time.timeScale = 0;
    // Activate the loose screen 
    looseScreen.SetActive(true);
  }

  public void
  LoadMenu() {
    // Load the menu scene referenced as 0
    SceneManager.LoadScene(0);
  }
  public void 
  RestartLevel() {
    // Load the game scene referenced as 1
    SceneManager.LoadScene(1);
  }
}
