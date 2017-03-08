using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;



public enum hairstyle
{
	BASIC,
	W_BASIC
}

public enum skinstyle
{
	BASIC
}

public enum face
{
	BASIC
}

public enum costume
{
	SUIT,
	W_SUIT
}

[System.Serializable]
public class PlayerHair
{
	public hairstyle hs;
	public Sprite[] hairLeft, hairUp, hairDown, hairRight;
}

[System.Serializable]
public class PlayerSkin
{
	public skinstyle ss;
	public Sprite[] skinLeft, skinDown, skinUp, skinRight;
}

[System.Serializable]
public class PlayerEye
{
	public face f;
	public Sprite[] eyeLeft, eyeDown, eyeRight;
}

[System.Serializable]
public class PlayerCos
{
	public costume cc;
	public Sprite[] cosLeft, cosUp, cosDown, cosRight;
	public Sprite[] inj_cosLeft, inj_cosUp, inj_cosDown, inj_cosRight;
}

[System.Serializable]
public class PlayerHand
{
	public skinstyle hs;
	public Sprite[] handLeft, handUp, handDown, handRight;
	public Sprite[] inj_handLeft, inj_handUp, inj_handDown, inj_handRight;
}

public static class TimeControl
{
	public static bool is_paused = false;
}

public class PlayerMovement : MonoBehaviour
{
	[System.NonSerialized] public int score = 0;
	[System.NonSerialized] public int level = 0;
	[System.NonSerialized] public int genepoint = 0;
	[System.NonSerialized] public int highscore = 0;

	public UnityEngine.UI.Text GUI_score;
	public UnityEngine.UI.Text GUI_genepoint;
	public UnityEngine.UI.Text GUI_highscore;

	public float[] pos = new float[2];
	private float gridSize = 116.0f;
	private float wherenow = 0.0000f;
	private float charspd, CHARSPD;
	private float accel;
	private int speedLv, luckLv;
	public int gender, life;

	public AudioSource bgm;
	public AudioSource footstep;

	public bool helmetOn;

	public GameObject restartButton = null;
	public GameObject helmet;
	public GameObject tomb, redsplash, keycanvas;

	private Vector2 wheretogo;
	private Vector2 posVec;

	private bool is_moving = false;
	private bool time = false;
	public bool injured;

	IEnumerator moveAnim;
	IEnumerator standAnim;

	public int animIndex;

	public GameObject hair, eye, skin, cos, hand;
	public costume cosType;
	public hairstyle hairStyle;

	public PlayerHair[] hairStyles;
	public PlayerHair playerHair;
	public PlayerSkin[] skinTypes;
	public PlayerSkin playerSkin;
	public PlayerEye[] faces;
	public PlayerEye playerEye;
	public PlayerCos[] costumes;
	public PlayerCos playerCos;
	public PlayerHand[] handTypes;
	public PlayerHand playerHand;

	public SoundControl soundScript;
	public AudioSource traffic;

	bool controller;

	//Animator skinAnim;

	public enum direction
	{
		RIGHT,
		LEFT,
		UP,
		DOWN
	}

	public direction CharMov = direction.DOWN;

	void Awake ()
	{
		CHARSPD = 0.0700f;
		accel = 0.0026f;
		helmetOn = false;
	}

	// Use this for initialization
	void Start ()
	{
		injured = false;
		int kitOn = PlayerPrefs.GetInt ("KitOn", 0);
		if (kitOn == 0)
			life = 1;
		else
			life = 2;
		Messenger.AddListener ("CheckPlayerDies", CheckPlayerDies);

		animIndex = 0;

		pos [0] = 4.0f;
		pos [1] = 3.0f;
		posVec = new Vector2 (pos [0], pos [1]);
		transform.position = new Vector2 ((posVec.x - 3.5f) * 1.1f, -(posVec.y - 3.5f) * 1.1f + 2.0f);
		tomb.SetActive (false);
		gender = PlayerPrefs.GetInt ("Gender", 0);
		speedLv = PlayerPrefs.GetInt ("SpeedLevel", 0);
		luckLv = PlayerPrefs.GetInt ("LuckLevel", 0);
		highscore = PlayerPrefs.GetInt ("HighScore", 0);
		controller = (PlayerPrefs.GetInt ("Controller", 0) == 0) ? false : true;
		CHARSPD = 0.0700f + speedLv * 0.0015f;

		hairStyle = (gender == 0) ? (hairstyle)PlayerPrefs.GetInt ("HairStyle", (int)hairstyle.BASIC) : (hairstyle)PlayerPrefs.GetInt ("W_HairStyle", (int)hairstyle.W_BASIC);
		cosType = (gender == 0) ? (costume)PlayerPrefs.GetInt ("Costume", (int)costume.SUIT) : (costume)PlayerPrefs.GetInt ("W_Costume", (int)costume.W_SUIT);

		playerHair = hairStyles [(int)hairStyle];
		for (int i = 0; i < hairStyles.Length; i++) {
			if (hairStyles [i].hs == hairStyle) {
				playerHair = hairStyles [i];
				break;
			}
		}
		playerCos = costumes [0];
		for (int i = 0; i < costumes.Length; i++) {
			if (costumes [i].cc == cosType) {
				playerCos = costumes [i];
				break;
			}
		}
		playerSkin = skinTypes [0];
		playerEye = faces [0];
		playerHand = handTypes [0];

		StartCoroutine (scoreUp ());
		is_moving = false;
		standAnim = standIndex ();
		StartCoroutine (standAnim);

		transform.localScale = new Vector2 (0.07f, 0.07f);

		if (soundScript._sfx) {
			traffic.Play ();
		}
		if (controller) 
			keycanvas.SetActive (true);
		else
			keycanvas.SetActive (false);
	}

	void TimeSlow ()
	{
		if (Input.GetKeyDown (KeyCode.T)) {
			time = !time;
		}
		if (time) {
			Time.timeScale = 0.10f;
		} else {
			Time.timeScale = 1.00f;
		}
	}

	// Update is called once per frame

	public void MoveRight() {
		if (!is_moving) {
			dirUpdate (direction.RIGHT);
			animUpdate (CharMov, animIndex);
			if (pos [0] < 6) {
				is_moving = true;	
				//transform.rotation = new Quaternion (0, 180, 0, 0);
				wheretogo = new Vector2 (pos [0] + 1, pos [1]);
				StartCoroutine (move ());
			}
		}
	}

	public void MoveLeft() {
		if (!is_moving) {
			dirUpdate (direction.LEFT);
			animUpdate (CharMov, animIndex);
			if (pos [0] > 1) {
				is_moving = true;	
				//transform.rotation = new Quaternion (0, 0, 0, 0);
				wheretogo = new Vector2 (pos [0] - 1, pos [1]);
				StartCoroutine (move ());
			}
		}
	}

	public void MoveUp() {
		if (!is_moving) {
			dirUpdate (direction.UP);
			animUpdate (CharMov, animIndex);
			if (pos [1] > 1) {
				is_moving = true;					
				//transform.rotation = new Quaternion (0, 0, 0, 0);
				wheretogo = new Vector2 (pos [0], pos [1] - 1);
				StartCoroutine (move ());	
			}
		}
	}

	public void MoveDown() {
		if (!is_moving) {
			dirUpdate (direction.DOWN);
			animUpdate (CharMov, animIndex);
			if (pos [1] < 6) {
				is_moving = true;	
				//transform.rotation = new Quaternion (0, 180, 0, 0);
				wheretogo = new Vector2 (pos [0], pos [1] + 1);
				StartCoroutine (move ());
			}
		}
	}

	void Update ()
	{	
		LevelUpdate ();

		GUI_score.text = string.Format ("{0}", score);

		GUI_genepoint.text = string.Format ("{0}", genepoint);
		GUI_highscore.text = string.Format ("{0}", highscore);

		if (controller) {
			if (Input.GetKeyDown (KeyCode.RightArrow)) {
				MoveRight ();
			} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				MoveLeft ();
			} else if (Input.GetKeyDown (KeyCode.UpArrow)) {
				MoveUp ();
			} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
				MoveDown ();
			}
		} else {
			if (SwipeManager.IsSwipingRight ()) {
				MoveRight ();
			} else if (SwipeManager.IsSwipingLeft ()) {
				MoveLeft ();
			} else if (SwipeManager.IsSwipingUp ()) {
				MoveUp ();
			} else if (SwipeManager.IsSwipingDown ()) {
				MoveDown ();
			}
		}
		transform.position = new Vector2 ((posVec.x - 3.5f) * 1.1f, -(posVec.y - 3.5f) * 1.1f + 2.4f);
	}

	void dirUpdate (direction dir)
	{
		CharMov = dir;
		//skinAnim.SetInteger ("direction", (int)dir);
	}

	void moveUpdate ()
	{
		is_moving = !is_moving;
		//skinAnim.SetBool ("moving", is_moving);
	}

	IEnumerator move ()
	{
		wherenow = 0.0000f;
		charspd = CHARSPD;
		if (soundScript._sfx)
			footstep.Play ();
		is_moving = true;
		moveAnim = moveIndex ();
		StopCoroutine (standAnim);
		StartCoroutine (moveAnim);
		while (wherenow < 0.95f) {
			posVec = Vector2.Lerp (new Vector2 (pos [0], pos [1]), wheretogo, wherenow);
			if (injured) {
				wherenow += charspd * 0.5f; // * Time.deltaTime* (1.0000f / 60.0000f);
				charspd -= accel * 0.4f; // * Time.deltaTime* (1.0000f / 60.0000f);
			} else {
				wherenow += charspd; // * Time.deltaTime* (1.0000f / 60.0000f);
				charspd -= accel; // * Time.deltaTime* (1.0000f / 60.0000f);
			}
			yield return null;
		} 
		posVec = wheretogo;
		pos [0] = wheretogo.x;
		pos [1] = wheretogo.y;
		moveUpdate ();
		is_moving = false;
		StopCoroutine (moveAnim);
		standAnim = standIndex ();
		StartCoroutine (standAnim);
	}

	IEnumerator standIndex ()
	{
		while (!is_moving) {
			if (animIndex == 1) {
				animIndex = 0;
			} else {
				animIndex = 1;
			}
			animUpdate (CharMov, animIndex);
			for (int i = 0; i < 25; i++) {
				yield return null;
			}
		}
		yield break;
	}

	IEnumerator moveIndex ()
	{
		while (is_moving) {
			if (animIndex == 2) {
				animIndex = 3;
			} else {
				animIndex = 2;
			}
			animUpdate (CharMov, animIndex);
			for (int i = 0; i < 10; i++) {
				yield return null;
			}
		}
		yield break;
	}

	void animUpdate (direction dir, int index)
	{
//		if (dir == direction.RIGHT) {
//			transform.rotation = new Quaternion (0, 180, 0, 0);
//		} else {
//			transform.rotation = new Quaternion (0, 0, 0, 0);
//		}
		skinUpdate (dir, index);
		hairUpdate (dir, index);
		eyeUpdate (dir, index);
		cosUpdate (dir, index);
		handUpdate (dir, index);
	}

	void LevelUpdate ()
	{
		level = ((int)score / 20) + 1;
	}

	IEnumerator scoreUp ()
	{
		while (TimeControl.is_paused) {
			yield return null;
		}
		while (true) {
			yield return new WaitForSeconds (1.0f);
			score++;
		}
	}

	void hairUpdate (direction dir, int animIndex)
	{
		SpriteRenderer spr = hair.GetComponent<SpriteRenderer> ();
		spr.enabled = true;
		switch (dir) {
		case direction.LEFT:
			spr.sprite = playerHair.hairLeft [0];
			break;
		case direction.RIGHT:
			spr.sprite = playerHair.hairRight [0];
			break;
		case direction.UP:
			spr.sprite = playerHair.hairUp [0];
			break;
		case direction.DOWN:
			spr.sprite = playerHair.hairDown [0];
			break;
		}
		hair.transform.position = new Vector2 (this.transform.position.x + 0.0f, this.transform.position.y - 0.06f * (animIndex % 2));
	}

	void eyeUpdate (direction dir, int animIndex)
	{
		SpriteRenderer spr = eye.GetComponent<SpriteRenderer> ();
		switch (dir) {
		case direction.LEFT:
			spr.enabled = true;
			spr.sprite = playerEye.eyeLeft [0];
			break;
		case direction.RIGHT:
			spr.enabled = true;
			spr.sprite = playerEye.eyeRight [0];
			break;
		case direction.DOWN:
			spr.enabled = true;
			spr.sprite = playerEye.eyeDown [0];
			break;
		case direction.UP:
			spr.enabled = false;
			break;
		}
		eye.transform.position = new Vector2 (this.transform.position.x + 0.0f, this.transform.position.y - 0.06f * (animIndex % 2));
	}

	void skinUpdate (direction dir, int animIndex)
	{
		SpriteRenderer spr = skin.GetComponent<SpriteRenderer> ();
		switch (dir) {
		case direction.LEFT:
			spr.sprite = playerSkin.skinLeft [0];
			break;
		case direction.RIGHT:
			spr.sprite = playerSkin.skinRight [0];
			break;
		case direction.DOWN:
			spr.sprite = playerSkin.skinDown [0];
			break;
		case direction.UP:
			spr.sprite = playerSkin.skinUp [0];
			break;
		}
		skin.transform.position = new Vector2 (this.transform.position.x + 0.0f, this.transform.position.y - 0.06f * (animIndex % 2));
	}

	void handUpdate (direction dir, int animIndex)
	{
		SpriteRenderer spr = hand.GetComponent<SpriteRenderer> ();
		switch (dir) {
		case direction.LEFT:
			if (injured)
				spr.sprite = playerHand.inj_handLeft [animIndex];
			else
				spr.sprite = playerHand.handLeft [animIndex];
			break;
		case direction.RIGHT:
			if (injured)
				spr.sprite = playerHand.inj_handRight [animIndex];
			else
				spr.sprite = playerHand.handRight [animIndex];
			break;
		case direction.DOWN:
			if (injured)
				spr.sprite = playerHand.inj_handDown [animIndex];
			else
				spr.sprite = playerHand.handDown [animIndex];
			break;
		case direction.UP:
			if (injured)
				spr.sprite = playerHand.inj_handUp [animIndex];
			else
				spr.sprite = playerHand.handUp [animIndex];
			break;
		}
	}

	void cosUpdate (direction dir, int animIndex)
	{
		SpriteRenderer spr = cos.GetComponent<SpriteRenderer> ();
		switch (dir) {
		case direction.LEFT:
			if (injured)
				spr.sprite = playerCos.inj_cosLeft [animIndex];
			else
				spr.sprite = playerCos.cosLeft [animIndex];
			break;
		case direction.RIGHT:
			if (injured)
				spr.sprite = playerCos.inj_cosRight [animIndex];
			else
				spr.sprite = playerCos.cosRight [animIndex];
			break;
		case direction.DOWN:
			if (injured)
				spr.sprite = playerCos.inj_cosDown [animIndex];
			else
				spr.sprite = playerCos.cosDown [animIndex];
			break;
		case direction.UP:
			if (injured)
				spr.sprite = playerCos.inj_cosUp [animIndex];
			else
				spr.sprite = playerCos.cosUp [animIndex];
			break;
		}
	}

	void CheckPlayerDies ()
	{
		GameObject red = GameObject.Instantiate (redsplash, null) as GameObject;
		red.SetActive (true);
		if (life > 1) {
			life = 1;
		} else if (life == 1) {
			int ran = Random.Range (0, 100);
			if (ran < luckLv) {
				life = 0;
				injured = true;
			} else {
				PlayerDies ();
			}
		} else {
			PlayerDies ();
		}
	}


	public void PlayerDies ()
	{
		PlayerPrefs.SetInt ("KitOn", 0);
		tomb.SetActive (true);
		tomb.transform.position = this.gameObject.transform.position;
		bgm.Stop ();
		Messenger.Broadcast ("PlayerDies");
	}
}

