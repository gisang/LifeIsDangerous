using UnityEngine;
using System.Collections;

public class CauseofDeath : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void Awake() {
		Messenger.AddListener ("PlayerDead", deathCause);
	}

	void deathCause() {
		
	}
}
