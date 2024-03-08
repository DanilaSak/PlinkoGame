using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LeadboardObject : MonoBehaviour {
	public Image icon;
	public Text place;
	public Text playerName;
	public Text score;
	#if PLAYFAB || GAMESPARKS


	void SetupIcon () {
		StartCoroutine (WaitForPicture ());
	}

	IEnumerator WaitForPicture () {
		print ("wait for picture");
		yield return null;


	}

	#endif

}
