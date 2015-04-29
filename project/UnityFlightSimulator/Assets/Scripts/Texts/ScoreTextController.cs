using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreTextController : MonoBehaviour {
	static int score;
	static Text scoreText;

	// Use this for initialization
	void Start () {
		score = 0;
		scoreText = GetComponent<Text>();
		scoreText.text = "Your score: " + score;
	}

	public static void AddScore(int points) {
		score += points;
		UpdateScoreText ();
	}

	private static void UpdateScoreText() {
		scoreText.text = "Your score: " + score;
	}
}
