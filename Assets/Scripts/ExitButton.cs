using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{

	void Start ()
	{
		GetComponent<SpriteRenderer> ().enabled = false;
		Messenger.AddListener ("PlayerDead", IfPlayerDead);
	}

	void Awake ()
	{
		GetComponent<SpriteRenderer> ().enabled = false;
	}

	void IfPlayerDead ()
	{
		if (this.gameObject != null) {
			GetComponent<SpriteRenderer> ().enabled = true;
		}
	}

	void OnMouseOver ()
	{
		if (this.gameObject != null) {
			if (GetComponent<SpriteRenderer> ().enabled == true) {
				if (Input.GetMouseButtonDown (0)) {
					SceneManager.LoadScene ("title");
				}
			}
		}
	}
}
