using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMotion : MonoBehaviour {
	public float speed;
	private Rigidbody rb;

	void Start () {
		rb = this.gameObject.GetComponent<Rigidbody> ();
		rb.velocity = transform.forward * speed;
	}
}
