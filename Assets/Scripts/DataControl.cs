using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataControl : MonoBehaviour
{

	private int highScore;
	private int currentScore;
	private int genePoint;
	private int gender;
	public CarType deathCar = CarType.None;
	private int generation = 1;

	//public UnityEngine.UI.Text GUI_genePoint;
	public Text GUI_currentScore;
	public Text GUI_generation;
	public Text causeofdeath;
	public Text dead;

	private Color32 deadColor;

	public GameObject deathframe;
	public GameObject deathframe_w;
	public GameObject newhigh;
	public GameObject title, redsplash;
	public GameObject loading, loadcos, loadcos_w, loadhair, loadhair_w;
	public ColorSwap loadskin, loadeye, loadhand;
	public GameObject[] buttons;

	public GameObject player;

	public ColorSwap framecolor, framecolor_w;

	public PlayerMovement playerScript;

	void Awake ()
	{
		loading.SetActive (false);
	}

	void Start ()
	{
		Messenger.AddListener ("PlayerDead", PlayerDead);
		Messenger.AddListener ("GameRestart", WhenRestarted);
		Messenger.AddListener ("RestartPopup", RestartPopup);
		Messenger.AddListener ("PlayerDies", _PlayerDies);
		title.GetComponent<SpriteRenderer> ().enabled = true;
		deathCar = CarType.None;
		causeofdeath.enabled = false;
		dead.enabled = false;
		newhigh.SetActive (false);
		deathframe.SetActive (false);
		deathframe_w.SetActive (false);

		generation = PlayerPrefs.GetInt ("Generation");
		GUI_currentScore.enabled = false;
		GUI_generation.enabled = true;
		if (10 < (generation % 100) && (generation % 100) < 20) {
			GUI_generation.text = string.Format ("{0}th Gen.", generation);
		} else {
			switch (generation % 10) {
			case 1:
				GUI_generation.text = string.Format ("{0}st Gen.", generation);
				break;
			case 2:
				GUI_generation.text = string.Format ("{0}nd Gen.", generation);
				break;
			case 3:
				GUI_generation.text = string.Format ("{0}rd Gen.", generation);
				break;
			default:
				GUI_generation.text = string.Format ("{0}th Gen.", generation);
				break;
			}
		}
	}

	void PlayerDead ()
	{
		title.SetActive (false);
		gender = playerScript.gender;
		genePoint = playerScript.genepoint;
		currentScore = playerScript.score;
		highScore = PlayerPrefs.GetInt ("HighScore");
		if (highScore < currentScore) {
			highScore = currentScore;
			newhigh.SetActive (true);
		}
		PlayerPrefs.SetInt ("Score", currentScore);
		PlayerPrefs.SetInt ("HighScore", highScore);
		PlayerPrefs.SetInt ("GenePoint", genePoint);
		PlayerPrefs.SetInt ("HelmetOn", 0);

		dead.enabled = true;
		switch (gender) {
		case 0:
			deathframe.SetActive (true);
			framecolor.SetColor ("ALL");
			break;
		case 1:
			deathframe_w.SetActive (true);
			framecolor_w.SetColor ("ALL");
			break;
		}
		//EditorUtility.SetDirty(causeofdeath);
		switch (deathCar) {
		case CarType.Orange:
			deadColor = new Color32 (247, 79, 9, 255);
			causeofdeath.text = "Car Accident";
			break;
		case CarType.Blue:
			deadColor = new Color32 (32, 153, 227, 255);
			causeofdeath.text = "Car Accident";
			break;
		case CarType.Red:
			deadColor = new Color32 (227, 30, 28, 255);
			causeofdeath.text = "Car Accident";
			break;
		case CarType.Van:
			deadColor = new Color32 (199, 194, 191, 255);
			causeofdeath.text = "Hit by a Van";
			break;
		case CarType.Police:
			deadColor = new Color32 (150, 150, 150, 255);
			causeofdeath.text = "Police Accident";
			break;
		case CarType.Truck:
			deadColor = new Color32 (239, 231, 223, 255);
			causeofdeath.text = "Hit by a Truck";
			break;
		case CarType.None:
			deadColor = new Color32 (153, 133, 67, 255);
			causeofdeath.text = "Concussion";
			break;
		}
		//GameObject.Find ("Reason").GetComponent<Text> ().color = deadColor;
		causeofdeath.font.material.color = deadColor;
		causeofdeath.enabled = true;

		//GUI_genePoint.enabled = true;

		GUI_currentScore.enabled = true;
		//GUI_genePoint.text = string.Format ("GenePoint {0}", genePoint);
		GUI_currentScore.text = string.Format ("Score {0}", currentScore);
		for (int i = 0; i < 3; i++) {
			buttons [i].SetActive (true);
		}
	}

	void _PlayerDies() {
		StartCoroutine (PlayerDies ());
	}

	IEnumerator PlayerDies() {
		GameObject red = GameObject.Instantiate (redsplash, null) as GameObject;
		red.SetActive (true);
		player.SetActive (false);
		yield return new WaitForSeconds (0.6f);
		Messenger.Broadcast ("PlayerDead");
	}

	void WhenRestarted ()
	{
		print ("restarted");
		generation++;
		PlayerPrefs.SetInt ("Generation", generation);
		int gender = Random.Range (0, 2);
		PlayerPrefs.SetInt ("Gender", gender);
		loading.SetActive (true);
		loadskin.SetColor ("SKIN");
		loadeye.SetColor ("EYE");
		loadhand.SetColor ("SKIN");
		ColorSwap c;
		switch (gender){
		case 0:
			c = loadhair.GetComponent<ColorSwap> ();
			c.SetColor ("HAIR");
			loadhair_w.SetActive (false);
			loadcos_w.SetActive (false);
			break;
		case 1:
			c = loadhair_w.GetComponent<ColorSwap> ();
			c.SetColor ("HAIR");
			loadhair.SetActive (false);
			loadcos.SetActive (false);
			break;
		}
		SceneManager.LoadScene ("gameplay");
	}

	void RestartPopup ()
	{
		for (int i = 0; i < 3; i++) {
			buttons [i].SetActive (false);
		}
		title.SetActive (true);
	}
}
