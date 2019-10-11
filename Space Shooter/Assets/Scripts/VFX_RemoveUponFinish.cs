using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_RemoveUponFinish : MonoBehaviour {
	private ParticleSystem sys;
	void Start () {
		sys = this.gameObject.GetComponent<ParticleSystem> ();
		Destroy (this.gameObject, sys.main.duration);
	}
}
