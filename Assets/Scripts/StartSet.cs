using UnityEngine;
using System.Collections;

public class StartSet : MonoBehaviour {

	void Awake() {
		//Screen.SetResolution(1280,720,true);
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;
	}
}
