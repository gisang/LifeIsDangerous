using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{

	public GameObject helpScreen, backButton, settingScreen;
	public GameObject[] buttons;

	public bool status;

	void Awake ()
	{
		status = false;
		Back ();
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit (); 
	}

	public void GameStart ()
	{
		SceneManager.LoadScene ("shop");
	}

	public void Help ()
	{
		if (!status) {
			backButton.SetActive (true);
			for (int i = 0; i < 3; i++) {
				buttons [i].SetActive (false);
			}
			helpScreen.SetActive (true);
			StartCoroutine(ChangeStatus());
		}
	}

	public void Back ()
	{
		if (status) {
			helpScreen.SetActive (false);
			backButton.SetActive (false);
			settingScreen.SetActive (false);
			for (int i = 0; i < 3; i++) {
				buttons [i].SetActive (true);
			}
			StartCoroutine(ChangeStatus());
		}
	}

	public void Setting ()
	{
		if (!status) {
			backButton.SetActive (true);
			for (int i = 0; i < 3; i++) {
				buttons [i].SetActive (false);
			}
			settingScreen.SetActive (true);
			StartCoroutine(ChangeStatus());
		}
	}

	IEnumerator ChangeStatus() {
		yield return new WaitForSeconds (0.1f);
		status = !status;
	}
}
