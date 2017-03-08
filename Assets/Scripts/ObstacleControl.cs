using UnityEngine;
using System.Collections;

public enum CarType
{
	Orange,
	Blue,
	Red,
	Police,
	Van,
	Truck,
	None,
	NumberOfTypes
}

public class ObstacleControl : MonoBehaviour
{

	public GameObject car;
	public GameObject van;
	public GameObject truck;
	public GameObject fall, fall2;
	private GameObject FALL = null;
	public GameObject player;
	private float oCarAppearChance, bCarAppearChance, rCarAppearChance, policeAppearChance, vanAppearChance, truckAppearChance;
	private float nCarAppearTime, policeAppearTime, vanAppearTime, truckAppearTime;
	public float[] fallAppearChance;
	public float[] fall2AppearChance;
	private float fallAppearTime;
	private GameObject normalCar;
	private CarMovement nCarScript;
	public AudioSource horn, police, carSound;
	bool[] carOn;
	CarPosition[] carPositions;
	int carVar = (int)CarType.NumberOfTypes;
	bool playerAlive;
	public SoundControl soundScript;

	public ArrayList CarPosControl;

	public UnityEngine.UI.Text GUI_level;
	public PlayerMovement playerScript;

	public int level;

	// Use this for initialization
	void Start ()
	{
		oCarAppearChance = 1.00f;
		bCarAppearChance = 0.00f;
		rCarAppearChance = 0.00f;
		policeAppearChance = 0.00f;
		vanAppearChance = 0.00f;
		truckAppearChance = 0.00f;

		nCarAppearTime = 1.00f;
		policeAppearTime = 4.00f;
		vanAppearTime = 1.00f;
		truckAppearTime = 6.00f;

		fallAppearTime = 1.00f;

		playerAlive = true;

		StartCoroutine (MakeCar ());
		StartCoroutine (MakeVan ());
		StartCoroutine (MakePolice ());
		StartCoroutine (MakeTruck ());
		StartCoroutine (MakeFall ());
	}

	void Awake ()
	{
		CarPosControl = new ArrayList ();
		Messenger.AddListener ("PlayerDead", IfPlayerDead);
		fallAppearChance = new float[4];
		fall2AppearChance = new float[2];
		carOn = new bool[carVar];
		carPositions = new CarPosition[carVar];
	}

	void IfPlayerDead ()
	{
		playerAlive = false;
	}

	// Update is called once per frame
	void Update ()
	{
		levelUpdate ();
		CheckPos ();
	}

	IEnumerator MakeFall ()
	{
		while (playerAlive) {
			while (TimeControl.is_paused)
			{
				yield return null;
			}
			yield return new WaitForSeconds (fallAppearTime);
			if (Random.Range (0.00f, 1.00f) < fallAppearChance[0]) {
				FALL = GameObject.Instantiate (fall, null) as GameObject;
				FALL.GetComponent<FallingObstacle> ().width = 1;
				FALL.SetActive (true);
			}
			if (Random.Range (0.00f, 1.00f) < fallAppearChance[1]) {
				FALL = GameObject.Instantiate (fall, null) as GameObject;
				FALL.GetComponent<FallingObstacle> ().width = 1;
				FALL.SetActive (true);
			}
			if (Random.Range (0.00f, 1.00f) < fallAppearChance[2]) {
				FALL = GameObject.Instantiate (fall, null) as GameObject;
				FALL.GetComponent<FallingObstacle> ().width = 1;
				FALL.SetActive (true);
			}
			if (Random.Range (0.00f, 1.00f) < fallAppearChance[3]) {
				FALL = GameObject.Instantiate (fall, null) as GameObject;
				FALL.GetComponent<FallingObstacle> ().width = 1;
				FALL.SetActive (true);
			}
			if (Random.Range (0.00f, 1.00f) < fall2AppearChance[0]) {
				FALL = GameObject.Instantiate (fall2, null) as GameObject;
				FALL.GetComponent<FallingObstacle> ().width = 2;
				FALL.SetActive (true);
			}
			if (Random.Range (0.00f, 1.00f) < fall2AppearChance[1]) {
				FALL = GameObject.Instantiate (fall2, null) as GameObject;
				FALL.GetComponent<FallingObstacle> ().width = 2;
				FALL.SetActive (true);
			}
		}
	}

	IEnumerator MakeCar ()
	{
		while (playerAlive) {
			while (TimeControl.is_paused)
			{
				yield return null;
			}
			yield return new WaitForSeconds (nCarAppearTime);
			//horn.Play ();
			if (Random.Range (0.00f, 1.00f) < oCarAppearChance) {
				carOn [(int)CarType.Orange] = true;
			}
			if (Random.Range (0.00f, 1.00f) < bCarAppearChance) {
				carOn [(int)CarType.Blue] = true;
			}
			if (Random.Range (0.00f, 1.00f) < rCarAppearChance) {
				carOn [(int)CarType.Red] = true;
			}
			CheckPos ();
		}
	}

	IEnumerator MakeVan ()
	{	
		while (playerAlive) {
			while (TimeControl.is_paused)
			{
				yield return null;
			}
			yield return new WaitForSeconds (vanAppearTime);
			if (Random.Range (0.00f, 1.00f) < vanAppearChance) {
				carOn [(int)CarType.Van] = true;
			}
			CheckPos ();
		}
	}

	IEnumerator MakePolice ()
	{
		while (playerAlive) {
			while (TimeControl.is_paused)
			{
				yield return null;
			}
			yield return new WaitForSeconds (policeAppearTime);
			if (Random.Range (0.00f, 1.00f) < policeAppearChance) {
				carOn [(int)CarType.Police] = true;
				if(soundScript._sfx)
					police.Play ();
			}
			CheckPos ();
		}
	}

	IEnumerator MakeTruck ()
	{
		while (playerAlive) {
			while (TimeControl.is_paused)
			{
				yield return null;
			}
			yield return new WaitForSeconds (truckAppearTime);
			if (Random.Range (0.00f, 1.00f) < truckAppearChance) {
				carOn [(int)CarType.Truck] = true;
				if(soundScript._sfx)
					carSound.Play ();
			}
			CheckPos ();
		}
	}

	void CheckPos ()
	{
		// make random positions
		if (carOn [(int)CarType.Orange] == true) {
			carPositions [(int)CarType.Orange] = createPosition (1);
			OrangeCar (carPositions [(int)CarType.Orange]);
			carOn [(int)CarType.Orange] = false;
		}
		if (carOn [(int)CarType.Blue] == true) {
			carPositions [(int)CarType.Blue] = createPosition (1);
			BlueCar (carPositions [(int)CarType.Blue]);
			carOn [(int)CarType.Blue] = false;
		}
		if (carOn [(int)CarType.Red] == true) {
			carPositions [(int)CarType.Red] = createPosition (1);
			RedCar (carPositions [(int)CarType.Red]);
			carOn [(int)CarType.Red] = false;
		}
		if (carOn [(int)CarType.Van] == true) {
			carPositions [(int)CarType.Van] = createPosition (1);
			VanCar (carPositions [(int)CarType.Van]);
			carOn [(int)CarType.Van] = false;
		}
		if (carOn [(int)CarType.Police] == true) {
			carPositions [(int)CarType.Police] = createPosition (1);
			PoliceCar (carPositions [(int)CarType.Police]);
			carOn [(int)CarType.Police] = false;
		}
		if (carOn [(int)CarType.Truck] == true) {
			carPositions [(int)CarType.Truck] = createPosition (2);
			TruckCar (carPositions [(int)CarType.Truck]);
			carOn [(int)CarType.Truck] = false;
		}
	}

	private CarPosition createPosition (int carwidth)
	{
		int carDirection = Random.Range (0, 4);
		int posIndex = Random.Range (1, 8 - carwidth);
		if (posIndex > 8 - carwidth) {
			posIndex = 8 - carwidth;
		}

		if (!isNewPosition (posIndex, carDirection, carwidth)) {
			return createPosition (carwidth);
		}

		CarPosition carPos = new CarPosition (posIndex, carDirection, carwidth);
		return carPos;

	}

	private bool isNewPosition (int posIndex, int carDir, int carWidth)
	{
		//print (controlScript.CarPosControl.Count);
		foreach (CarPosition carPos in carPositions) {
			if (carPos != null) {
				if (posIndex == carPos.posIndex && carDir == carPos.dir) {
					return false;
				}
				if (carPos.width != 1) {
					if (posIndex + carWidth - 1 == carPos.posIndex && carDir == carPos.dir) {
						return false;
					}
				}
			}
		}
		return true;
	}

	void levelUpdate ()
	{
		level = playerScript.level;
		GUI_level.text = string.Format ("{0}", level);
		nCarAppearTime = 2.05f - level * 0.05f;
		if (nCarAppearTime < 0.75f) {
			nCarAppearTime = 0.75f;
		}
		fallAppearTime = nCarAppearTime * 1.5f;
		policeAppearTime = nCarAppearTime * 4;
		truckAppearTime = nCarAppearTime * 6;
		vanAppearTime = nCarAppearTime * 2;
		if (level >= 1) {
			oCarAppearChance = 1.00f;
			fallAppearChance [0] = 1.00f;
			//fall2AppearChance [0] = 1.00f;
		}
		if (level >= 2) {
			bCarAppearChance = 0.40f + 0.10f * level;
		}
		if (level >= 3) {
			rCarAppearChance = 0.10f + 0.10f * level;
			fallAppearChance [1] = 0.25f + 0.05f * level;
		}
		if (level >= 5) {
			vanAppearChance = 0.35f + 0.05f * level;
			fallAppearChance [2] = 0.15f + 0.05f * level;
			fall2AppearChance[0] =  0.15f + 0.05f * level;
		}
		if (level >= 7) {
			policeAppearChance = 0.20f + 0.05f * level;
			fallAppearChance [3] = 0.05f + 0.05f * level;
		}
		if (level >= 9) {
			truckAppearChance = 0.10f+0.05f * level;
			fall2AppearChance[1] = 0.05f * level-0.05f;
		}
	}

	void OrangeCar (CarPosition carpos)
	{
		int CARWIDTH = 1;
		float STARTSPD = 0.0000f;
		float ACCEL = 0.0005f;
		float WAIT = nCarAppearTime * 0.6f;
		normalCar = GameObject.Instantiate (car, null) as GameObject;
		normalCar.SetActive (true);
		nCarScript = normalCar.GetComponent<CarMovement> ();
		nCarScript.Initialize (CARWIDTH, STARTSPD, ACCEL, WAIT, CarType.Orange, carpos);
	}

	void BlueCar (CarPosition carpos)
	{
		int CARWIDTH = 1;
		float STARTSPD = 0.0000f;
		float ACCEL = 0.0007f;
		float WAIT = nCarAppearTime * 0.6f;
		normalCar = GameObject.Instantiate (car, null) as GameObject;
		normalCar.SetActive (true);
		nCarScript = normalCar.GetComponent<CarMovement> ();
		nCarScript.Initialize (CARWIDTH, STARTSPD, ACCEL, WAIT, CarType.Blue, carpos);
	}

	void RedCar (CarPosition carpos)
	{
		int CARWIDTH = 1;
		float STARTSPD = 0.0000f;
		float ACCEL = 0.0010f;
		float WAIT = nCarAppearTime * 0.6f;
		normalCar = GameObject.Instantiate (car, null) as GameObject;
		normalCar.SetActive (true);
		nCarScript = normalCar.GetComponent<CarMovement> ();
		nCarScript.Initialize (CARWIDTH, STARTSPD, ACCEL, WAIT, CarType.Red, carpos);
	}

	void PoliceCar (CarPosition carpos)
	{
		int CARWIDTH = 1;
		float STARTSPD = 0.0030f;
		float ACCEL = 0.0010f;
		float WAIT = nCarAppearTime * 0.6f * 0.35f;
		normalCar = GameObject.Instantiate (car, null) as GameObject;
		normalCar.SetActive (true);
		nCarScript = normalCar.GetComponent<CarMovement> ();
		nCarScript.Initialize (CARWIDTH, STARTSPD, ACCEL, WAIT, CarType.Police, carpos); 
	}

	void VanCar (CarPosition carpos)
	{
		int CARWIDTH = 1;
		float STARTSPD = 0.0020f;
		float ACCEL = 0.0001f;
		float WAIT = 0.0f;
		normalCar = GameObject.Instantiate (van, null) as GameObject;
		normalCar.SetActive (true);
		nCarScript = normalCar.GetComponent<CarMovement> ();
		nCarScript.Initialize (CARWIDTH, STARTSPD, ACCEL, WAIT, CarType.Van, carpos); 
	}

	void TruckCar (CarPosition carpos)
	{
		int CARWIDTH = 2;
		float STARTSPD = 0.0010f;
		float ACCEL = 0.0001f;
		float WAIT = 0.0f;
		normalCar = GameObject.Instantiate (truck, null) as GameObject;
		normalCar.SetActive (true);
		nCarScript = normalCar.GetComponent<CarMovement> ();
		nCarScript.Initialize (CARWIDTH, STARTSPD, ACCEL, WAIT, CarType.Truck, carpos); 
	}
}
