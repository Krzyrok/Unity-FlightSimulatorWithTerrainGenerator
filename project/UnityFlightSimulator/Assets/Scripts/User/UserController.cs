using UnityEngine;
using System.Collections;

public class UserController : MonoBehaviour {
	private int _score;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddScore(int points) {
		_score += points;
		ScoreTextController.UpdateScoreText (_score.ToString());
	}
}
