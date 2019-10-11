using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
	public float velocityMultiplier;
	public float tiltMultiplier;

	public GameObject bullet;
	public Transform weaponMuzzle;

	private Rigidbody rb;
	private AudioSource au;

	public float recoil;
	private float _recoil = 0;

	void Start () {
		rb = this.gameObject.GetComponent<Rigidbody> ();
		au = this.gameObject.GetComponent<AudioSource> ();
	}

	void Update() {
		//fire 
		_recoil += Time.deltaTime;
		if (_recoil >= recoil && Input.GetButton ("Fire1")) {
			Instantiate (bullet, weaponMuzzle.position, weaponMuzzle.rotation);
			_recoil = 0;
			au.Play ();
		}
	}

	void FixedUpdate () {
		//motion
		float motionX = Input.GetAxis ("Horizontal");
		float motionZ = Input.GetAxis ("Vertical");
		rb.AddForce (new Vector3 (motionX, 0.0f, motionZ) * velocityMultiplier);
		transform.eulerAngles = new Vector3 (0.0f, 0.0f, -rb.velocity.x * tiltMultiplier);

		if (rb.velocity.magnitude <= 0.05f) {
			rb.velocity = Vector3.zero;
		}

		Vector3 handleBoundary = new Vector3 (Mathf.Clamp(transform.position.x, -7.5f, 7.5f), 0.0f, Mathf.Clamp(transform.position.z, -2.0f, 16.0f));
		if (transform.position.x < handleBoundary.x || transform.position.x > handleBoundary.x) {
			rb.velocity = new Vector3 (-0.2f * rb.velocity.x, 0.0f, rb.velocity.z);
		}
		if (transform.position.z < handleBoundary.z || transform.position.z > handleBoundary.z) {
			rb.velocity = new Vector3 (rb.velocity.x, 0.0f, -0.2f * rb.velocity.z);
		}
		transform.position = handleBoundary;
	}
}
