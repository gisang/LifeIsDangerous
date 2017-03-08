using UnityEngine;
using System.Collections;

public class HelmetControl : MonoBehaviour {

	public Sprite[] moveUp, moveLeft, moveDown, moveRight;

	public GameObject player;
	public PlayerMovement playerScript;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt ("HelmetOn") == 0) {
			playerScript.helmetOn = false;
		} else {
			playerScript.helmetOn = true;
		}
	}

	void Awake() {
		//player = GameObject.Find ("Player");
		//playerScript = player.GetComponent<PlayerMovement> ();
		Messenger.AddListener ("PlayerDead", PlayerDead);
		//PlayerPrefs.SetInt ("HelmetOn", 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (playerScript.helmetOn) {
			GetComponent<SpriteRenderer> ().enabled = true;
		} else {
			GetComponent<SpriteRenderer> ().enabled = false;
		}
		animUpdate ();
	}

	void animUpdate() {
		switch (playerScript.CharMov) {
		case PlayerMovement.direction.LEFT:
			GetComponent<SpriteRenderer> ().sprite = moveLeft [0];
			break;
		case PlayerMovement.direction.RIGHT:
			GetComponent<SpriteRenderer> ().sprite = moveRight [0];
			break;
		case PlayerMovement.direction.UP:
			GetComponent<SpriteRenderer> ().sprite = moveUp [0];
			break;
		case PlayerMovement.direction.DOWN:
			GetComponent<SpriteRenderer> ().sprite = moveDown [0];
			break;
		}
	}

	void PlayerDead() {
		PlayerPrefs.SetInt ("HelmetOn", 0);
	}
}
