using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoButton : MonoBehaviour
{

	void OnMouseDown ()
	{
		Messenger.Broadcast ("GameRestart");
	}
}
