using UnityEngine;
using System.Collections;

public class CollisionDetector : MonoBehaviour {

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.name == Constants.AirplaneObjectName) {
			Destroy(collider.gameObject);
			GameController.EndGame();
			GameOverTextController.ShowGameOverText();
		}
	}
}
