using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class FriendAvatar : MonoBehaviour, IAvatarLoader {
	public Image image;
	

	void OnEnable () {
		Hide ();
	}

	public void ShowPicture () {
		StartCoroutine (WaitForPicture ());
	}

	IEnumerator WaitForPicture ()
	{
		yield return null;
	}


	void Hide () {
		GetComponent<SpriteRenderer> ().enabled = false;
		image.enabled = false;
	}

	void OnDisable () {
		Hide ();
	}

}
