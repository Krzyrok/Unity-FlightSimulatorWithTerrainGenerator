using UnityEngine;
using System.Collections;

public class AirplaneMovement : MonoBehaviour {
	public float AirplaneForwardSpeed = 20.0f;
	public float AirplaneSteeringSpeed = 15.0f;
	public AudioSource EngineSound;
	void Start () 
	{ 
		EngineSound.loop = true;
		EngineSound.Play();
	}
	
	// Chnage on the FixedUpdate if will be used RgidBody
	void Update () {
		Rotate ();
		SetSpeed ();
		Move ();
	}

	void Rotate() {
		float translationHorizontal = Input.GetAxis("Horizontal") * (-1) * AirplaneSteeringSpeed;
		translationHorizontal *= Time.deltaTime;

		float translationVertical = Input.GetAxis("Vertical") * (-1) * AirplaneSteeringSpeed;
		translationVertical *= Time.deltaTime;

		transform.Rotate(translationVertical, translationHorizontal, 0);
	}

	void SetSpeed()
	{
		if (Input.GetKey (KeyCode.LeftControl)) {
			AirplaneForwardSpeed += 5 * Time.deltaTime;
		} else if (Input.GetKey(KeyCode.LeftAlt)){
			AirplaneForwardSpeed -= 5 * Time.deltaTime;
		}
		if (AirplaneForwardSpeed > 40) {
			AirplaneForwardSpeed = 40;
		} else if (AirplaneForwardSpeed < 8){
			AirplaneForwardSpeed = 8;
		}
	}

	void Move() {
		float movementSpeed = Time.deltaTime * AirplaneForwardSpeed;
		if (AirplaneForwardSpeed > 8) {
			transform.Translate (Vector3.up * movementSpeed);
		} else {
			transform.Translate (Vector3.down * movementSpeed,Space.World);
			transform.Translate (Vector3.up * movementSpeed*0.6f);
		}
	}
}
