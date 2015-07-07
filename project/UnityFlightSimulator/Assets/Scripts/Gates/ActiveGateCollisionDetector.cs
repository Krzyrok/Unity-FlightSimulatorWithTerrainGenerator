using UnityEngine;
using System.Collections;

public class ActiveGateCollisionDetector : MonoBehaviour {
	private GatesController _gatesController;

	void Awake () {
		var gatesControllerObject = GameObject.Find(Constants.GatesControllerObjectName);  
		_gatesController = gatesControllerObject.GetComponent<GatesController> ();
	}

	void OnCollisionEnter(Collision collisionObject) {
		if (collisionObject.gameObject.name == Constants.AirplaneObjectName) {
			ScoreTextController.AddScore(1);
			_gatesController.ShowNextActiveGateOrEndGame();
		}
	}
}
