using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
	KIT,
	HELMET
}

public class ItemDisplay : MonoBehaviour
{

	public ItemType mItemType;
	public PlayerMovement playerScript;
	SpriteRenderer sp;

	void Awake() {
		sp = gameObject.GetComponent<SpriteRenderer> ();
	}

	void Update() {
		switch (mItemType) {
		case ItemType.KIT:
			if (playerScript.life > 1)
				sp.enabled = true;
			else
				sp.enabled = false;
			break;
		case ItemType.HELMET:
			if (playerScript.helmetOn)
				sp.enabled = true;
			else
				sp.enabled = false;
			break;
		}
	}
}
