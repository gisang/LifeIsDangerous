using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBack : MonoBehaviour {

	public TitleScreen titleScript;

	void OnMouseDown() {
		titleScript.Back ();
	}
}
