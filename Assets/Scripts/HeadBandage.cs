using UnityEngine;
using System.Collections;

public class HeadBandage : MonoBehaviour {

	public Sprite[] moveUp, moveLeft, moveDown, moveRight;

	public GameObject player;
	public PlayerMovement playerScript;
	
	// Update is called once per frame
	void Update () {
		if (playerScript.injured) {
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
		
}
