using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashScene : MonoBehaviour {

	public float delayTime = 3;

	void Awake() {
		StartCoroutine (Splash ());
	}
	
	IEnumerator Splash() {
		yield return new WaitForSeconds(delayTime);
		SceneManager.LoadScene ("title");
	}
}
