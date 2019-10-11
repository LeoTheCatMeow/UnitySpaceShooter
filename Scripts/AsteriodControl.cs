using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteriodControl : MonoBehaviour {
	public float angularVelocityMultiplier;

	private Rigidbody rb;

	void Start () {
		rb = this.gameObject.GetComponent<Rigidbody> ();
		rb.angularVelocity = Random.insideUnitSphere * angularVelocityMultiplier;
		int r = Random.Range (0, 3);
		GameObject randomChild = gameObject.transform.GetChild (r).gameObject;
		randomChild.SetActive (true);
	}
}
