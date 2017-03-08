using UnityEngine;
using System.Collections;

public class FallingObstacle : MonoBehaviour
{
	private float[] pos = new float[2];
	private Vector2 posVec, wheretogo;
	private Color tmp;
	public Sprite[] Sprites;
	private float wherenow = 0.000f;
	private float speed, accel;
	public GameObject shadow = null;
	public GameObject itsshadow, player, redsplash;
	public PlayerMovement playerScript;
	public int width;
	public DataControl dataControl;
	public SoundControl soundScript;

	public AudioSource smash;

	public enum State
	{
		NONE,
		SHADOW,
		FALLING,
		HIT,
		BROKEN
	}

	public State state;

	void Start ()
	{
		transform.rotation = new Quaternion (0, 0, 0, 0);
		speed = 0.000f;
		accel = 0.002f;
		GetComponent<SpriteRenderer> ().enabled = false;
		pos [0] = 1.0f;
		pos [1] = 1.0f;
		tmp = GetComponent<SpriteRenderer> ().color;
		posVec = new Vector2 (pos [0], pos [1]);
		transform.position = new Vector2 ((posVec.x - 3.5f) * 1.1f, -(posVec.y - 3.5f) * 1.1f + 2.0f);
		state = State.NONE;
		StartFall (width);
	}
		
	
	// Update is called once per frame

	public void StartFall (int _width)
	{
		speed = -0.050f;
		accel = 0.002f;
		wherenow = 0.00f;
		pos [0] = Random.Range (1, 8 - _width);
		if (pos [0] > 6) {
			pos [0] = 6;
		}
		pos [1] = Random.Range (1, 7);
		if (pos [1] > 6) {
			pos [1] = 6;
		}
		wheretogo = new Vector2 ((pos [0] - 3.5f) * 1.1f+0.5f*(width-1), -(pos [1] - 3.5f) * 1.1f + 2.0f+0.2f*(width-1));
		transform.position = wheretogo;
		state = State.SHADOW;
		GetComponent<SpriteRenderer> ().enabled = true;
		transform.localScale = new Vector2 (0.36f, 0.36f);
		StartCoroutine (Fall ());
	}

	IEnumerator Fall ()
	{
		while (TimeControl.is_paused) {
			yield return null;
		}
		GetComponent<SpriteRenderer> ().sortingLayerName = "Fall";
		GetComponent<SpriteRenderer> ().sortingOrder = 1;
		GetComponent<SpriteRenderer> ().sprite = Sprites [0];
		yield return new WaitForSeconds (0.5f);
		GetComponent<SpriteRenderer> ().sprite = Sprites [1];
		yield return new WaitForSeconds (0.5f);
		GetComponent<SpriteRenderer> ().sprite = Sprites [2];
		yield return new WaitForSeconds (0.5f);
		itsshadow = GameObject.Instantiate (shadow, new Vector3 (transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
		itsshadow.SetActive (true);
		ShadowControl shadowControl = itsshadow.GetComponent<ShadowControl> ();
		shadowControl.ShadowSetup (transform.position, 0.36f, 1.0f);

		state = State.FALLING;
		GetComponent<SpriteRenderer> ().sortingLayerName = "Fall";
		GetComponent<SpriteRenderer> ().sortingOrder = 1;
		GetComponent<SpriteRenderer> ().sprite = Sprites [3];
		float size = 0;
		switch (width) {
		case 1:
			size = 0.750f;
			break;
		case 2:
			size = 0.920f;
			break;
		}
		while (wherenow < 1) {
			while (TimeControl.is_paused) {
				yield return null;
			}
			posVec = Vector2.Lerp (new Vector2 (wheretogo.x, wheretogo.y + 5.0f), wheretogo, wherenow);
			transform.position = posVec;
			transform.localScale = new Vector2 (size, size);
			size -= 0.030f;
			wherenow -= speed; // * Time.deltaTime / (1.0000f / 60.0000f);
			speed -= accel; // * Time.deltaTime / (1.0000f / 60.0000f);
			//wherenow -= speed; // * Time.deltaTime / (1.0000f / 60.0000f);
			//speed -= accel; // * Time.åeltaTime / (1.0000f / 60.0000f);
			yield return null;
		} 

		state = State.HIT;
		playerScript.score++;
		GetComponent<SpriteRenderer> ().sortingLayerName = "Grounded";
		GetComponent<SpriteRenderer> ().sortingOrder = 1;
		GetComponent<SpriteRenderer> ().sprite = Sprites [4];
		state = State.BROKEN;
		itsshadow.GetComponent<SpriteRenderer> ().sortingLayerName = "Grounded";
		yield return new WaitForSeconds (0.7f);

		Destroy (itsshadow);
		Destroy (this.gameObject);

	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == "Player") {
			if (wherenow > 0.80f && state == State.FALLING /*&& playerScript.pos [0] == pos [0] && playerScript.pos [1] == pos [1]*/) {
				if(soundScript._sfx)
					smash.Play ();
				GameObject red = GameObject.Instantiate (redsplash, null) as GameObject;
				red.SetActive (true);
				dataControl.deathCar = CarType.None;
				if (!playerScript.helmetOn){
					Destroy (itsshadow);
					playerScript.PlayerDies ();
					Destroy (gameObject);
				} else {
					playerScript.helmetOn = false;
					Destroy (itsshadow);
					Destroy (gameObject);
				}
			}
		}
	}
}
