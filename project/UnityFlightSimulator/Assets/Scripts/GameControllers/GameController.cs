using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	private static bool _isGameOver = false;
	private static bool _isGamePaused = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		RestartIfNecessary ();
		UpdateDisplayedTime ();
		PauseOrResumeIfNecessary ();
	}

	public static void EndGame() {
		_isGameOver = true;
		Time.timeScale = 0.0f;
	}

	private void RestartIfNecessary()
	{
		if (_isGameOver && Input.GetKeyDown(KeyCode.R))
		{
			_isGameOver = false;
			Time.timeScale = 1.0f;
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	private void UpdateDisplayedTime()
	{
		TimeTextController.UpdateTimeText (Time.time.ToString());
	}

	private void PauseOrResumeIfNecessary()
	{
		if (Input.GetKeyDown(KeyCode.F1))
		{
			if (_isGamePaused)
			{
				HelpTextController.HideHelpText();
				Time.timeScale = 1.0f;
			}
			else
			{
				HelpTextController.ShowHelpText();
				Time.timeScale = 0.0f;
			}
			_isGamePaused = !_isGamePaused;
		}
	}
}
