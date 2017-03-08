using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadtoTitle : MonoBehaviour {

	private int gender;
	public GameObject character, character_w;

	void Awake() {
		gender = PlayerPrefs.GetInt ("Gender");
		switch(gender) {
		case 0:
			character.SetActive (true);
			break;
		case 1:
			character_w.SetActive (true);
			break;
		}
		SceneManager.LoadScene ("gameplay");
	}
}
