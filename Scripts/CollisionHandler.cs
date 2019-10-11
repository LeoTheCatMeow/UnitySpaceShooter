using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour {
	public GameObject deathAnimation;
	public int durability;
	private int _durability;

	void Start() {
		_durability = durability;
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag.Contains("Mortal") && this.tag != other.tag) {
			_durability--;

			if (_durability <= 0) {
				Destroy (this.gameObject);
				if (deathAnimation) {
					Instantiate (deathAnimation, transform.position, transform.rotation);
				}
			}

			if (this.tag == "Mortal, Player") {
				GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController> ().modifyPlayerHp (_durability, durability);
			}
		}
	}
}
