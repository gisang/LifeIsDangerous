using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OkayButton : MonoBehaviour {


	void OnMouseOver() {
		if (this.gameObject != null) {
			if (GetComponent<SpriteRenderer> ().enabled == true) {
				if (Input.GetMouseButtonDown (0)) {
					Messenger.Broadcast ("GameRestart");
					//SceneManager.LoadScene ("gameplay");
				}
			}
		}
	}
}
