using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundControl : MonoBehaviour {
	public float scrollSpeed;
	private Renderer r;

	void Start () {
		r = this.gameObject.GetComponent<Renderer> ();
	}

	void Update () {
		float offset = Time.time * scrollSpeed;
		r.material.SetTextureOffset ("_MainTex", new Vector2 (0, offset));
	}
}
