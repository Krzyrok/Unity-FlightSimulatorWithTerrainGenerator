using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeTextController : MonoBehaviour {

	static Text timeText;
	
	// Use this for initialization
	void Start () {
		timeText = GetComponent<Text>();
	}
	
	public static void UpdateTimeText(string timeToDisplay) {
		timeText.text = "Time: " + timeToDisplay;
	}
}
