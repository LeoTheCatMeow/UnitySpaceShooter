using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {
	public GameObject bullet;
	public Transform weaponMuzzle;

	private Rigidbody rb;
	private int r;
	private AudioSource au;

	public float recoil;
	private float _recoil;

	void Start () {
		rb = this.gameObject.GetComponent<Rigidbody> ();
		au = this.gameObject.GetComponent<AudioSource> ();
		r = Random.Range (-1, 1);
		_recoil = Random.Range (0.0f, recoil * 0.5f);
	}

	void Update() {
		//fire 
		_recoil += Time.deltaTime;
		if (_recoil >= recoil) {
			Instantiate (bullet, weaponMuzzle.position, weaponMuzzle.rotation);
			_recoil = 0;
			au.Play ();
		}
	}

	void FixedUpdate() {
		rb.velocity = new Vector3 (Mathf.Cos (Time.time) * r * 2, 0.0f, -4.0f);
		float rotationY = Mathf.Atan2 (rb.velocity.x, rb.velocity.z) * 180.0f / Mathf.PI;
		transform.eulerAngles = new Vector3 (0.0f, rotationY, Mathf.Clamp(rb.velocity.x, -9.0f, 9.0f) * 5.0f);
	}
}
