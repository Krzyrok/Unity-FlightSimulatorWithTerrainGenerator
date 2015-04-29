using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverTextController : MonoBehaviour {
	static Text gameOverText;

	// Use this for initialization
	void Start () {
		gameOverText = GetComponent<Text> ();
		gameOverText.text = "";
	}

	public static void ShowGameOverText() {
		gameOverText.text = "Game Over\nPress 'R' to restart";
	}
}
