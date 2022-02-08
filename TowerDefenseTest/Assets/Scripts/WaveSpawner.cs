using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * brief: Enum in charge of enumerating the amount of wave phases.
 */
enum 
WavePhase {
  Phase_1 = 0,
  Phase_2 = 1,
  Phase_3 = 2,
  Phase_4 = 3,
  Phase_5 = 4,
}

public class 
WaveSpawner : MonoBehaviour {
  public Transform enemyPrefab;
  public Transform spawnLocation;
  public float timeOfWaves = 6.5f;
  public float timer = 2.5f;
  public int waveIndex = 0;
  public TextMeshProUGUI TimerTxt;
  public TextMeshProUGUI WaveTxt;
  public GameObject winScreen;
  WavePhase wavePhase;
  // Start is called before the first frame update
  void 
  Start() {
    // Set the initial phase
    wavePhase = WavePhase.Phase_1;
  }

  // Update is called once per frame
  void
  Update() {
    // Spawn multiple waves of enemies depending on the time of waves generated.
    if (waveIndex != 25) {
      if (timer <= 0.0f) {
        StartCoroutine( Wave());
        timer = timeOfWaves;
      }
    }


    // Update texts
    var fixTime = Mathf.Floor(timer);
    TimerTxt.text = "Next wave start in: " + fixTime.ToString();
    WaveTxt.text = waveIndex.ToString();

    // Win condition
    if (LevelManager.instance.amounOfEnemies == 0 && waveIndex >=2 && waveIndex == 25) {
      Time.timeScale = 0;
      PlayerPrefs.SetInt("Points", LevelManager.instance.points);
      winScreen.SetActive(true);
    }
    else {
      timer -= Time.deltaTime;
    }
  }

  /* 
   * brief: Spawn group of enemies. 
   */
  private IEnumerator 
  Wave() {
    // Change the Wave phase
    switch (wavePhase) {
      case WavePhase.Phase_1:
        waveIndex++;
        wavePhase = WavePhase.Phase_2;
        break;
      case WavePhase.Phase_2:
        waveIndex += 1;
        wavePhase = WavePhase.Phase_3;
        break;
      case WavePhase.Phase_3:
        waveIndex += 1;
        wavePhase = WavePhase.Phase_4;
        break;
      case WavePhase.Phase_4:
        waveIndex += 1;
        wavePhase = WavePhase.Phase_5;
        break;
      case WavePhase.Phase_5:
        waveIndex += 1;
        break;
      default:
        break;
    }

    // Spawn the enemies
    for (int i = 0; i < waveIndex; i++) {
      SpawnEnemy();
      yield return new WaitForSeconds(0.8f);
    }

  }

  /*
   * brief: Method in charge of generating a new enemy at the enemy base.
   */
  private void 
  SpawnEnemy() {
    Instantiate(enemyPrefab, spawnLocation.position, spawnLocation.rotation);
    LevelManager.instance.amounOfEnemies++;
  }

}
