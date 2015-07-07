using UnityEngine;
using System.Collections;

public class GatesFactory : MonoBehaviour {

	public float VerticalAngleDeviationWhenGateShouldBeElavated = 40.0f;
	public float UpwardTranslationWhenGateIsAlmostHorizontal = 100.0f;

	public ArrayList InstantiateGatesForLocations(Vector3[] gateLocations, GameObject activeGate, GameObject inactiveGate)
	{
		var gates = new ArrayList ();
		var newGate = Instantiate (activeGate, gateLocations[0], Quaternion.identity);
		gates.Add (newGate);
		for(int gateIndex = 1; gateIndex < gateLocations.Length; gateIndex++)
		{
			var quaternion = GetRandomQuaternion();
			newGate = Instantiate (inactiveGate, gateLocations[gateIndex], quaternion);
			var gateGameObject = (GameObject)newGate;
			if (GateIsRotatedHorizontally(gateGameObject.transform.rotation.eulerAngles))
			{
				var upwardTranslation = new Vector3(0.0f, 0.0f, UpwardTranslationWhenGateIsAlmostHorizontal);
				gateGameObject.transform.Translate(upwardTranslation);
			}
			
			gates.Add (newGate);
		}

		return gates;
	}
	
	private float _horizontalAngle1 = 90.0f;
	private float _horizontalAngle2 = 270.0f;
	private bool GateIsRotatedHorizontally(Vector3 gateRotation)
	{
		bool gateIsRotatedHorizontally = 
			( (gateRotation.x >= _horizontalAngle1 -  VerticalAngleDeviationWhenGateShouldBeElavated)
			 		&& (gateRotation.x <= _horizontalAngle1 +  VerticalAngleDeviationWhenGateShouldBeElavated) )
			|| ( (gateRotation.x >= _horizontalAngle2 -  VerticalAngleDeviationWhenGateShouldBeElavated)
				    &&  (gateRotation.x <= _horizontalAngle2 +  VerticalAngleDeviationWhenGateShouldBeElavated) );
		
		return gateIsRotatedHorizontally;
	}
	
	private Quaternion GetRandomQuaternion()
	{
		var randomX = Random.Range (0, 10);
		var randomY = Random.Range (0, 100);
		var randomZ = Random.Range (0, 100);
		return new Quaternion (randomX, randomY, randomZ, 0);
	}
}
