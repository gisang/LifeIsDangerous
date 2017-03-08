using UnityEngine;
using System.Collections;

public class CarMovement : MonoBehaviour
{
	private bool is_active;
	private int posIndex, carwidth;
	private float speed, accel;
	private float wherenow;
	private Vector2 startpos, endpos, nowpos;
	private bool is_appear, is_moving;
	private direction carDirection;
	public GameObject player;
	public PlayerMovement playerScript;
	private CarType cartype;
	public ObstacleControl controlScript;
	public DataControl dataControl;
	public SoundControl soundScript;

	public AudioSource crash;

	private float wait;
	private float UPPOS, DOWNPOS, LEFTPOS, RIGHTPOS;
	private float VSTART, HSTART;
	private int animIndex;
	private CarPosition thisCarPos;

	public Sprite[] cars;
	public Sprite[] police;


	private enum direction
	{
		RIGHT,
		LEFT,
		UP,
		DOWN

	}


	public void Initialize (int _carwidth, float _startspd, float _accel, float _wait, CarType _cartype, CarPosition carPos)
	{
		GetComponent<SpriteRenderer> ().enabled = false;
		wherenow = 0.000f;
		speed = _startspd;
		accel = _accel;
		carwidth = _carwidth;
		wait = _wait;
		cartype = _cartype;
		thisCarPos = carPos;
		is_active = true;
		is_appear = false;
		is_moving = false;
		switch (cartype) {
		case CarType.Orange:
			GetComponent<SpriteRenderer> ().sprite = cars [0];
			break;
		case CarType.Blue:
			GetComponent<SpriteRenderer> ().sprite = cars [1];
			break;
		case CarType.Red:
			GetComponent<SpriteRenderer> ().sprite = cars [2];
			break;
		case CarType.Van:
			GetComponent<SpriteRenderer> ().sprite = cars [3];
			break;
		case CarType.Truck:
			GetComponent<SpriteRenderer> ().sprite = cars [4];
			break;
		case CarType.Police:
			GetComponent<SpriteRenderer> ().sprite = police [0];
			animIndex = 0;
			break;
		}
		CarAppear ();
	}

	void Awake ()
	{
		is_active = false;
		//player = GameObject.Find ("Player");
		//playerScript = player.GetComponent<PlayerMovement> ();
		//controlScript = player.GetComponent<ObstacleControl> ();
		//dataControl = GameObject.Find ("grey_bg").GetComponent<DataControl> ();
		//crash = GameObject.Find ("crash").GetComponent<AudioSource> ();
	}



	public void CarAppear ()
	{
		is_appear = false;
		is_moving = false;
		GetComponent<SpriteRenderer> ().enabled = false;
		posIndex = thisCarPos.posIndex;
		carDirection = (direction)thisCarPos.dir;
		controlScript.CarPosControl.Add (thisCarPos);

		wherenow = 0.000f;
		switch (cartype) {
		case CarType.Blue:
		case CarType.Orange:
		case CarType.Red:
		case CarType.Police:
			UPPOS = 6.6f;
			DOWNPOS = -2.4f;
			LEFTPOS = -4.5f;
			RIGHTPOS = 4.5f;
			break;
		case CarType.Truck:
			UPPOS = 9.5f;
			DOWNPOS = -5.5f;
			LEFTPOS = -7.5f;
			RIGHTPOS = 7.5f;
			break;
		case CarType.Van:
			UPPOS = 9.0f;
			DOWNPOS = -6.0f;
			LEFTPOS = -7.0f;
			RIGHTPOS = 7.0f;
			break;
		}
		switch (carwidth) {
		case 1:
			VSTART = (posIndex - 3.5f) * 2.75f / 2.5f;
			HSTART = (posIndex - 3.5f) * 2.75f / 2.5f + 2.05f;
			break;
		case 2:
			VSTART = (posIndex - 3.5f) * 2.75f / 2.5f + 0.5f;
			HSTART = (posIndex - 3.5f) * 2.75f / 2.5f + 2.55f;
			break;
		}
		switch (carDirection) {
		case direction.DOWN:
			transform.rotation = Quaternion.Euler (0, 0, 180);
			startpos = new Vector2 (VSTART, UPPOS);
			endpos = new Vector2 (VSTART, DOWNPOS-1.0f);
			break;
		case direction.UP:
			transform.rotation = Quaternion.Euler (0, 0, 0);
			startpos = new Vector2 (VSTART, DOWNPOS);
			endpos = new Vector2 (VSTART, UPPOS+1.0f);
			break;
		case direction.LEFT:
			transform.rotation = Quaternion.Euler (0, 0, 90);
			startpos = new Vector2 (RIGHTPOS, HSTART);
			endpos = new Vector2 (LEFTPOS-1.0f, HSTART);
			break;
		case direction.RIGHT:
			transform.rotation = Quaternion.Euler (0, 0, 270);
			startpos = new Vector2 (LEFTPOS, HSTART);
			endpos = new Vector2 (RIGHTPOS+1.0f, HSTART);
			break;
		}
		nowpos = Vector2.Lerp (startpos, endpos, wherenow);
		transform.position = nowpos;
		is_appear = true;
		StartCoroutine (CarMove (startpos, endpos, wait));
	}

	public IEnumerator CarMove (Vector2 start, Vector2 end, float wait)
	{
		while (TimeControl.is_paused)
		{
			yield return null;
		}
		GetComponent<SpriteRenderer> ().enabled = true;
		is_moving = false;
		yield return new WaitForSeconds (wait);
		is_moving = true;
		controlScript.CarPosControl.Remove (thisCarPos);
		while (wherenow < 1) {
			while (TimeControl.is_paused) {
				yield return null;
			}
			wherenow += speed; // * Time.deltaTime / (1.0000f / 60.0000f);
			speed += accel;
			//wherenow += speed; // * Time.deltaTime / (1.0000f / 60.0000f);
			//speed += accel;// * Time.deltaTime / (1.0000f / 60.0000f);
			nowpos = Vector2.Lerp (start, end, wherenow);
			transform.position = nowpos;
			if (cartype == CarType.Police) {
				AnimUpdate ();
			}
			yield return null;
		} 
		is_appear = false;
		playerScript.score++;
		Destroy (this.gameObject);
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == "Player") {
			Messenger.Broadcast ("CheckPlayerDies");
			dataControl.deathCar = cartype;
			this.is_appear = false;
			Destroy (this.gameObject);
			if(soundScript._sfx)
				crash.Play ();
		}
	}

	void AnimUpdate ()
	{
		if (Time.frameCount % 7 == 0) {
			if (animIndex == 1) {
				animIndex = 2;
			} else {
				animIndex = 1;
			}
			GetComponent<SpriteRenderer> ().sprite = police [animIndex];
		}
	}

	void OnDestroy ()
	{
		//controlScript.CarPosControl.Remove (thisCarPos);
	}

}