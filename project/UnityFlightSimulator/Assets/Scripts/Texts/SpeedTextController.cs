using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpeedTextController : MonoBehaviour {
	private static Text _speedText;
	private static float _initializedSpeed = 0.0f;

	// Use this for initialization
	void Start () {
		_speedText = GetComponent<Text>();
		UpdateSpeedText(_initializedSpeed.ToString());
	}

	public static void InitializeSpeed(float speed)
	{
		_initializedSpeed = speed;
		if (_speedText != null) 
		{
			UpdateSpeedText(speed.ToString());
		}
	}
	
	// Update is called once per frame
	public static void UpdateSpeedText(string speed)
	{
		if (_speedText == null) {

		}
		_speedText.text = "Your speed: " + speed;
	}
}
