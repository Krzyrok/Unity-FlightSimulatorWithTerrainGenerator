using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverTextController : MonoBehaviour {
	private static Text _scoreText;

	// Use this for initialization
	void Start () {
		_scoreText = GetComponent<Text> ();
		_scoreText.text = "";
	}

	public static void ShowGameOverText() {
		_scoreText.text = "Game Over\nPress 'R' to restart";
	}
}
