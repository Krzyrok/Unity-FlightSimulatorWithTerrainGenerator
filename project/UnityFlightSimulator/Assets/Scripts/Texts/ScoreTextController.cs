using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreTextController : MonoBehaviour {
	private static Text _scoreText;

	// Use this for initialization
	void Start () {
		_scoreText = GetComponent<Text>();
		UpdateScoreText ("0");
	}

	public static void UpdateScoreText(string score) {
		_scoreText.text = "Your score: " + score;
	}
}
