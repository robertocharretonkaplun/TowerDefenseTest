using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class 
MainMenu : MonoBehaviour {
  public TextMeshProUGUI Points;
  private void 
  Start() {
    string previousPoints = PlayerPrefs.GetInt("Points").ToString();
    Points.text = "Score: " + previousPoints;
  }
  public void
  LoadGame() {
    SceneManager.LoadScene(1);
  }

  public void 
  ExitGame() {
    Application.Quit();
  }

}
