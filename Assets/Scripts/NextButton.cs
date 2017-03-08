using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButton : MonoBehaviour
{

	void OnMouseDown ()
	{
		if (transform.rotation.z == 0) {
			Messenger.Broadcast<int> ("ShopChange", 2);
		} else {
			Messenger.Broadcast<int> ("ShopChange", 1);
		}
	}
}
