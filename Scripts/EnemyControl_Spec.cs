using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl_Spec : MonoBehaviour {
	public GameObject bullet;
	public Transform weaponMuzzle;

	private Rigidbody rb;
	private float r;
	private AudioSource au;

	public float recoil;
	private float _recoil;

	void Start () {
		rb = this.gameObject.GetComponent<Rigidbody> ();
		au = this.gameObject.GetComponent<AudioSource> ();
		r = Random.Range (0.15f, 0.35f);
		_recoil = Random.Range (0.0f, recoil * 0.5f);
		rb.velocity = new Vector3 (-transform.position.x / Mathf.Abs (transform.position.x), 0.0f, -4.0f);
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
		rb.velocity = new Vector3 (rb.velocity.x + rb.velocity.x * r * 2 * Time.deltaTime , rb.velocity.y, rb.velocity.z);
		float rotationY = Mathf.Atan2 (rb.velocity.x, rb.velocity.z) * 180.0f / Mathf.PI;
		transform.eulerAngles = new Vector3 (0.0f, rotationY, Mathf.Clamp(rb.velocity.x, -9.0f, 9.0f) * 5.0f);
	}
}
