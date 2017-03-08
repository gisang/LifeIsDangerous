using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ShopButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().enabled = false;
		Messenger.AddListener ("PlayerDead", IfPlayerDead);
	}

	void Awake() {
		GetComponent<SpriteRenderer> ().enabled = false;
	}

	void IfPlayerDead() {
		GetComponent<SpriteRenderer> ().enabled = true;
	}

	void OnMouseOver() {
		if (this.gameObject != null) {
			if (GetComponent<SpriteRenderer> ().enabled == true) {
				if (Input.GetMouseButtonDown (0)) {
					SceneManager.LoadScene ("shop");
				}
			}
		}
	} 
}
