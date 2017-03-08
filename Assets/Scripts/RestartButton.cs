using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour {

	public PlayerMovement playerScript;
	public GameObject dark;
	public GameObject restartPopup;
	public GameObject[] buttons;

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().enabled = false;
		dark.SetActive (false);
		restartPopup.SetActive (false);
		Messenger.AddListener ("PlayerDead", IfPlayerDead);
	}

	void Awake() {
		GetComponent<SpriteRenderer> ().enabled = false;
		//playerScript = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
		//dark = GameObject.Find ("dark");
		//restartPopup = GameObject.Find ("RestartPopup");
		dark.SetActive (false);

	}

	void IfPlayerDead() {
		GetComponent<SpriteRenderer> ().enabled = true;
		dark.SetActive (true);
	}

	void OnMouseOver() {
		if (this.gameObject != null) {
			if (GetComponent<SpriteRenderer> ().enabled == true) {
				if (Input.GetMouseButtonDown (0)) {
					int cheap = PlayerPrefs.GetInt ("Cheapest");
					if (playerScript.genepoint < cheap) {
						Messenger.Broadcast ("GameRestart");
						//SceneManager.LoadScene ("gameplay");
					} else {
						restartPopup.SetActive (true);
						Messenger.Broadcast ("RestartPopup");
					}
				}
			}
		}
	}
}
