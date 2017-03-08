using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ShopDataControl : MonoBehaviour {

	public int genePoint;
	public UnityEngine.UI.Text GUI_genePoint;
	public UnityEngine.UI.Text GUI_speedLevel;
	public UnityEngine.UI.Text GUI_speedCost;
	public UnityEngine.UI.Text GUI_luckLevel;
	public UnityEngine.UI.Text GUI_luckCost;
	public UnityEngine.UI.Text GUI_luckText;
	private int speedLv, luckLv;
	private int MAXSPEEDLV, MAXLUCKLV;
	private int speedCost, luckCost;
	private int cheap;
	private int generation;

	public int shopNum;

	public GameObject helmetButton, speedUpButton, speedLevelText, speedCostText, shopBg, nextButton, luckButton, luckLevelText, luckCostText, luckText, kitButton;
	public GameObject player,player_w, beforeButton, skinbutton, dyebutton, eyebutton;
	public HelmetButton helmetScript;
	public GameObject loading;

	void Start () {
		Messenger.AddListener ("GameRestart", WhenRestarted);
		Messenger.AddListener<int> ("ShopChange", ShopChange);
		genePoint = PlayerPrefs.GetInt ("GenePoint",0);
		speedLv = PlayerPrefs.GetInt ("SpeedLevel", 0);
		luckLv = PlayerPrefs.GetInt ("LuckLevel", 0);
		shopNum = 1;
		SetShop1 (true);
		SetShop2 (false);
	}

	void Awake() {
		speedCost = 100;
		luckCost = 100;
		MAXSPEEDLV = 100;
		MAXLUCKLV = 100;
		cheap = Mathf.Min (speedCost, luckCost);
		loading.SetActive (false);
	}

	void Update () {
		speedCost = 100 + speedLv*50;
		luckCost = 100 + luckLv*50;
		GUI_genePoint.text = string.Format ("{0}", genePoint);
		GUI_speedLevel.text = string.Format ("Lv. {0}", speedLv);
		if(speedLv<MAXSPEEDLV)
			GUI_speedCost.text = string.Format ("{0}", speedCost);
		else
			GUI_speedCost.text = string.Format ("MAX");
		GUI_luckLevel.text = string.Format ("Lv. {0}", luckLv);
		if(luckLv<MAXLUCKLV)
			GUI_luckCost.text = string.Format ("{0}", luckCost);
		else
			GUI_luckCost.text = string.Format ("MAX");
		GUI_luckText.text = string.Format ("{0}% chance to survive from cars", luckLv);

//		if (Input.GetKeyDown (KeyCode.F)) {
//			speedLv = MAXSPEEDLV;
//		}
//		if (Input.GetKeyDown (KeyCode.M)) {
//			genePoint = 99999;
//		}

		cheap = Mathf.Min (speedCost, luckCost);

	}

	public bool EnoughGene(int cost) {
		if (genePoint >= cost) {
			genePoint -= cost;
			PlayerPrefs.SetInt ("GenePoint", genePoint);
			return true;
		} else {
			Debug.Log ("Not enough GenePoint");
			return false;
		}
	}

	public void SpeedUp() {
		speedCost = 100 + speedLv*50;
		if (genePoint >= speedCost) {
			if (speedLv < MAXSPEEDLV) {
				genePoint -= speedCost;
				speedLv++;
				PlayerPrefs.SetInt ("SpeedLevel", speedLv);
				PlayerPrefs.SetInt ("GenePoint", genePoint);
			} else {
				Debug.Log ("MAX LEVEL");
			}		
		} else {
			Debug.Log ("Not enough GenePoint");
		}
	}

	public void LuckUp() {
		luckCost = 100 + luckLv*50;
		if (genePoint >= luckCost) {
			if (luckLv < MAXLUCKLV) {
				genePoint -= luckCost;
				luckLv++;
				PlayerPrefs.SetInt ("LuckLevel", luckLv);
				PlayerPrefs.SetInt ("GenePoint", genePoint);
			} else {
				Debug.Log ("MAX LEVEL");
			}		
		} else {
			Debug.Log ("Not enough GenePoint");
		}
	}


	void WhenRestarted() {
		PlayerPrefs.SetInt ("SpeedLevel", speedLv);
		PlayerPrefs.SetInt ("LuckLevel", luckLv);
		PlayerPrefs.SetInt ("Cheapest", cheap);
		generation = PlayerPrefs.GetInt ("Generation");
		generation++;
		PlayerPrefs.SetInt ("Generation", generation);
		int gender = Random.Range (0, 2);
		PlayerPrefs.SetInt ("Gender", gender);
		loading.SetActive (true);
		switch (gender) {
		case 0:
			player.SetActive (true);
			player_w.SetActive (false);
			player.transform.position = new Vector2 (0.00f, 0.12f);
			player.transform.localScale = new Vector2 (0.10f, 0.10f);
			break;
		case 1:
			player_w.SetActive (true);
			player.SetActive (false);
			player_w.transform.position = new Vector2 (0.00f, 0.12f);
			player_w.transform.localScale = new Vector2 (0.10f, 0.10f);
			break;
		}
		SceneManager.LoadScene ("gameplay");
	}

	void ShopChange(int shopto) {
		int shopfrom = shopNum;

		switch (shopfrom) {
		case 1:
			SetShop1 (false);
			break;
		case 2:
			SetShop2 (false);
			break;
		}

		switch (shopto) {
		case 1:
			shopNum = 1;
			SetShop1 (true);
			break;
		case 2:
			shopNum = 2;
			SetShop2 (true);
			break;
		}

	}

	void SetShop1(bool n) {
		helmetButton.SetActive (n);
		speedUpButton.SetActive (n);
		speedLevelText.SetActive (n);
		speedCostText.SetActive (n);
		shopBg.SetActive (n);
		luckButton.SetActive (n);
		//luckLevelText.SetActive (n);
		luckCostText.SetActive (n);
		luckText.SetActive (n);
		kitButton.SetActive (n);
	}

	void SetShop2(bool n) {
		SpriteRenderer sp1 = beforeButton.GetComponent<SpriteRenderer> ();
		SpriteRenderer sp2 = nextButton.GetComponent<SpriteRenderer> ();
		Color c1 = sp1.color;
		Color c2 = sp2.color;
		if (n) {
			player.transform.position = new Vector2 (-1.55f, 0.12f);
			player.transform.localScale = new Vector2 (0.13f, 0.13f);
			player_w.transform.position = new Vector2 (1.69f, 0.12f);
			player_w.transform.localScale = new Vector2 (0.13f, 0.13f);
			c1.a = 1.0f;
			c2.a = 0.3f;
			sp1.color = c1;
			sp2.color = c2;
		} else {
			c1.a = 0.3f;
			c2.a = 1.0f;
			sp1.color = c1;
			sp2.color = c2;
		}
		player.SetActive (n);
		player_w.SetActive (n);
		skinbutton.SetActive (n);
		dyebutton.SetActive (n);
		eyebutton.SetActive (n);
	}
}

// 0.87,3 / -2.79,0.7 / 4.65,0.6