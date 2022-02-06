using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class LevelManager : MonoBehaviour
{
  public static LevelManager instance;
  public GameObject looseScreen;
  public TextMeshProUGUI pointsTxt;
  public TextMeshProUGUI BasicTurretBtn;
  public int points = 25;
  public bool isBaseDestroyed = false;
  public int amounOfBasicTurrets = 5;
  public int amounOfEnemies = 0;
  // Start is called before the first frame update
  void Start()
  {
    // Set instance of the object
    if (instance != null)
    {
      return;
    }
    instance = this;
    // Remove Pause state
    Time.timeScale = 1;
    looseScreen.SetActive(false);
  }

  // Update is called once per frame
  void Update()
  {
    pointsTxt.text = "Points: " + points.ToString();
    BasicTurretBtn.text = "Basic x " + amounOfBasicTurrets.ToString();
  }

  public void LooseScreen() {
    Time.timeScale = 0;
    looseScreen.SetActive(true);
  }

  public void LoadMenu()
  {
    SceneManager.LoadScene(0);
  }
  public void RestartLevel()
  {
    SceneManager.LoadScene(1);
  }
}
