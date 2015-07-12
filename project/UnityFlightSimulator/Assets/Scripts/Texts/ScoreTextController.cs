using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreTextController : MonoBehaviour {
	static Text scoreText;

	// Use this for initialization
	void Start () {
		scoreText = GetComponent<Text>();
		UpdateScoreText ("0");
	}

	public static void UpdateScoreText(string score) {
		scoreText.text = "Your score: " + score;
	}
}
