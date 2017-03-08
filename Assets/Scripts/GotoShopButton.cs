using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GotoShopButton : MonoBehaviour
{

	void OnMouseOver ()
	{
		if (this.gameObject != null) {
			if (Input.GetMouseButtonDown (0)) {
				SceneManager.LoadScene ("shop");
			}
		}
	}
}
