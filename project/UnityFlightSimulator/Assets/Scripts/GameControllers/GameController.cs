using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	private static bool _isGameOver = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (_isGameOver && Input.GetKeyDown(KeyCode.R))
		{
			_isGameOver = false;
			Time.timeScale = 1.0f;
			Application.LoadLevel(Application.loadedLevel);
		}

		UpdateDisplayedTime ();
	}

	public static void EndGame() {
		_isGameOver = true;
		Time.timeScale = 0.0f;
	}

	private void UpdateDisplayedTime()
	{
		TimeTextController.UpdateTimeText (Time.time.ToString());
	}
}
