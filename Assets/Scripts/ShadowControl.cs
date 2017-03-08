using UnityEngine;
using System.Collections;

public class ShadowControl : MonoBehaviour {

	public Vector2 pos;

	// Use this for initialization
	void Start () {
		transform.rotation = new Quaternion (0, 0, 0, 0);
		GetComponent<SpriteRenderer> ().enabled = true;
		transform.position = pos;
	}

	public void ShadowSetup(Vector2 position, float size, float opacity){
		GetComponent<SpriteRenderer> ().sortingLayerName = "Fall";
		GetComponent<SpriteRenderer> ().sortingOrder = 0;
		Color tmp = GetComponent<SpriteRenderer> ().color;
		tmp.a = opacity;
		GetComponent<SpriteRenderer> ().color = tmp;
		pos = position;
		transform.localScale = new Vector2 (size, size);
	}
}
