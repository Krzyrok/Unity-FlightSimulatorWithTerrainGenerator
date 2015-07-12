using UnityEngine;
using System.Collections;

public class AirplaneMovement : MonoBehaviour {
	public float AirplaneForwardSpeed = 60.0f;
	public float MaximumAirplaneForwardSpeed = 100.0f;
	public float MinimumAirplaneForwardSpeed = 20.0f;
	public float AirplaneAccelerationSpeed = 20.0f;

	public float AirplaneSteeringSpeed = 40.0f;
	public AudioSource EngineSound;

	void Start () 
	{ 
		EngineSound.loop = true;
		EngineSound.Play();
		SpeedTextController.InitializeSpeed (AirplaneForwardSpeed);
	}
	
	// Chnage on the FixedUpdate if will be used RgidBody
	void Update () {
		Rotate ();
		SetSpeed ();
		Move ();
	}

	private void Rotate() {
		float translationHorizontal = Input.GetAxis("Horizontal") * (-1) * AirplaneSteeringSpeed;
		translationHorizontal *= Time.deltaTime;

		float translationVertical = Input.GetAxis("Vertical") * (-1) * AirplaneSteeringSpeed;
		translationVertical *= Time.deltaTime;

		transform.Rotate(translationVertical, translationHorizontal, 0);
	}

	private void SetSpeed()
	{
		if (Input.GetKey (KeyCode.LeftControl)) {
			AirplaneForwardSpeed += AirplaneAccelerationSpeed * Time.deltaTime;
			CheckMaximumAndMinimumForwardSpeed();
		} else if (Input.GetKey(KeyCode.LeftAlt)){
			AirplaneForwardSpeed -= AirplaneAccelerationSpeed * Time.deltaTime;
			CheckMaximumAndMinimumForwardSpeed();
		}
	}

	private void CheckMaximumAndMinimumForwardSpeed()
	{
		if (AirplaneForwardSpeed > MaximumAirplaneForwardSpeed) {
			AirplaneForwardSpeed = MaximumAirplaneForwardSpeed;
		} else if (AirplaneForwardSpeed < MinimumAirplaneForwardSpeed){
			AirplaneForwardSpeed = MinimumAirplaneForwardSpeed;
		}

		SpeedTextController.UpdateSpeedText (AirplaneForwardSpeed.ToString());
	}

	private void Move() {
		float movementSpeed = Time.deltaTime * AirplaneForwardSpeed;
		transform.Translate (Vector3.up * movementSpeed);
	}
}
