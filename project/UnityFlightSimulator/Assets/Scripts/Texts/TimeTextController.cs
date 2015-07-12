using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeTextController : MonoBehaviour {
	private static Text _timeText;
	
	// Use this for initialization
	void Start () {
		_timeText = GetComponent<Text>();
	}
	
	public static void UpdateTimeText(string timeToDisplay) {
		_timeText.text = "Time: " + timeToDisplay;
	}
}
