using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroyScore : MonoBehaviour {
	public int points;
	private GameController gameController;
	void Start() {
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();
	}

	void OnDestroy() {
		gameController.addScore (points);
		if (gameController.FinalWave()) {
			gameController.RemoveFromFinalWave(this.gameObject);
		}
	}
}
