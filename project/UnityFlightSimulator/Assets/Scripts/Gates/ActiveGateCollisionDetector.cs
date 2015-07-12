using UnityEngine;
using System.Collections;

public class ActiveGateCollisionDetector : MonoBehaviour {
	public int PointsForPassedGate = 1;
	private GatesController _gatesController;

	void Awake () {
		var gatesControllerObject = GameObject.Find(Constants.GatesControllerObjectName);  
		_gatesController = gatesControllerObject.GetComponent<GatesController> ();
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.name == Constants.AirplaneObjectName) {
			var userController = collider.gameObject.GetComponent<UserController>();
			userController.AddScore(PointsForPassedGate);
			_gatesController.ShowNextActiveGateOrEndGame();
		}
	}
}
