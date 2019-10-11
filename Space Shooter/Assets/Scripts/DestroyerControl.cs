using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerControl : MonoBehaviour {

	public GameObject bullet, bullet1;
	public Transform weaponMuzzle, weaponMuzzle1, weaponMuzzle2;

	private Rigidbody rb;
	private int r;
	private AudioSource au;

	public float recoil;
	private float _recoil;
	private int ammo = 0;

	void Start () {
		rb = this.gameObject.GetComponent<Rigidbody> ();
		au = this.gameObject.GetComponent<AudioSource> ();
		r = Random.Range (14, 17);
		_recoil = Random.Range (0.0f, recoil * 0.5f);
	}

	void Update() {
		//fire 
		_recoil += Time.deltaTime;
		if (_recoil >= recoil) {
			if (ammo == 0) {
				ammo = 3;
			}
			Instantiate (bullet, weaponMuzzle.position, weaponMuzzle.rotation);
			Instantiate (bullet1, weaponMuzzle1.position, weaponMuzzle1.rotation);
			Instantiate (bullet1, weaponMuzzle2.position, weaponMuzzle2.rotation);
			ammo--;
			if (ammo == 0) {
				_recoil = 0.0f;
			} else {
				_recoil = recoil * 0.85f;
			}
			au.Play ();
		} 
	}

	void FixedUpdate() {
		rb.velocity = new Vector3 (0.0f, 0.0f, -3.0f + (r - transform.position.z));
		float rotationY = Mathf.Atan2 (rb.velocity.x, rb.velocity.z) * 180.0f / Mathf.PI;
		transform.eulerAngles = new Vector3 (0.0f, rotationY, Mathf.Clamp(rb.velocity.x, -9.0f, 9.0f) * 5.0f);
	}
}
