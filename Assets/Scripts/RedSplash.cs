using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSplash : MonoBehaviour {

	void Start() {
		StartCoroutine (_RedSplash ());
	}
		
	IEnumerator _RedSplash() {
		SpriteRenderer sp = gameObject.GetComponent<SpriteRenderer> ();
		float opacity = 0.00f;
		Color tmp;
		tmp.a = opacity;
		for (opacity = 0.00f; opacity < 0.60f; opacity += 0.08f) {
			tmp = sp.color;
			tmp.a = opacity;
			sp.color = tmp;
			yield return null;
		}
		for (opacity = 0.60f; opacity > 0.00f; opacity -= 0.06f) {
			tmp = sp.color;
			tmp.a = opacity;
			sp.color = tmp;
			yield return null;
		}
		gameObject.SetActive (false);
}
}