using UnityEngine;
using System.Collections;

public class AirplaneMovement : MonoBehaviour {
	public float AirplaneForwardSpeed = 20.0f;
	public float AirplaneSteeringSpeed = 15.0f;

	void Start () {
	
	}
	
	// Chnage on the FixedUpdate if will be used RgidBody
	void Update () {
		MoveForward ();
		MoveInDirectionWantedByPlayer ();
	}

	void MoveForward() {
		float movementSpeed = Time.deltaTime * AirplaneForwardSpeed;
		transform.Translate(Vector3.up * movementSpeed);
	}

	void MoveInDirectionWantedByPlayer() {
		float translationHorizontal = Input.GetAxis("Horizontal") * (-1) * AirplaneSteeringSpeed;
		translationHorizontal *= Time.deltaTime;

		float translationVertical = Input.GetAxis("Vertical") * (-1) * AirplaneSteeringSpeed;
		translationVertical *= Time.deltaTime;

		transform.Rotate(translationVertical, translationHorizontal, 0);
	}
}
