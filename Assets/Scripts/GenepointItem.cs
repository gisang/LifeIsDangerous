using UnityEngine;
using System.Collections;

public class GenepointItem : MonoBehaviour
{

	public Sprite[] Sprite = new Sprite[4];
	private float[] pos = new float[2];
	private Vector2 wheretogo;
	public GameObject player;
	public PlayerMovement playerScript;
	public SoundControl soundScript;
	private int stack;

	public AudioSource eat;

	void Start ()
	{
		GetComponent<SpriteRenderer> ().enabled = false;

		Setpos ();
	}

	void Setpos ()
	{
		pos [0] = (int)Random.Range (1.0f, 7.0f);
		if (pos [0] > 6) {
			pos [0] = 6;
		}
		pos [1] = (int)Random.Range (1.0f, 7.0f);
		if (pos [1] > 6) {
			pos [1] = 6;
		}
		wheretogo = new Vector2 ((pos [0] - 3.5f) * 1.1f, -(pos [1] - 3.5f) * 1.1f + 2.1f);
		transform.position = wheretogo;
		transform.localScale = new Vector2 (0.36f, 0.36f);
		if (playerScript.level < 3) {
			stack = 1;
		} else if (playerScript.level < 7) {
			stack = 2;
		} else if(playerScript.level < 13) {
			stack = 3;
		} else {
			stack = 4;
		}
		switch (stack) {
		case 1:
			GetComponent<SpriteRenderer> ().sprite = Sprite [0];
			break;
		case 2:
			GetComponent<SpriteRenderer> ().sprite = Sprite [1];
			break;
		case 3:
			GetComponent<SpriteRenderer> ().sprite = Sprite [2];
			break;
		case 4:
			GetComponent<SpriteRenderer> ().sprite = Sprite [3];
			break;
		default:
			GetComponent<SpriteRenderer> ().sprite = Sprite [3];
			break;
		}
		GetComponent<SpriteRenderer> ().enabled = true;
		StartCoroutine (Appear());
	}

	IEnumerator Appear() {
		while (TimeControl.is_paused) {
			yield return null;
		}
		GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (5.0f);
		Destroy (this.gameObject);
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == "Player") {
			playerScript.genepoint += stack * 100;
			if(soundScript._sfx)
				eat.Play ();
			Destroy (this.gameObject);
		}
		if (col.gameObject.tag == "Item") {
			Destroy (this.gameObject);
		}
	}
}
