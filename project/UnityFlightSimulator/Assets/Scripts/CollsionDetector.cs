using UnityEngine;
using System.Collections;

public class CollsionDetector : MonoBehaviour {

	void OnCollisionEnter(Collision collisionObject) {
		if (collisionObject.gameObject.name == Constants.AirplaneObjectName) {
			Destroy(collisionObject.gameObject);
			GameController.EndGame();
			GameOverTextController.ShowGameOverText();
		}
	}
}
