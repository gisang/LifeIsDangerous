using UnityEngine;
using System.Collections;

public class Cheats : MonoBehaviour {

	public GameObject player;
	public PlayerMovement playerScript;

//	void Update() {
//		ResetCheat ();
//		StageCheat ();
////		if (Input.GetKeyDown (KeyCode.Space)) {
////			TimeControl.is_paused = !TimeControl.is_paused;
////			print (TimeControl.is_paused);
////		}
//	}

	void ResetCheat() {
		if (Input.GetKeyDown (KeyCode.R)) {
			PlayerPrefs.SetInt ("SpeedLevel", 0);
			PlayerPrefs.SetInt ("HighScore", 0);
			PlayerPrefs.SetInt ("GenePoint", 0);
			PlayerPrefs.SetInt ("LuckLevel", 0);
		}
	}

	void StageCheat() {
		if (Input.GetKeyDown (KeyCode.S)) {
			playerScript.score = 500;
		}
	}

	void GeneCheat() {
		if (Input.GetKeyDown (KeyCode.G)) {
			playerScript.genepoint = 10000;
		}
	}
}
