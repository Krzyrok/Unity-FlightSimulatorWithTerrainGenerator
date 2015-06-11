using UnityEngine;
using System.Collections;

public class ActiveGateCollisionDetector : MonoBehaviour {

	void OnCollisionEnter(Collision collisionObject) {
		if (collisionObject.gameObject.name == Constants.AirplaneObjectName) {
			ScoreTextController.AddScore(1);
			MyTerrain.UserCrossedTheGate();
		}
	}
}
