using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	static bool isGameOver = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isGameOver && Input.GetKeyDown(KeyCode.R))
		{
			isGameOver = false;
			Time.timeScale = 1.0f;
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	public static void EndGame() {
		isGameOver = true;
		Time.timeScale = 0.0f;
	}
}
