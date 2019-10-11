using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	//enemy spawning related variables
	public float spawnWait;
	public GameObject asteroid;
	public float spawnInterval;
	public GameObject enemy;
	public float spawnInterval2;
	public GameObject enemy_spec;
	public float spawnInterval3;
	public GameObject destroyer;
	public float spawnInterval4;
	private float _spawnInterval = 0;
	private float _spawnInterval2 = 0;
	private float _spawnInterval3 = 0;
	private float _spawnInterval4 = 0;
	private float lastRandomX = 100;

	//game condition related varaibles
	public float difficultyUpInterval = 60;
	private float _difficultyUpInterval = 0;
	private List<GameObject> finalWave;
	public int finalWaveTime;
	private bool finalW = false;
	private bool victory = false;

	//UI related variables 
	public Text scoreText;
	public RectTransform hp;
	public CanvasRenderer victoryText;
	public CanvasRenderer gameOverText;
	public Toggle pauseButton;
	private int score = 0;

	void Start () {
		_spawnInterval -= spawnWait;
		_spawnInterval2 -= spawnWait * 8;
		_spawnInterval3 -= spawnWait * 20;
		_spawnInterval4 -= spawnWait * 32;
	}


	void Update () {
		//enemy generation ----------------------------------------------------------------------------------------------------------
		_difficultyUpInterval += Time.deltaTime; //increase difficulty every minute 
		if (_difficultyUpInterval >= difficultyUpInterval) {
			spawnInterval2 *= 0.95f;
			spawnInterval3 *= 0.95f;
			spawnInterval4 *= 0.95f;
			_difficultyUpInterval = 0;
		}

		if (!victory) { //asteroids spawn till the end
			_spawnInterval += Time.deltaTime;
			if (_spawnInterval >= spawnInterval) { 
				_spawnInterval = 0;
				Spawn (asteroid, 8.0f);
			}
		}
			
		if (Time.time < finalWaveTime - 12) { //enemies stop spawning during final wave
			_spawnInterval2 += Time.deltaTime;
			if (_spawnInterval2 >= spawnInterval2) {
				_spawnInterval2 = 0;
				Spawn (enemy, 6.0f);
			}

			_spawnInterval3 += Time.deltaTime;
			if (_spawnInterval3 >= spawnInterval3) {
				_spawnInterval3 = 0;
				int s = Random.Range (1, 3); 
				Vector3 spawnPosition = new Vector3 ((s * 2 - 3) * 12.0f, 0.0f, 20.0f);
				Instantiate (enemy_spec, spawnPosition, Quaternion.identity);
			}

			_spawnInterval4 += Time.deltaTime;
			if (_spawnInterval4 >= spawnInterval4) {
				_spawnInterval4 = 0;
				Spawn (destroyer, 4.5f, 0.0f, 22.0f);
			}

		}
			
		if (Time.time > finalWaveTime && !finalW) { //final wave 
			finalWave = new List<GameObject> ();
			finalW = true;
			for (float i = -6.0f; i <= 6.0f; i += 4.0f) {
				Vector3 spawnPosition = new Vector3 (i, 0.0f, 22.0f);
				finalWave.Add(Instantiate (destroyer, spawnPosition, Quaternion.identity)); 
			}
			for (float i = -7.0f; i <= 7.0f; i += 7.0f) {
				Vector3 spawnPosition = new Vector3 (i, 0.0f, 20.0f);
				finalWave.Add(Instantiate (enemy, spawnPosition, Quaternion.identity)); 
			}
		}

		if (finalW && finalWave.Count == 0) { //victory 
			victory = true;
		}

		if (victory && GameObject.FindGameObjectsWithTag("Mortal, Enemy").Length == 0) {
			victoryText.gameObject.SetActive (true);
		}

		//UI-------------------------------------------------------------------------------------------------------------------------
		if (hp.localScale.x > 0 && !victory) {
			int totalScore = (int)(Time.timeSinceLevelLoad * 2.0f) + score;
			scoreText.text = "Score: " + totalScore.ToString ();
		}

		if (hp.localScale.x == 0 && GameObject.FindGameObjectWithTag("Player") == null) {
			gameOverText.gameObject.SetActive (true);
		}

		//Game Control----------------------------------------------------------------------------------------------------------------
		if (pauseButton.isOn) {
			Time.timeScale = 0.0f;
		} else {
			Time.timeScale = 1.0f;
		}

		if (gameOverText.gameObject.activeSelf && Input.GetKeyDown ("r")) {
			SceneManager.LoadScene ("MainScene");
		}
	}

	void Spawn(GameObject unit, float spawnRange, float hight = 0.0f, float distance = 20.0f) {
		Vector3 randomPosition = new Vector3 (Random.Range (-1.0f, 1.0f) * spawnRange, hight, distance);
		while (Mathf.Abs (randomPosition.x - lastRandomX) < 1.0f) {
			randomPosition = new Vector3 (Random.Range (-1.0f, 1.0f) * spawnRange, hight, distance);
		}
		lastRandomX = randomPosition.x;
		Instantiate (unit, randomPosition, Quaternion.identity);
	}
		
	public void modifyPlayerHp(int current, int max) {
		hp.localScale = new Vector3((float)current / (float)max, 1.0f, 1.0f);
	}
		
	public void addScore(int points) {
		score += points;
	}
		
	public bool FinalWave() {
		return finalW;
	}
		
	public void RemoveFromFinalWave(GameObject obj) {
		if (finalWave.Contains (obj)) {
			finalWave.Remove (obj);
		}
	}
}
